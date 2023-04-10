using MoneyAdministrator.DTOs;
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
        int SelectedSummaryId { get; set; }

        DateTime Period { get; set; }
        DateTime Date { get; set; }
        DateTime Expiration { get; set; }
        DateTime NextDate { get; set; }
        DateTime NextExpiration { get; set; }
        decimal TotalArs { get; set; }
        decimal TotalUsd { get; set; }
        decimal minimumPayment { get; set; }

        bool ImportedSummary { get; set; }

        //methods
        void TvRefreshData(List<TreeViewSummaryListDto> datasource);

        //events
        event EventHandler ButtonImportClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonExitClick;
        event EventHandler ButtonSearchCreditCardClick;
        event EventHandler SummaryListNodeClick;
    }
}
