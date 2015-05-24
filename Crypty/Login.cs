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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            #region Events

            exitButton.Click += (sender, args) => Application.Exit();
             
            #endregion
        }

        private bool CheckPasswordField()
        {
            return textBox1.Text != null;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (!CheckPasswordField()) return;
            if (Security.Login(textBox1.Text))
            {
                new Crypty().Show();
            }
            else
            {
                MessageBox.Show(@"Check your password and try again", @"Wrong password", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (CheckPasswordField())
            {
                Security.Register(textBox1.Text);
            }
        }


    }
}
