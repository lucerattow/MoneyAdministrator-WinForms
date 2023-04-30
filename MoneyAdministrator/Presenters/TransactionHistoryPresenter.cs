using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Services.Interfaces;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views;
using MoneyAdministrator.Views.UserControls;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Windows.Forms.Design;
using static iText.IO.Util.IntHashtable;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MoneyAdministrator.Presenters
{
    public class TransactionHistoryPresenter
    {
        //fields
        private ITransactionHistoryView _view;
        private string _databasePath;
        private Action _closeView;

        //properties
        public ITransactionHistoryView View 
        { 
            get { return _view; } 
        }

        public TransactionHistoryPresenter(string databasePath, Action closeView)
        {
            _databasePath = databasePath;
            _closeView = closeView;
            _view = new TransactionHistoryView();

            AssosiateEvents();
            SetCurrenciesList();
            GrdRefreshData();
        }

        //methods
        private void AssosiateEvents()
        {
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonNewPayClick += ButtonNewPayClick;
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
            _view.ButtonExitClick += ButtonExitClick;
            _view.ButtonEntitySearchClick += ButtonEntitySearchClick;
            _view.GrdDoubleClick += GrdDoubleClick;
            _view.GrdValueChange += GrdValueChange;
        }

        private void SetCurrenciesList()
        {
            using (new CursorWait())
            {
                var service = new CurrencyService(_databasePath);
                var entities = service.GetAll();
                _view.SetCurrenciesList(entities);
            }
        }

        private void GrdRefreshData()
        {
            using (new CursorWait())
            {
                var dtos = new TransactionDetailService(_databasePath).GetIntermediateDetailDtos();
                _view.GrdRefreshData(dtos);
            }
        }

        private void SelectTransaction()
        {
            using (new CursorWait())
            {
                if (_view.SelectedDto is null)
                    return;

                //Relleno los campos
                _view.EntityName = _view.SelectedDto.EntityName;
                _view.Date = _view.SelectedDto.Date;
                _view.Description = _view.SelectedDto.Description;
                _view.Amount = _view.SelectedDto.Amount;
                _view.Currency = new Currency { Name = _view.SelectedDto.CurrencyName };

                //Guardo el tipo de transaccion
                _view.IsInstallment = _view.SelectedDto.TransactionType == TransactionType.Installments;
                _view.IsService = _view.SelectedDto.TransactionType == TransactionType.Service;

                //Relleno si es necesario, los datos de las cuotas
                if (_view.IsInstallment && !string.IsNullOrEmpty(_view.SelectedDto.Installment))
                {
                    var installments = _view.SelectedDto.Installment.Split('/');
                    _view.InstallmentCurrent = IntTools.Convert(installments[0]);
                    _view.InstallmentMax = IntTools.Convert(installments[1]);
                }

                //Relleno si es necesario, los datos del servicio
                if (_view.IsService)
                    _view.Frequency = _view.SelectedDto.Frequency;
            }
        }

        private void UpdateSingle(TransactionDetail detail)
        {
            detail.Date = _view.Date;
            detail.EndDate = _view.Date;
            detail.Amount = _view.Amount;

            new TransactionDetailService(_databasePath).Update(detail);

            //Indico que hay que hacer focus en esta transaccion modificada
            _view.FocusRow = detail.Id;
            GrdRefreshData();
        }

        private void UpdateInstallment(TransactionDetail detail)
        {
            string message = "Al modificar esta cuota se modificarán todas las cuotas relacionadas. ¿Desea continuar?";
            string title = "Actualización de cuotas";

            if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
            {
                //Obtengo el detalle inicial
                var initDetailDate = detail.Transaction.TransactionDetails.OrderBy(x => x.Date).Select(x => x.Date).FirstOrDefault();

                //Obtengo la fecha del detalle actual
                var currentDetailDate = detail.Date;

                //Calculo la diferencia de meses y actualizo la fecha incial de las cuotas
                var difference = DateTimeTools.GetMonthDifference(currentDetailDate, _view.Date);
                var initDate = initDetailDate.AddMonths(difference);

                var dateToCompare = new DateTime(initDate.Year, initDate.Month, initDate.Day > 28 ? 28 : initDate.Day);
                //Actualizo las fechas de cada cuota
                foreach (var details in detail.Transaction.TransactionDetails.OrderBy(x => x.Date))
                {
                    //Calculo la diferencia entre fechas y me dedico a sumar meses a las fechas originales
                    difference = DateTimeTools.GetMonthDifference(details.Date, dateToCompare);

                    var newDate = details.Date.AddMonths(difference);
                    var newEndDate = details.EndDate.AddMonths(difference);
                    newEndDate = new DateTime(newEndDate.Year, newEndDate.Month, dateToCompare.Day);

                    //Actualizo el detalle
                    details.Date = new DateTime(newDate.Year, newDate.Month, dateToCompare.Day);
                    details.EndDate = newEndDate;
                    details.Amount = _view.Amount;
                    new TransactionDetailService(_databasePath).Update(detail);

                    //guardo la nueva fecha inicial de la proxima cuota
                    dateToCompare = newEndDate.AddMonths(1);
                }
            }

            //Indico que hay que hacer focus en esta transaccion modificada
            _view.FocusRow = detail.Id;
            GrdRefreshData();
        }

        private void UpdateService(TransactionDetail detail)
        {
            //Inicializo el servicio
            var transactionDetailService = new TransactionDetailService(_databasePath);

            //Obtengo los objetos a editar
            var allDetails = detail.Transaction.TransactionDetails;
            var current = allDetails.Where(x => x.Date <= _view.Date).OrderByDescending(x => x.Date).FirstOrDefault();
            var futureDetails = allDetails.Where(x => x.Date > _view.Date).ToList();

            _view.FocusRow = detail.Id;
            if (futureDetails.Count == 0)
            {
                //Actualizo el servicio y guardo el ID del detalle que se debe seleccionar
                _view.FocusRow = transactionDetailService.UpdateServiceTransaction(detail, _view.Date, _view.Amount, _view.Frequency, true);
            }
            else
            {
                string message = "Al modificar este servicio, cambiarán todos los futuros vinculados. ¿Confirmas? " +
                    "(\"No\" para actualizar hasta el proximo precio)";
                string title = "Actualización de servicio";

                var dialogResult = CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNoCancel, title);

                if (dialogResult != DialogResult.Cancel)
                {
                    if (dialogResult == DialogResult.Yes)
                        _view.FocusRow = transactionDetailService.UpdateServiceTransaction(detail, _view.Date, _view.Amount, _view.Frequency, true);
                    else if (dialogResult == DialogResult.No)
                        _view.FocusRow = transactionDetailService.UpdateServiceTransaction(detail, _view.Date, _view.Amount, _view.Frequency, false);

                    GrdRefreshData();
                }
            }
        }

        //events
        private void ButtonNewPayClick(object? sender, EventArgs e)
        {
            var summaries = new CCSummaryService(_databasePath).GetAll();
            var detail = new TransactionDetailService(_databasePath).Get(_view.SelectedDto.Id);
            var summary = summaries
                .Where(x => x.TransactionId == detail.TransactionId)
                .FirstOrDefault();

            if (summary is null)
                return;

            var creditCardPayPresenter = new CreditCardPayPresenter(_databasePath, summary.Id);
            creditCardPayPresenter.Show();

            GrdRefreshData();
        }

        private void ButtonInsertClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                try
                {
                    //Inicializo los servicios
                    var transactionService = new TransactionService(_databasePath);
                    var transactionDetailService = new TransactionDetailService(_databasePath);
                    var entityService = new EntityService(_databasePath);

                    //Transaction Inputs
                    var transactionType = TransactionType.Single;
                    var description = _view.Description;
                    var currencyId = _view.Currency.Id;

                    //Details Inputs
                    var date = _view.Date;
                    var amount = _view.Amount;
                    var installments = _view.InstallmentMax;
                    var frequency = _view.Frequency;

                    //Si la entidad no existe la inserto
                    var entity = entityService.GetByName(_view.EntityName);
                    if (entity is null)
                    {
                        entity = new Entity
                        {
                            Name = _view.EntityName,
                            EntityTypeId = 1, //General
                        };
                        entityService.Insert(entity);
                    }

                    //Determino el tipo de transaccion
                    var endDate = _view.Date;
                    if (_view.IsInstallment)
                    {
                        transactionType = TransactionType.Installments;
                        //Se resta 1 ya que la cuota 1 es el mes inicial
                        endDate = _view.Date.AddMonths(_view.InstallmentMax - 1);
                    }
                    else if (_view.IsService)
                    {
                        transactionType = TransactionType.Service;
                        endDate = DateTime.MaxValue;
                    }

                    var dto = new TransactionDetailDto
                    {
                        EntityId = entity.Id,
                        CurrencyId = _view.Currency.Id,
                        TransactionType = transactionType,
                        Description = _view.Description,
                        //details
                        Date = _view.Date,
                        EndDate = endDate,
                        Amount = _view.Amount,
                        Frequency = _view.Frequency,
                    };
                    int id = transactionService.CreateTransaction(dto);

                    //Indico que hay que hacer focus en esta transaccion recien creada
                    _view.FocusRow = id;
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                GrdRefreshData();
            }
        }

        private void ButtonUpdateClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                try
                {
                    //Inicializo los servicios
                    var transactionService = new TransactionService(_databasePath);
                    var transactionDetailService = new TransactionDetailService(_databasePath);
                    var entityService = new EntityService(_databasePath);

                    //Compruebo que el transactionDetail existe
                    var detail = transactionDetailService.Get(_view.SelectedDto.Id);
                    if (detail == null)
                    {
                        CommonMessageBox.errorMessageShow("La transacción seleccionada no existe", MessageBoxButtons.OK);
                        return;
                    }

                    //Transaction Inputs
                    var transactionType = _view.SelectedDto.TransactionType;
                    var description = _view.Description;
                    var currencyId = _view.Currency.Id;

                    //Si la entidad no existe la inserto
                    var entity = entityService.GetByName(_view.EntityName);
                    if (entity is null)
                    {
                        entity = new Entity
                        {
                            Name = _view.EntityName,
                            EntityTypeId = 1, //General
                        };
                        entityService.Insert(entity);
                    }

                    //Modifico la transaccion
                    detail.Transaction.EntityId = entity.Id;
                    detail.Transaction.CurrencyId = currencyId;
                    detail.Transaction.Description = description;
                    transactionDetailService.Update(detail);

                    //Dependiendo el tipo de transaccion, la modifico de forma diferente
                    if (transactionType == TransactionType.Single)
                        UpdateSingle(detail);
                    else if (transactionType == TransactionType.Installments)
                        UpdateInstallment(detail);
                    else if (transactionType == TransactionType.Service)
                        UpdateService(detail);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
            }
        }

        private void ButtonDeleteClick(object? sender, EventArgs e)
        {
            if (_view.SelectedDto is null)
                return;

            using (new CursorWait())
            {
                try
                {
                    //Inicializo los servicios
                    var transactionService = new TransactionService(_databasePath);
                    var transactionDetailService = new TransactionDetailService(_databasePath);
                    var creditCardSummaryService = new CCSummaryService(_databasePath);

                    //Compruebo que el transactionDetail existe
                    var detail = transactionDetailService.Get(_view.SelectedDto.Id);
                    if (detail is null)
                    {
                        CommonMessageBox.errorMessageShow("La transacción seleccionada ya ha sido eliminada", MessageBoxButtons.OK);
                        return;
                    }

                    var allDetails = detail.Transaction.TransactionDetails.ToList();

                    //Elimino transaccion unica
                    if (_view.SelectedDto.TransactionType == TransactionType.Single)
                    {
                        //Elimino el detalle
                        transactionDetailService.Delete(detail);
                    }
                    //Elimino una transaccion en cuotas
                    else if (_view.SelectedDto.TransactionType == TransactionType.Installments)
                    {
                        string message = "Al eliminar esta cuota, también se eliminarán todas las cuotas relacionadas. ¿Desea continuar con la eliminación?";
                        string title = "Confirmar eliminación de cuotas";

                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                            foreach (var td in allDetails)
                                transactionDetailService.Delete(td);
                    }
                    //Elimino un servicio
                    else if (_view.SelectedDto.TransactionType == TransactionType.Service)
                    {
                        string message = "Al eliminar este servicio, se dará por finalizado y se eliminarán " +
                            "tanto la transacción seleccionada como las futuras transacciones. ¿Desea continuar con la eliminación?";
                        string title = "Confirmar finalización de servicio";

                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                        {
                            var date = _view.SelectedDto.Date;
                            var current = allDetails.Where(x => x.Date.Date <= _view.SelectedDto.Date.Date && x.EndDate.Date >= _view.SelectedDto.Date.Date).FirstOrDefault();

                            if (current.Date == date)
                            {
                                transactionDetailService.Delete(current);
                            }
                            else
                            {
                                current.EndDate = date.AddMonths(-1);
                                transactionDetailService.Update(current);
                            }

                            //elimino detalles futuros
                            foreach (var futureDetail in allDetails.Where(x => x.Date > _view.SelectedDto.Date).ToList())
                                transactionDetailService.Delete(futureDetail);
                        }
                    }
                    else if (_view.SelectedDto.TransactionType == TransactionType.CreditCardOutstanding)
                    {
                        //Compruebo si la transaccion esta asociada a una tarjeta de credito
                        var summary = creditCardSummaryService.GetAll().Where(x => x.TransactionId == _view.SelectedDto.Id).FirstOrDefault();
                        if (summary != null)
                        {
                            string descripcion = $"{summary.CreditCard.CreditCardBrand.Name} *{summary.CreditCard.LastFourNumbers}";
                            string message = $"Al eliminar este servicio, también se eliminará el resumen de tarjeta de crédito relacionado " +
                                $"({descripcion} : {_view.SelectedDto.Date.ToString("yyyy-MM")}). ¿Desea continuar con la eliminación?";
                            string title = "Confirmar eliminación de cuotas";

                            if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.No)
                                return;
                        }

                        //Elimino el detalle
                        transactionDetailService.Delete(detail);
                    }
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }

                GrdRefreshData();
            }
        }

        private void ButtonExitClick(object? sender, EventArgs e)
        {
            _closeView();
        }

        private void ButtonEntitySearchClick(object? sender, EventArgs e)
        {
            var selectedId = new EntityPresenter(_databasePath).Show();
            var entity = new EntityService(_databasePath).Get(selectedId);
            if (entity != null)
                _view.EntityName = entity.Name;
        }

        private void GrdDoubleClick(object? sender, EventArgs e)
        {
            SelectTransaction();
        }

        private void GrdValueChange(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                if (_view.CheckBoxChangeDto is null)
                    return;

                //Inicializo los servicios
                var detailsService = new TransactionDetailService(_databasePath);

                var detail = detailsService.Get(_view.CheckBoxChangeDto.Id);
                var type = detail.Transaction.TransactionType;

                //Si es una transaccion simple o de tarjeta de credito
                if (type == TransactionType.Single || type == TransactionType.CreditCardOutstanding)
                {
                    detail.Concider = _view.CheckBoxChangeDto.Concider;
                    detail.Paid = _view.CheckBoxChangeDto.Paid;

                    detailsService.Update(detail);
                }
                //Si es una transaccion servicio o en cuotas
                else if (type == TransactionType.Service || type == TransactionType.Installments)
                {
                    detailsService.UpdateCheckBox(detail, _view.CheckBoxChangeDto.Date, _view.CheckBoxChangeDto.Concider, _view.CheckBoxChangeDto.Paid);
                }
            }
        }
    }
}
