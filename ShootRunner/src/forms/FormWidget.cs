﻿using Microsoft.Web.WebView2.Core;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json;

#nullable disable


namespace ShootRunner
{

    public partial class FormWidget : Form
    {
        public Widget widget = null;
        public Microsoft.Web.WebView2.WinForms.WebView2 webView = null;

        public FormWidget(Widget widget)
        {
            this.widget = widget;
            widget.widgetForm = this;
            InitializeComponent();
            InitializeWebView2();
        }

        private void FormWidget_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(64, 64);
            this.SetStartPosition();
            this.ShowInTaskbar = false;
            this.Deactivate += new EventHandler(this.Form_Deactivate);
            this.Activated += new EventHandler(this.Form_Activated);
            this.Opacity = this.widget.transparent < 0.2 ? 0.2 : this.widget.transparent;

            AddTypesToContextMenu();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= ToolsWindow.WS_EX_TOOLWINDOW; // Add the tool window style
                return cp;
            }
        }

        public void AddTypesToContextMenu()
        {

            foreach (var widgeType in Program.widgetManager.widgeTypes)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(widgeType.name);
                item.Click += (sender, e) => SelectType(widgeType);
                typeToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        public void SelectType(WidgetType widgetType)
        {
            widget.widgetType = widgetType;
            widget.type = widgetType.name;
            this.InitializeWebView2();
        }

        public void Center()
        {
            Screen currentScreen = Screen.FromPoint(Cursor.Position);
            Rectangle screenBounds = currentScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(
                screenBounds.Left + (screenBounds.Width - this.Width) / 2,
                screenBounds.Top + (screenBounds.Height - this.Height) / 2
            );
        }

        public void SetStartPosition()
        {
            this.Resize -= FormWidget_Resize;
            this.Move -= FormWidget_Move;
            this.Left = this.widget.StartLeft;
            this.Top = this.widget.StartTop;
            this.Width = this.widget.StartWidth;
            this.Height = this.widget.StartHeight;
            this.Resize += FormWidget_Resize;
            this.Move += FormWidget_Move;
        }

        ///////////////////////////////////////////////////////////////////////

        private async void InitializeWebView2()
        {

            if (webView != null)
            {
                this.Controls.Remove(webView);
                webView.Dispose();
            }

            try
            {
                webView = new Microsoft.Web.WebView2.WinForms.WebView2();
                webView.Dock = DockStyle.Fill;

                this.Controls.Add(webView);


                var environment = await CoreWebView2Environment.CreateAsync(userDataFolder: Program.webview2Path);
                await webView.EnsureCoreWebView2Async(environment);

                await webView.CoreWebView2.CallDevToolsProtocolMethodAsync(
                    "Runtime.enable", "{}");

                await webView.CoreWebView2.CallDevToolsProtocolMethodAsync(
                    "Log.enable", "{}");

                webView.CoreWebView2.WebMessageReceived += WebView_WebMessageReceived;

                webView.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;

                string htmlContent = "";
                if (widget != null && widget.type != null && widget.widgetType != null)
                {
                    htmlContent = widget.widgetType.html;
                }

                string script1 = @"

                    (function() {
                        const originalLog = console.log;
                        const originalError = console.error;

                        // Override console.log
                        console.log = function(...args) {
                            window.chrome.webview.postMessage({type: 'log', message: args.join(' ')});
                            originalLog.apply(console, args);
                        };

                        // Override console.error
                        console.error = function(...args) {
                            window.chrome.webview.postMessage({type: 'error', message: args.join(' ')});
                            originalError.apply(console, args);
                        };

                        window.onerror = function(...args) {
                            window.chrome.webview.postMessage({type: 'error', message: args.join(' ')});
                            return true;
                        };


                    })();              

                ";


                await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script1);

                string script2 = @"

                    function uid(length = 32) {
                        const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
                        let result = '';
                        const charactersLength = characters.length;
                        for (let i = 0; i < length; i++) {
                            const randomIndex = Math.floor(Math.random() * charactersLength);
                            result += characters.charAt(randomIndex);
                        }
                        return result;
                    }

                    function setData(k,v){
                        return send('setData', null, k ,v, null);
                    }

                    function getData(k) {
                        return send('getData', null, k , null, null);
                    }

                    function send(t,m,k,v,p){
                        var message = {
                            uid: uid(),
                            parent: p,
                            type: t, 
                            message: m,
                            key: k,
                            value: v                        
                        };
                        window.chrome.webview.postMessage(message);
                        return message;
                    }

                    window.receiveMessage = function(response) {
                        document.getElementById('response').innerText = data.message;
                    };

                    window.chrome.webview.addEventListener('message', event => {
                        const response = event.data;
                        if(typeof window.receiveMessage === 'function') { 
                            window.receiveMessage(response);
                        }
                    });

                ";

                await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script2);

                webView.NavigationCompleted += WebView_NavigationCompleted;

                webView.NavigateToString(htmlContent);



            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }

        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                string titleJson = await webView.CoreWebView2.ExecuteScriptAsync("document.title");
                string title = System.Text.Json.JsonSerializer.Deserialize<string>(titleJson);
                this.Text = title == null || title.Trim() == "" ? "Widget" : title;
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }
        }

        ///////////////////////////////////////////////////////////////////////

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string json = e.WebMessageAsJson;
            Program.debug($"JS: {json}");

            try
            {
                JSMessage request = JsonSerializer.Deserialize<JSMessage>(json);

                if (request.type == "log")
                {
                    Program.debug("JS: " + request.message);

                }

                if (request.type == "error")
                {
                    Program.debug("JS ERROR: " + request.message);

                }

                if (request.type == "setData" && request.key != "")
                {
                    this.widget.data[request.key] = request.value;
                    Program.Update();
                }

                if (request.type == "getData")
                {
                    JSMessage response = new JSMessage();
                    response.uid = Generator.uid();
                    response.parent = request.uid;
                    response.key = request.key;
                    response.value = this.widget.data.ContainsKey(request.key) ? this.widget.data[request.key] : "";
                    response.message = "";
                    string responseJson = System.Text.Json.JsonSerializer.Serialize(response);
                    webView.CoreWebView2.PostWebMessageAsJson(responseJson);
                }
            }
            catch (Exception)
            {
            }
        }

        private void CoreWebView2_ContextMenuRequested(object sender, CoreWebView2ContextMenuRequestedEventArgs e)
        {
            e.Handled = true;

            ContextMenuStrip menu = this.contextMenuStrip;
            menu.Show(Cursor.Position);
        }

        ///////////////////////////////////////////////////////////////////////

        protected override void WndProc(ref Message m)
        {

            if (this.widget.locked && (m.Msg == ToolsWindow.WM_NCLBUTTONDOWN && m.WParam.ToInt32() == ToolsWindow.HTCAPTION))
            {
                return;
            }

            base.WndProc(ref m);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Form_Deactivate(object sender, EventArgs e)
        {
            ToolsWindow.RemoveTitleBar(this.Handle);


        }

        private void Form_Activated(object sender, EventArgs e)
        {
            ToolsWindow.AddTitleBar(this.Handle);
            this.Refresh();
        }

        private void FormWidget_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseForm();
        }

        private void FormWidget_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.closingApplication)
            {
                return;
            }

            // Display a confirmation dialog
            DialogResult result = MessageBox.Show(
                "Are you sure you want to close form? Data will be lost.",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        public void CloseForm()
        {
            Program.widgetManager.RemoveWidget(this);
            Program.Update();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mostTopToolStripMenuItem.Checked = this.TopMost;
            lockedToolStripMenuItem.Checked = this.widget.locked;
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTransparent form = new FormTransparent(this, null);
            form.trackBar1.Value = (int)(this.Opacity * 100);
            form.Show();
            this.widget.transparent = this.Opacity;
        }

        private void removeWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.AddEmptyWidget();
        }

        private void mostTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            mostTopToolStripMenuItem.Checked = this.TopMost;
        }

        private void lockedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.widget.locked = !this.widget.locked;
            lockedToolStripMenuItem.Checked = this.widget.locked;
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Exit();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (widget == null || widget.widgetType == null)
            {
                return;
            }

            try
            {
                widget.widgetType.html = File.ReadAllText(widget.widgetType.source);
                InitializeWebView2();
            }
            catch (Exception)
            {


            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (widget == null || widget.widgetType == null || !File.Exists(widget.widgetType.source))
            {
                return;
            }

            try
            {
                string defaultEditor = SystemTools.GetDefaultEditorPath(".txt");
                if (!string.IsNullOrEmpty(defaultEditor))
                {
                    SystemTools.OpenFileWithEditor(defaultEditor, widget.widgetType.source);
                }
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }
        }

        private void FormWidget_Resize(object sender, EventArgs e)
        {
            this.widget.StartLeft = this.Left;
            this.widget.StartTop = this.Top;
            this.widget.StartWidth = this.Width;
            this.widget.StartHeight = this.Height;
            Program.Update();
            this.Refresh();
        }

        private void FormWidget_Move(object sender, EventArgs e)
        {
            this.widget.StartLeft = this.Left;
            this.widget.StartTop = this.Top;
            this.widget.StartWidth = this.Width;
            this.widget.StartHeight = this.Height;
            Program.Update();
            this.Refresh();
        }

        private void createWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.widgetManager.ShowCreateWidgetForm();
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowConsole();
        }
    }
}
