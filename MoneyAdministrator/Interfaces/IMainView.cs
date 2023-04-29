using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface IMainView
    {
        //properties
        /// <summary>Informa si la aplicacion tiene una base de datos abierta</summary>
        bool IsFileOpened { get; set; }

        //Methods
        /// <summary>Cierro la ventana abierta en el panel principal</summary>
        void CloseChildrens();
        /// <summary>Abro una ventana en el panel principal</summary>
        void OpenChildren(UserControl children);
        /// <summary>Selecciono el boton Dashboard</summary>
        void SelectDashboardButton();

        //events
        event EventHandler ShowDashboard;
        event EventHandler ShowTransactionHistory;
        event EventHandler ShowCreditCardSummary;
        event EventHandler FileNew;
        event EventHandler FileOpen;
        event EventHandler FileClose;
    }
}
