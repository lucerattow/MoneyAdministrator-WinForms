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
        //properties
        /// <summary>Obtengo el periodo seleccionado</summary>
        DateTime? SelectedPeriod { get; set; }
        /// <summary>Obtengo el valor del usd en este periodo</summary>
        decimal UsdValue { get; set; }
        /// <summary>Obtengo el sueldo en ARS en este periodo</summary>
        decimal SalaryArs { get; set; }
        /// <summary>Obtengo el sueldo en USD en este periodo</summary>
        decimal SalaryUsd { get; set; }

        //Methods
        /// <summary>Envio los datos de resumen al GRD del dashboard</summary>
        /// <param name="dashboardDtos">DTOs para rellenar la GRD del dashboard</param>
        void GrdRefreshData(List<DashboardViewDto> dashboardDtos);
        /// <summary>Limpio los campos del formulario</summary>
        void ClearInputs();
        /// <summary>Ejecuto la logica de botones</summary>
        void ButtonsLogic();

        //events
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonExitClick;
        event EventHandler GrdDoubleClick;
    }
}
