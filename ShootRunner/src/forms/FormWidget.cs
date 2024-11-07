using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;
using ShootRunner.src.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ShootRunner
{

    public partial class FormWidget : Form
    {
        public Widget widget = null;
        public Microsoft.Web.WebView2.WinForms.WebView2 webView = null;

        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

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
                const int WS_EX_TOOLWINDOW = 0x00000080;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW; // Add the tool window style
                return cp;
            }
        }

        public void AddTypesToContextMenu()
        {

            foreach (var widgeType in Program.widgetManager.widgeTypes ) {
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
            this.Left = this.widget.StartLeft;
            this.Top = this.widget.StartTop;
            this.Width = this.widget.StartWidth;
            this.Height = this.widget.StartHeight;
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
                if (widget!= null && widget.type != null && widget.widgetType != null) {
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

                if (request.type == "getData") {
                    JSMessage response = new JSMessage();
                    response.uid = Program.widgetManager.uid();
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

        private void RemoveTitleBar()
        {
            /*initTop = this.Top;
            initHeight = this.Height;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Top = initTop + initialCaptionHeight;
            this.Height = initHeight - initialCaptionHeight;*/


            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, style & ~WS_CAPTION);
            this.Refresh();

            //titleBarHeight = SystemInformation.CaptionHeight;
            //ToggleBorder(FormBorderStyle.None);


        }

        private void AddTitleBar()
        {

            /*this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Top = initTop;
            this.Height = initHeight;*/

            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, style | WS_CAPTION);
            this.Refresh();

            //ToggleBorder(FormBorderStyle.Sizable);
        }

        protected override void WndProc(ref Message m)
        {
            
            if (this.widget.locked && ( m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION))
            {
                return; 
            }

            base.WndProc(ref m);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Form_Deactivate(object sender, EventArgs e)
        {
            RemoveTitleBar();
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            AddTitleBar();
        }

        private void FormWidget_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseForm();
        }

        private void FormWidget_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            form.ShowDialog();
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
            Application.Exit();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (widget ==null || widget.widgetType == null) {
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
                else
                {
                    Console.WriteLine("Default text editor not found.");
                }
            }
            catch (Exception ex)
            {
               Program.error($"Error opening file: {ex.Message}");
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            /*string titleJson = await webView.ExecuteScriptAsync("document.title");
            string title = System.Text.Json.JsonSerializer.Deserialize<string>(titleJson);
            this.Text = title == null || title.Trim() == "" ? "Widget" : title;*/
        }
    }
}
