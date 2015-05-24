using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypty.DataSetTableAdapters;

namespace Crypty
{
    static class Security
    {
        private static bool IsPasswordAlreadyExist()
        {
            string password = null;
            var passTableAdapter = new PassTableAdapter();
            try
            {
                password = passTableAdapter.GetData()[0].Password;
            }
            catch (Exception e)
            {
                
            }
            if (password != null) return true;
            return false;
        }

        public static void Register(string password)
        {
            if (IsPasswordAlreadyExist()) return;
            var passTableAdapter = new PassTableAdapter();
            passTableAdapter.Insert(password);
        }

        public static bool Login(string password)
        {
            if (!IsPasswordAlreadyExist()) return false;
            var passTableAdapter = new PassTableAdapter();
            var currentPassword = passTableAdapter.GetData()[0].Password;
            return currentPassword == password;
        }
    }
}
