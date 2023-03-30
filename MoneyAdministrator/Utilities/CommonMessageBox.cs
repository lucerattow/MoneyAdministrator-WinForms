using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities
{
    public class CommonMessageBox
    {
        public static DialogResult errorMessageShow(string message, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, $"{ConfigurationManager.AppSettings["AppTitle"]} : Error", buttons, MessageBoxIcon.Error);
        }
    }
}
