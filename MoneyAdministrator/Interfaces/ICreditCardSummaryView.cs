using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface ICreditCardSummaryView
    {
        //properties
        CreditCard CreditCard { get; set; }
        List<CreditCardSummaryDetailDto> CCSummaryDetailDtos { get; set; }
        int CCSummaryId { get; set; }

        DateTime Period { get; set; }
        DateTime Date { get; set; }
        DateTime Expiration { get; set; }
        DateTime NextDate { get; set; }
        DateTime NextExpiration { get; set; }
        decimal TotalArs { get; set; }
        decimal TotalUsd { get; set; }
        decimal minimumPayment { get; set; }
        decimal OutstandingArs { get; set; }

        bool SummaryImported { get; set; }

        //methods
        void TvRefreshData(List<TreeViewSummaryListDto> datasource);
        void GrdPaymentsRefreshData(List<CreditCardPayDto> datasource);

        //events
        event EventHandler ButtonImportClick;
        event EventHandler ButtonNewPayClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonExitClick;
        event EventHandler ButtonSearchCreditCardClick;
        event EventHandler SummaryListNodeClick;
    }
}
