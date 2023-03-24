using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface IMainView
    {
        //Events
        event EventHandler ShowTransactionHistory;
        event EventHandler FileNew;
        event EventHandler FileOpen;
        event EventHandler FileClose;

        //Properties
        bool IsFileOpened { get; set; }

        //Methods
        /// <summary>Cierro la ventana abierta en el panel principal</summary>
        void CloseChildrens();

        /// <summary>Abro una ventana en el panel principal</summary>
        void OpenChildren(UserControl children);
    }
}
