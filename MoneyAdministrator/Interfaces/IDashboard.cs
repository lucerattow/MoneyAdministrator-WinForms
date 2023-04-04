using MoneyAdministrator.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface IDashboard
    {
        DateTime? SelectedPeriod { get; set; }
        decimal UsdValue { get; set; }
        decimal SalaryArs { get; set; }
        decimal SalaryUsd { get; set; }

        void GrdRefreshData(List<DashboardViewDto> dashboardDtos);
        void ClearInputs();
        void ButtonsLogic();

        //events
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonExitClick;
        event EventHandler GrdDoubleClick;
    }
}
