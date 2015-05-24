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
            Loger.CheckDirectory(); // Need to initialize log files folder

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
                Loger.AddToJournal(Loger.LogKind.Info, @"Successfull login");
                new Crypty().Show();
            }
            else
            {
                Loger.AddToJournal(Loger.LogKind.Warning, @"Wrong login");
                MessageBox.Show(@"Check your password and try again", @"Wrong password", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (CheckPasswordField())
            {
                Loger.AddToJournal(Loger.LogKind.Info, @"Successfull registration");
                Security.Register(textBox1.Text);
            }
        }


    }
}
