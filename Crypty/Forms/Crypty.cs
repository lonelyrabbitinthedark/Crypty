using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypty
{
    public partial class Crypty : Form
    {
        public Crypty()
        {
            InitializeComponent();
            Loger.CheckDirectory(); // Need to initialize log files folder

            #region Events

            exitToolStripMenuItem.Click += (sender, args) => Application.Exit(); // Exit from application
            aboutToolStripMenuItem.Click += (sender, args) => { new AboutBox().ShowDialog(); }; // Show aboutbox whith info about program
            settingsToolStripMenuItem.Click += (sender, args) => { new Settings().ShowDialog(); }; // Show application settings
            dataReportToolStripMenuItem.Click += (sender, args) => { new DataReport().Show(); }; // Show data from database
            journalToolStripMenuItem.Click += (sender, args) => { new LogViewer().Show(); }; // Show application log file
            cryptFileToolStripMenuItem.Click += (sender, args) => EncryptFile();

            #endregion

            //TestCrypt();
        }

        private void TestCrypt()
        {
            byte[] key = ASCIIEncoding.ASCII.GetBytes("Live0101");
            var encoder = new Cryprography.RC4(key);
            byte[] cryptBytes = ASCIIEncoding.ASCII.GetBytes("Sometext");
            byte[] resultBytes = encoder.Encode(cryptBytes, cryptBytes.Length);

            var decoder = new Cryprography.RC4(key);
            byte[] decryptedBytes = decoder.Decode(resultBytes, resultBytes.Length);
            MessageBox.Show(ASCIIEncoding.ASCII.GetString(decryptedBytes));

        }

        private void EncryptFile()
        {
            new EncodeType().ShowDialog();
        }
    }
}
