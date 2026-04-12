using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

namespace ShootRunner
{
    public partial class FormCommandEditor : Form
    {
        string fileNewLine = "\r\n";
        string indentChars = "  ";

        public FormCommandEditor()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                string content = textBoxCommand.Text;

                if (ValidateXml(content, out var error))
                {
                    content = ConvertLineEndings(content, this.fileNewLine);
                    File.WriteAllText(Program.commandFielPath, content);
                    this.Close();
                }
                else
                {
                    labelError.Text = "No Error";
                }
            }
            catch (Exception)
            {

            }
        }


        private void FormConfigEditor_Load(object sender, EventArgs e)
        {
            try
            {
                string content = File.ReadAllText(Program.commandFielPath);
                this.fileNewLine = DetectLineEnding(content);
                content = NormalizeToCrLf(content);
                textBoxCommand.Text = content;
            }
            catch (Exception)
            {


            }

            webViewHelp.EnsureCoreWebView2Async().ContinueWith(_ =>
            {
                webViewHelp.NavigateToString(@"
            <html><body>
            <style>
            h1{font-size:16px;color:white;}</style>
            <h1>Help</h1>
            </body></html>");
            });

        }

        string ConvertLineEndings(string text, string newline)
        {
            return text
                .Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Replace("\n", newline);
        }

        string NormalizeToCrLf(string text)
        {
            return text.Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Replace("\n", "\r\n");
        }

        string DetectLineEnding(string text)
        {
            int rn = text.IndexOf("\r\n");
            int n = text.IndexOf("\n");
            int r = text.IndexOf("\r");

            if (rn >= 0) return "\r\n";
            if (n >= 0) return "\n";
            if (r >= 0) return "\r";

            return Environment.NewLine;
        }

        public bool ValidateXml(string xml, out string error)
        {
            error = null;

            try
            {
                var settings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Ignore
                };

                using var reader = XmlReader.Create(new StringReader(xml), settings);

                while (reader.Read()) { }

                return true;
            }
            catch (XmlException ex)
            {
                error = $"Line {ex.LineNumber}, Position {ex.LinePosition}: {ex.Message}";
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!ValidateXml(textBoxCommand.Text, out var error))
            {
                labelError.Text = error;
            }
            else
            {
                labelError.Text = "No Error";
            }
        }

        private void textBox1_Resize(object sender, EventArgs e)
        {
            buttonOK.Left = this.Width - buttonOK.Width - 30;
        }

        private void CursorChanged(object sender, EventArgs e)
        {
            int index = textBoxCommand.SelectionStart;

            int line = textBoxCommand.GetLineFromCharIndex(index); // 0-based
            int col = index - textBoxCommand.GetFirstCharIndexFromLine(line);

            toolStripStatusLabelPosition.Text = $"Line {line + 1}, Col {col + 1}";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.CursorChanged(sender, e);
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.CursorChanged(sender, e);
        }

        string PrettyPrintXml(string xml)
        {
            if (!ValidateXml(xml, out var error))
            {
                return xml;
            }

            var doc = XDocument.Parse(xml);

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = this.indentChars,
                NewLineChars = this.fileNewLine,
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false
            };

            using var sw = new StringWriter();
            using (var xw = XmlWriter.Create(sw, settings))
            {
                doc.Save(xw);
                xw.Flush();
            }

            return sw.ToString();
        }

        private void buttonPretty_Click(object sender, EventArgs e)
        {
            string content = textBoxCommand.Text;

            if (ValidateXml(content, out var error))
            {
                textBoxCommand.Text = NormalizeToCrLf(this.PrettyPrintXml(content));
            }
        }

        private void buttonExternalEditor_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(Program.commandFielPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
