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
    public partial class EncodeType : Form
    {
        public EncodeType()
        {
            InitializeComponent();

            #region Events

            cancelButton.Click += (sender, args) => this.Close();
            encodeButton.Click += (sender, args) => { if (keyTextBox.Text != "") openFileDialog1.ShowDialog(); };

            #endregion
        }
    }
}
