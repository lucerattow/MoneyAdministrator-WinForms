using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Services.Controllers;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MoneyAdministrator.Presenters
{
    public class CreditCardPayPresenter
    {
        private readonly string _databasePath;
        private int _summaryId;
        private ICreditCardPayView _view;
        private CreditCardPayController _controller;

        public CreditCardPayPresenter(string databasePath)
        {
            using (new CursorWait())

            _databasePath = databasePath;
            _controller = new CreditCardPayController(databasePath);
            _view = new CreditCardPayView();
        }

        private void Initialize()
        {
            AssosiateEvents();
            GrdRefreshData();
        }

        //methods
        public static void Show(string _databasePath, int summaryId)
        {
            var presenter = new CreditCardPayPresenter(_databasePath);
            var summary = presenter._controller.GetSummaryById(summaryId);

            if (summary is null)
                return;

            //Establezco los valores
            presenter._view.TransactionOutstandingId = summary.TransactionId;
            presenter._view.PayDay = summary.Period;
            presenter._view.CreditCardDescription = $"{summary.CreditCard.Entity.Name} {summary.CreditCard.CreditCardBrand.Name} :" +
                $" ●●●● ●●●● ●●●● {summary.CreditCard.LastFourNumbers}";

            //Asigno el id del resumen
            presenter._summaryId = summary.Id;

            presenter.Initialize();
            presenter._view.ShowDialog();
        }

        private void AssosiateEvents()
        {
            _view.GrdDoubleClick += GrdDoubleClick;
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
        }

        private void GrdRefreshData()
        {
            using (new CursorWait())
            {
                //Inicializo los servicios
                var transactionDetailService = new TransactionDetailService(_databasePath);
                var ccSummaryService = new CCSummaryService(_databasePath);

                var ccSummary = ccSummaryService.Get(_summaryId);
                if (ccSummary == null)
                    return;

                var transactions = transactionDetailService.GetAll().Where(x => x.TransactionId == ccSummary.TransactionId).ToList()
                    .Union(transactionDetailService.GetAll().Where(x => x.TransactionId == ccSummary.TransactionPayId).ToList());

                List<CreditCardPayDto> dtos = new List<CreditCardPayDto>();
                foreach (var transactionDetail in transactions.OrderByDescending(x => x.Date.Day).ThenBy(x => x.Transaction.Description))
                {
                    dtos.Add(new CreditCardPayDto
                    { 
                        Id = transactionDetail.Id,
                        Date = transactionDetail.Date,
                        EntityName = transactionDetail.Transaction.Entity.Name,
                        Description = transactionDetail.Transaction.Description,
                        AmountArs = transactionDetail.Amount,
                    });
                }
                this._view.GrdRefreshData(dtos, ccSummary.Period);
            }
        }

        private void SelectTransaction()
        {
            using (new CursorWait())
            {
                //Inicializo los servicios
                var transactionDetailService = new TransactionDetailService(_databasePath);

                //Obtengo la transaccion
                var transactionDetail = transactionDetailService.Get(_view.SelectedTransactionDetail);

                //Evito seleccionar la transaccion de saldo total
                if (transactionDetail.TransactionId == _view.TransactionOutstandingId)
                {
                    _view.SelectedTransactionDetail = 0;
                    return;
                }

                _view.AmountPay = transactionDetail.Amount;

                //Invierto el valor para poder pagar el saldo pendiente
                if (_view.SelectedTransactionDetail == _view.TransactionOutstandingId)
                    _view.AmountPay = _view.AmountPay * -1;
            }
        }

        //events
        private void GrdDoubleClick(object? sender, EventArgs e)
        {
            SelectTransaction();
        }

        private void ButtonInsertClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                //Inicializo los servicios
                var transactionService = new TransactionService(_databasePath);
                var summaryService = new CCSummaryService(_databasePath);

                //Obtengo el resumen
                var summary = summaryService.Get(_summaryId);
                if (summary == null)
                    return;

                var transactionOutstanding = transactionService.Get(_view.TransactionOutstandingId);

                if (summary.TransactionPayId == 0)
                {
                    var descripcion = $"{summary.CreditCard.CreditCardBrand.Name} - *{summary.CreditCard.LastFourNumbers} :: Pago realizado";
                    var transaction = new Transaction
                    {
                        TransactionType = TransactionType.CreditCardOutstanding,
                        EntityId = transactionOutstanding.EntityId,
                        CurrencyId = transactionOutstanding.CurrencyId,
                        Description = descripcion,
                    };
                    transactionService.Insert(transaction);

                    //Asigno la transaccion de pago al resumen
                    summary.TransactionPayId = transaction.Id;
                    summaryService.Update(summary);
                }

                var transactionDetail = new TransactionDetail
                {
                    TransactionId = (int)summary.TransactionPayId,
                    Date = _view.PayDay,
                    EndDate = _view.PayDay,
                    Amount = _view.AmountPay,
                    Frequency = 1,
                    Concider = true,
                    Paid = false,
                };
                new TransactionDetailService(_databasePath).Insert(transactionDetail);

                GrdRefreshData();
            }
        }

        private void ButtonUpdateClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                //Inicializo los servicios
                var transactionDetailService = new TransactionDetailService(_databasePath);

                var detail = transactionDetailService.Get(_view.SelectedTransactionDetail);
                detail.Date = _view.PayDay;
                detail.Amount = _view.AmountPay;

                transactionDetailService.Update(detail);
                GrdRefreshData();
            }
        }

        private void ButtonDeleteClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                //Inicializo los servicios
                var transactionDetailService = new TransactionDetailService(_databasePath);

                var detail = transactionDetailService.Get(_view.SelectedTransactionDetail);
                transactionDetailService.Delete(detail);

                GrdRefreshData();
            }
        }
    }
}
