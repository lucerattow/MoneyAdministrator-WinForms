using MoneyAdministrator.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface ICreditCardPayView
    {
        //properties
        int TransactionOutstandingId { get; set; }
        string CreditCardDescription { set; }
        int SelectedTransactionDetail { get;  set; }
        DateTime PayDay { get; set; }
        decimal AmountPay { get; set; }

        //methods
        /// <summary>Envio los datos de las transacciones al GRD de CreditCardPay</summary>
        /// <param name="datasource">DTOs para rellenar la GRD de CreditCardPay</param>
        void GrdRefreshData(List<CreditCardPayDto> datasource, DateTime period);

        //functions
        /// <summary>Muestra el formulario como Dialogo</summary>
        DialogResult ShowDialog();

        //events
        event EventHandler GrdDoubleClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonDeleteClick;
    }
}
