using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crypty.DataSetTableAdapters;

namespace Crypty
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Contains("-avoid_password"))
            {
                Application.Run(new Crypty());
            }
            else
            {
                Application.Run(new Login());    
            }
            
        }
    }
}
