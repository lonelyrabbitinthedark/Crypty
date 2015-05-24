using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Crypty
{
    public partial class LogViewer : Form
    {
        private string _currentDocumentName;
        public LogViewer()
        {
            InitializeComponent();

            #region Events

            this.Load += (sender, args) => { FillTreeView(treeView1, false); };
            treeView1.NodeMouseClick += (sender, args) =>
            {
                _currentDocumentName = Environment.CurrentDirectory + @"\" + Loger.JournalDirectoryName + @"\" +
                                       args.Node.Text;
                richTextBox1.Text = GetTextFromFile(_currentDocumentName);
                
            };
            closeToolStripMenuItem.Click += (sender, args) => this.Close();
            printPreviewToolStripMenuItem.Click += (sender, args) => PreviewPrint(Environment.CurrentDirectory + @"\" + Loger.JournalDirectoryName + @"\" +
                                         treeView1.SelectedNode.Text);
            openInNotepadToolStripMenuItem.Click += (sender, args) => OpenFileInNotePad(_currentDocumentName);
            updateToolStripMenuItem.Click += (sender, args) => FillTreeView(treeView1, true);

            #endregion
        }

        private void PreviewPrint(string filePath)
        {
            if (_currentDocumentName == null)
            {
                MessageBox.Show(@"Please choose file at first", @"Nothing to show", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            _currentDocumentName = filePath;
            var printDocument = new PrintDocument {DocumentName = "LogViewer"};
            printDocument.PrintPage += document_PrintPage;
            printPreviewDialog1.Document = printDocument;
            printPreviewDialog1.UseAntiAlias = true;
            printPreviewDialog1.ShowDialog();
        }

        private void OpenFileInNotePad(string path)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = Environment.SystemDirectory + @"\" + "notepad.exe",
                    Arguments = _currentDocumentName
                }
            };
            process.Start();
        }

        private void FillTreeView(TreeView treeView, bool refill)
        {
            if (refill)
            {
                treeView1.Nodes.Clear();
            }
            Loger.CheckDirectory();
            var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\" + Loger.JournalDirectoryName);
            var files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                treeView.Nodes.Add(file.ToString());
            }
        }

        private string GetTextFromFile(string fileName)
        {
            using (var streamReader = new StreamReader(fileName, Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }

        private void document_PrintPage(object sender,
        PrintPageEventArgs e)
        {
            var text = GetTextFromFile(_currentDocumentName);
            var printFont =
                new Font("Arial", 35,
                FontStyle.Regular);

            e.Graphics.DrawString(text, printFont,
                Brushes.Black, 0, 0);

        }
    }
}
