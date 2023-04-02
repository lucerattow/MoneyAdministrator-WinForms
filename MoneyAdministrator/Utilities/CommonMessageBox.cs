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
        public static DialogResult errorMessageShow(string message, MessageBoxButtons buttons, string title = "Error")
        {
            return MessageBox.Show(message, $"{ConfigurationManager.AppSettings["AppTitle"]} : {title}", buttons, MessageBoxIcon.Error);
        }

        public static DialogResult warningMessageShow(string message, MessageBoxButtons buttons, string title = "Atencion!")
        {
            return MessageBox.Show(message, $"{ConfigurationManager.AppSettings["AppTitle"]} : {title}", buttons, MessageBoxIcon.Warning);
        }
    }
}
