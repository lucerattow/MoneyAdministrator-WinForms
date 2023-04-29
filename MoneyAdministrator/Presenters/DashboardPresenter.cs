using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities.Disposable;
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
        private IDashboardView _view;
        private string _databasePath;
        private Action _closeView;

        //properties
        public IDashboardView View
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
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
            _view.ButtonExitClick += ButtonExitClick;
            _view.GrdDoubleClick += GrdDoubleClick;
        }

        private void GrdRefreshData()
        {
            using (new CursorWait())
            {
                //Obtengo los servicios
                var transactionDetailService = new TransactionDetailService(_databasePath);
                var currencyValueService = new CurrencyValueService(_databasePath);
                var salaryService = new SalaryService(_databasePath);

                var transactionDetails = transactionDetailService.GetIntermediateDetailDtos().Where(x => x.Concider == true).ToList();

                List<DashboardDto> dtos = new List<DashboardDto>();

                var usdList = currencyValueService.GetAll().OrderByDescending(x => x.Date);
                var salaryList = salaryService.GetAll().OrderByDescending(x => x.Date);

                //Unifico la lista de años
                var allYears = new List<int>();
                allYears.AddRange(transactionDetails.Select(x => x.Date.Year).Distinct().ToList());
                allYears.AddRange(usdList.Select(x => x.Date.Year).Distinct());
                allYears.AddRange(salaryList.Select(x => x.Date.Year).Distinct());

                //Genero los años intermedios si es que faltan
                if (allYears.Count != 0)
                {
                    var initYear = allYears.Min();
                    var endYear = allYears.Max();

                    allYears.Clear();
                    allYears.AddRange(IntTools.GetIntermediateNumbers(initYear, endYear));

                    foreach (var year in allYears.OrderByDescending(x => x))
                    {
                        for (int month = 12; month >= 1; month--)
                        {
                            var monthTransactions = transactionDetails.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();

                            var usd = usdList.Where(x => x.Date <= new DateTime(year, month, 1)).FirstOrDefault();
                            var salaryArs = salaryList.Where(x => x.Date <= new DateTime(year, month, 1) && x.CurrencyId == 1).FirstOrDefault();
                            var salaryUsd = salaryList.Where(x => x.Date <= new DateTime(year, month, 1) && x.CurrencyId == 2).FirstOrDefault();

                            decimal usdValue = usd != null ? usd.Value : 0;
                            decimal salaryArsValue = salaryArs != null ? salaryArs.Value : 0;
                            decimal salaryUsdValue = salaryUsd != null ? salaryUsd.Value : 0;
                            decimal usdSalary = usdValue != 0 ? salaryArsValue / usdValue : 0;

                            decimal assets = monthTransactions.Where(x => x.Amount > 0).Sum(x => x.Amount);
                            decimal passives = monthTransactions.Where(x => x.Amount < 0).Sum(x => x.Amount);

                            dtos.Add(new DashboardDto()
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
                }
                
                this._view.GrdRefreshData(dtos);
            }
        }

        private void InsertRecord(DateTime period)
        {
            InsertCurrency(period);
            InsertSalary(period);
            GrdRefreshData();
        }

        private void InsertCurrency(DateTime period)
        {
            //Inicializo el servico
            var currencyValueService = new CurrencyValueService(_databasePath);

            var currencyValue = currencyValueService.GetByPeriod(period);
            if (currencyValue != null)
            {
                currencyValue.Value = _view.UsdValue;
                currencyValueService.Update(currencyValue);
            }
            else
            {
                currencyValue = new CurrencyValue
                {
                    Date = period,
                    CurrencyId = 2, //USD
                    Value = _view.UsdValue,
                };
                currencyValueService.Insert(currencyValue);
            }
        }

        private void InsertSalary(DateTime period)
        {
            //Inicializo el servicio
            var salaryService = new SalaryService(_databasePath);

            //Salary ARS
            var salaryArs = salaryService.GetByPeriod(period, 1);
            if (salaryArs != null)
            {
                salaryArs.Value = _view.SalaryArs;
                salaryService.Update(salaryArs);
            }
            else
            {
                salaryArs = new Salary
                {
                    Date = period,
                    CurrencyId = 1, //ARS
                    Value = _view.SalaryArs,
                };
                salaryService.Insert(salaryArs);
            }

            //Salary USD
            var salaryUsd = salaryService.GetByPeriod(period, 2);
            if (salaryUsd != null)
            {
                salaryUsd.Value = _view.SalaryUsd;
                salaryService.Update(salaryUsd);
            }
            else
            {
                salaryUsd = new Salary
                {
                    Date = period,
                    CurrencyId = 2, //USD
                    Value = _view.SalaryUsd,
                };
                salaryService.Insert(salaryUsd);
            }
        }

        private void DeleteRecord(DateTime period)
        {
            DeleteCurrency(period);
            DeleteSalary(period);
            GrdRefreshData();
        }

        private void DeleteCurrency(DateTime period)
        {
            //Inicializo el servico
            var currencyValueService = new CurrencyValueService(_databasePath);

            var currencyValue = currencyValueService.GetByPeriod(period);
            if (currencyValue != null)
                currencyValueService.Delete(currencyValue);
        }

        private void DeleteSalary(DateTime period)
        {
            //Inicializo el servicio
            var salaryService = new SalaryService(_databasePath);

            //Salary ARS
            var salaryArs = salaryService.GetByPeriod(period, 1);
            if (salaryArs != null)
                salaryService.Delete(salaryArs);

            //Salary USD
            var salaryUsd = salaryService.GetByPeriod(period, 2);
            if (salaryUsd != null)
                salaryService.Delete(salaryUsd);
        }

        //events
        private void GrdDoubleClick(object sender, EventArgs e)
        {
            using (new CursorWait())
            { 
                var currencyValueService = new CurrencyValueService(_databasePath);
                var salaryService = new SalaryService(_databasePath);

                if (_view.SelectedRecordPeriod == null)
                    return;

                var usd = currencyValueService.GetAll().Where(x => x.Date <= _view.SelectedRecordPeriod.Value).FirstOrDefault();
                var salaryArs = salaryService.GetAll().Where(x => x.Date <= _view.SelectedRecordPeriod.Value && x.CurrencyId == 1).FirstOrDefault();
                var salaryUsd = salaryService.GetAll().Where(x => x.Date <= _view.SelectedRecordPeriod.Value && x.CurrencyId == 2).FirstOrDefault();

                _view.UsdValue = usd != null ? usd.Value : 0;
                _view.SalaryArs = salaryArs != null ? salaryArs.Value : 0;
                _view.SalaryUsd = salaryUsd != null ? salaryUsd.Value : 0;
            }
        }

        private void ButtonInsertClick(object sender, EventArgs e)
        {
            using (new CursorWait())
            {
                InsertRecord(_view.Period);
            }
        }

        private void ButtonUpdateClick(object sender, EventArgs e)
        {
            using (new CursorWait())
            {
                if (_view.SelectedRecordPeriod != null)
                    InsertRecord(_view.SelectedRecordPeriod.Value);
            }
        }

        private void ButtonDeleteClick(object sender, EventArgs e)
        {
            using (new CursorWait())
            {
                if (_view.SelectedRecordPeriod != null)
                    DeleteRecord(_view.SelectedRecordPeriod.Value);
            }
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            _closeView();
        }
    }
}
