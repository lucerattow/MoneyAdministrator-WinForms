using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class DashboardPresenter
    {
        //fields
        private IDashboard _view;
        private string _databasePath;
        private Action _closeView;

        //properties
        public IDashboard View
        {
            get { return _view; }
        }

        public DashboardPresenter(string databasePath, Action closeView)
        {
            _view = new DashboardView();
            _databasePath = databasePath;
            _closeView = closeView;
            GrdRefreshData();
            AssosiateEvents();
        }

        //methods
        private void AssosiateEvents()
        {
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonExitClick += ButtonExitClick;
            _view.GrdDoubleClick += GrdDoubleClick;
        }

        private void GrdRefreshData()
        {
            //Obtengo los servicios
            var transactionDetailService = new TransactionDetailService(_databasePath);
            var currencyValueService = new CurrencyValueService(_databasePath);
            var salaryService = new SalaryService(_databasePath);

            var transactionDetails = transactionDetailService.GetAll();

            List<DashboardViewDto> dtos = new List<DashboardViewDto>();

            var usdList = currencyValueService.GetAll().OrderByDescending(x => x.Date);
            var salaryList = salaryService.GetAll().OrderByDescending(x => x.Date);

            var yearsList = transactionDetails.Select(x => x.Date.Year).Distinct().ToList();
            foreach (var year in yearsList.OrderByDescending(x => x))
            {
                for (int month = 12; month >= 1; month--)
                {
                    var monthTransactions = transactionDetails.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();

                    //Aca tengo que obtener el usd y salary, pero no por periodo, sino obtener todos donde el valor del periodo sea igual o anterior al que toca ahora
                    //luego tomar al valor mas reciente
                    //Al actualizar el precio solo consultar si ya existe un valor para ese periodo, si es asi editarlo, sino, crear uno nuevo,
                    //podria implementar esto para los servicios

                    var usd = usdList.Where(x => x.Date <= new DateTime(year, month, 1)).FirstOrDefault();
                    var salaryArs = salaryList.Where(x => x.Date <= new DateTime(year, month, 1) && x.CurrencyId == 1).FirstOrDefault();
                    var salaryUsd = salaryList.Where(x => x.Date <= new DateTime(year, month, 1) && x.CurrencyId == 2).FirstOrDefault();

                    decimal usdValue = usd != null ? usd.Value : 0;
                    decimal salaryArsValue = salaryArs != null ? salaryArs.Value : 0;
                    decimal salaryUsdValue = salaryUsd != null ? salaryUsd.Value : 0;
                    decimal usdSalary = usdValue != 0 ? salaryArsValue / usdValue : 0;

                    decimal assets = monthTransactions.Where(x => x.Amount > 0).Sum(x => x.Amount);
                    decimal passives = monthTransactions.Where(x => x.Amount < 0).Sum(x => x.Amount);
                    dtos.Add(new DashboardViewDto()
                    {
                        Period = new DateTime(year, month, 1),
                        UsdValue = usdValue,
                        UsdSalary = usdSalary,
                        SalaryArs = salaryArsValue,
                        SalaryUsd = salaryUsdValue,
                        Assets = assets,
                        Passives = passives,
                        Balance = salaryArsValue + assets + passives,
                    });
                }
            }

            this._view.GrdRefreshData(dtos);
        }

        //events
        private void GrdDoubleClick(object sender, EventArgs e)
        {
            var currencyValueService = new CurrencyValueService(_databasePath);
            var salaryService = new SalaryService(_databasePath);

            if (_view.SelectedPeriod == null)
                return;

            var usd = currencyValueService.GetAll().Where(x => x.Date <= _view.SelectedPeriod.Value).FirstOrDefault();
            var salaryArs = salaryService.GetAll().Where(x => x.Date <= _view.SelectedPeriod.Value && x.CurrencyId == 1).FirstOrDefault();
            var salaryUsd = salaryService.GetAll().Where(x => x.Date <= _view.SelectedPeriod.Value && x.CurrencyId == 2).FirstOrDefault();

            _view.UsdValue = usd != null ? usd.Value : 0;
            _view.SalaryArs = salaryArs != null ? salaryArs.Value : 0;
            _view.SalaryUsd = salaryUsd != null ? salaryUsd.Value : 0;
        }

        private void ButtonUpdateClick(object sender, EventArgs e)
        {
            var currencyValueService = new CurrencyValueService(_databasePath);
            var salaryService = new SalaryService(_databasePath);

            if (_view.SelectedPeriod == null)
                return;

            var currencyValue = currencyValueService.GetByPeriod(_view.SelectedPeriod.Value);
            if (currencyValue != null)
            {
                currencyValue.Value = _view.UsdValue;
                currencyValueService.Update(currencyValue);
            }
            else
            {
                currencyValue = new CurrencyValue
                {
                    Date = (DateTime)_view.SelectedPeriod,
                    CurrencyId = 2, //USD
                    Value = _view.UsdValue,
                };
                currencyValueService.Insert(currencyValue);
            }

            //Salary ARS
            var salaryArs = salaryService.GetByPeriod(_view.SelectedPeriod.Value, 1);
            if (salaryArs != null)
            {
                salaryArs.Value = _view.SalaryArs;
                salaryService.Update(salaryArs);
            }
            else
            {
                salaryArs = new Salary
                {
                    Date = (DateTime)_view.SelectedPeriod,
                    CurrencyId = 1, //ARS
                    Value = _view.SalaryArs,
                };
                salaryService.Insert(salaryArs);
            }

            //Salary USD
            var salaryUsd = salaryService.GetByPeriod(_view.SelectedPeriod.Value, 2);
            if (salaryUsd != null)
            {
                salaryUsd.Value = _view.SalaryUsd;
                salaryService.Update(salaryUsd);
            }
            else
            {
                salaryUsd = new Salary
                {
                    Date = (DateTime)_view.SelectedPeriod,
                    CurrencyId = 2, //USD
                    Value = _view.SalaryUsd,
                };
                salaryService.Insert(salaryUsd);
            }

            GrdRefreshData();
            _view.ClearInputs();
            _view.ButtonsLogic();
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            _closeView();
        }
    }
}
