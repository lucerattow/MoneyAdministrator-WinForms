using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class CreditCardPayPresenter
    {
        private readonly string _databasePath;
        private readonly int _summaryId;
        private ICreditCardPayView _view;

        public CreditCardPayPresenter(string databasePath, int summaryId)
        {
            using (new CursorWait())

            _databasePath = databasePath;
            _summaryId = summaryId;
            _view = new CreditCardPayView();

            //Busco el resumen
            var ccSummaryService = new CCSummaryService(_databasePath);
            var ccSummary = ccSummaryService.Get(summaryId);
            if (ccSummary != null)
            {
                _view.TransactionOutstandingId = ccSummary.Transaction.Id;

                _view.PayDay = ccSummary.Period;
                _view.CreditCardDescription = $"{ccSummary.CreditCard.Entity.Name} {ccSummary.CreditCard.CreditCardBrand.Name} :" +
                    $" ●●●● ●●●● ●●●● {ccSummary.CreditCard.LastFourNumbers}";
            }

            AssosiateEvents();
            GrdRefreshData();
        }

        //methods
        public void Show()
        {
            if (_view == null)
                throw new Exception("Ocurrio un error al intentar abrir el popup");

            _view.ShowDialog();
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
                var ccSummaryService = new CCSummaryService(_databasePath);

                //Obtengo el resumen
                var ccSummary = ccSummaryService.Get(_summaryId);
                if (ccSummary == null)
                    return;

                var transactionOutstanding = transactionService.Get(_view.TransactionOutstandingId);

                if (ccSummary.TransactionPayId == 0)
                {
                    var descripcion = $"{ccSummary.CreditCard.CreditCardBrand.Name} - *{ccSummary.CreditCard.LastFourNumbers} :: Pago realizado";
                    var transaction = new Transaction
                    {
                        EntityId = transactionOutstanding.EntityId,
                        CurrencyId = transactionOutstanding.CurrencyId,
                        Description = descripcion,
                    };
                    transactionService.Insert(transaction);

                    //Asigno la transaccion de pago al resumen
                    ccSummary.TransactionPayId = transaction.Id;
                    ccSummaryService.Update(ccSummary);
                }

                var transactionDetail = new TransactionDetail
                {
                    TransactionId = (int)ccSummary.TransactionPayId,
                    Date = _view.PayDay,
                    Amount = _view.AmountPay,
                    Installment = 0,
                    Frequency = 0,
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
