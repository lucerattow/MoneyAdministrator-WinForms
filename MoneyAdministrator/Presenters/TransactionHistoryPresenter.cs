using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Services.Interfaces;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms.Design;
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
            _view.GrdDoubleClick += GrdDoubleClick;
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
            _view.ButtonExitClick += ButtonExitClick;
            _view.SelectedYearChange += SelectedYearChange;
            _view.ButtonEntitySearchClick += ButtonEntitySearchClick;
        }

        private void SetCurrenciesList()
        {
            using (new CursorWait())
            {
                var service = new CurrencyService(_databasePath);
                var entities = service.GetAll();
                this._view.SetCurrenciesList(entities);
            }
        }

        private void GrdRefreshData()
        {
            using (new CursorWait())
            {
                var transactionDetailService = new TransactionDetailService(_databasePath);
                var transactionDetails = transactionDetailService.GetAll();

                List<TransactionViewDto> dtos = new List<TransactionViewDto>();
                foreach (var transactionDetail in transactionDetails)
                {
                    //Obtengo el valor maximo de la propiedad installment para esta transaccion
                    int maxInstallment = transactionDetails
                        .Where(x => x.TransactionId == transactionDetail.TransactionId)
                        .Max(x => x.Installment);

                    //Genero el string "1 / 6" que indica el numero de cuotas
                    string installments = maxInstallment > 1 ? $"{transactionDetail.Installment} / {maxInstallment}" : "";

                    dtos.Add(new TransactionViewDto()
                    {
                        Id = transactionDetail.Id,
                        Date = transactionDetail.Date,
                        EntityName = transactionDetail.Transaction.Entity.Name,
                        Description = transactionDetail.Transaction.Description,
                        Installment = installments,
                        CurrencyName = transactionDetail.Transaction.Currency.Name,
                        Amount = transactionDetail.Amount
                    });
                }
                this._view.GrdRefreshData(dtos);
            }
        }

        private void SelectTransaction()
        {
            using (new CursorWait())
            {
                var entity = new TransactionDetailService(_databasePath).Get(_view.SelectedId);

                _view.EntityName = entity.Transaction.Entity.Name;
                _view.Date = entity.Date;
                _view.Description = entity.Transaction.Description;
                _view.Amount = entity.Amount;
                _view.Currency = entity.Transaction.Currency;
                _view.InstallmentCurrent = entity.Installment;
                _view.InstallmentMax = entity.Transaction.TransactionDetails.Max(t => t.Installment);
                _view.IsService = entity.Frequency > 0;
                _view.Frequency = entity.Frequency;

                _view.Editing = true;
            }
        }

        private bool IsService()
        {
            return _view.IsService;
        }

        private bool IsInstallment()
        {
            return _view.InstallmentMax > 1;
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
                try
                {
                    //Inicializo los servicios
                    var transactionService = new TransactionService(_databasePath);
                    var transactionDetailService = new TransactionDetailService(_databasePath);
                    var entityService = new EntityService(_databasePath);

                    //Obtengo los valores
                    var entity = new Entity() { Name = _view.EntityName };
                    var date = _view.Date;
                    var description = _view.Description;
                    var amount = _view.Amount;
                    var currencyId = _view.Currency.Id;
                    var installments = _view.InstallmentMax;
                    var frequency = _view.Frequency;
                    var isService = _view.IsService;

                    //Compruebo si la entidad ya existe, si no existe la inserto
                    var searchEntity = entityService.GetByName(entity.Name);
                    if (searchEntity == null)
                        entityService.Insert(entity);
                    else
                        entity.Id = searchEntity.Id;

                    //Inserto la transaccion
                    var transaction = new Transaction()
                    {
                        EntityId = entity.Id,
                        CurrencyId = currencyId,
                        Description = description,
                    };
                    transactionService.Insert(transaction);

                    //Si es transaccion unica
                    if (!IsService() && !IsInstallment())
                    {
                        //Inserto 1 transaccionDetails
                        transactionDetailService.GenerateTransactionDetails(transaction.Id, date, amount, 1, frequency, isService)
                            .ForEach(x => transactionDetailService.Insert(x));
                    }
                    //Si es transaccion en cuotas
                    else if (IsInstallment())
                    {
                        transactionDetailService.GenerateTransactionDetails(transaction.Id, date, amount, installments, frequency, isService)
                            .ForEach(x => transactionDetailService.Insert(x));
                    }
                    //Si es transaccion de servicio
                    else if (IsService())
                    {
                        var monthsPeriod = frequency > 3 ? 24 : 12;
                        transactionDetailService.GenerateTransactionDetails(transaction.Id, date, amount, monthsPeriod, frequency, isService)
                            .ForEach(x => transactionDetailService.Insert(x));
                    }
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
                    var transactionDetail = transactionDetailService.Get(_view.SelectedId);
                    if (transactionDetail == null)
                    {
                        CommonMessageBox.errorMessageShow("La transacción seleccionada no existe", MessageBoxButtons.OK);
                        return;
                    }

                    //Obtengo los objetos a editar
                    var transactionDetails = transactionDetail.Transaction.TransactionDetails;

                    //Obtengo los valores
                    var entity = new Entity() { Name = _view.EntityName };
                    var date = _view.Date;
                    var description = _view.Description;
                    var amount = _view.Amount;
                    var currencyId = _view.Currency.Id;
                    var installments = _view.InstallmentMax;
                    var frequency = _view.Frequency;
                    var isService = _view.IsService;

                    //Compruebo si la entidad ya existe, si no existe la inserto
                    var searchEntity = entityService.GetByName(entity.Name);
                    if (searchEntity == null)
                        entityService.Insert(entity);
                    else
                        entity = searchEntity;

                    //Modifico la transaccion
                    transactionDetail.Transaction.EntityId = entity.Id;
                    transactionDetail.Transaction.CurrencyId = currencyId;
                    transactionDetail.Transaction.Description = description;
                    transactionDetailService.Update(transactionDetail);

                    //Si es transaccion unica
                    if (!IsService() && !IsInstallment())
                    {
                        transactionDetail.Date = date;
                        transactionDetail.Amount = amount;

                        transactionDetailService.Update(transactionDetail);
                    }
                    //Si es transaccion en cuotas
                    else if (IsInstallment())
                    {
                        string message = "Al modificar esta cuota, también se modificarán todas las cuotas relacionadas. ¿Desea continuar con la modificación?";
                        string title = "Confirmar modificación de cuotas";

                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                            transactionDetail.Transaction.TransactionDetails.ToList()
                            .ForEach(x =>
                            {
                                x.Date = new DateTime(x.Date.Year, x.Date.Month, date.Day);
                                x.Amount = amount;
                                transactionDetailService.Update(x);
                            });
                    }
                    //Si es transaccion de servicio
                    else if (IsService())
                    {
                        string message = "Al modificar esta transacción de servicio, se modificará la transacción seleccionada y se insertarán nuevas transacciones a futuro relacionadas. ¿Desea continuar con la modificación?";
                        string title = "Confirmar modificación de servicio";

                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                        {
                            //Elimino desde la transaccion actual en adelante
                            transactionDetailService.GetAll()
                            .Where(x => x.TransactionId == transactionDetail.Transaction.Id && x.Id >= transactionDetail.Id).ToList()
                            .ForEach(x => transactionDetailService.Delete(x));

                            var monthsPeriod = frequency > 3 ? 24 : 12;
                            transactionDetailService.GenerateTransactionDetails(
                                transactionDetail.Transaction.Id, date, amount, monthsPeriod, frequency, isService)
                                .ToList().ForEach(x => transactionDetailService.Insert(x));
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                GrdRefreshData();
            }
        }

        private void ButtonDeleteClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                try
                {
                    //Inicializo los servicios
                    var transactionService = new TransactionService(_databasePath);
                    var transactionDetailService = new TransactionDetailService(_databasePath);

                    //Compruebo que el transactionDetail existe
                    var transactionDetail = transactionDetailService.Get(_view.SelectedId);
                    if (transactionDetail == null)
                    {
                        CommonMessageBox.errorMessageShow("La transacción seleccionada ya ha sido eliminada", MessageBoxButtons.OK);
                        return;
                    }

                    var transaction = transactionDetail.Transaction;
                    var transactionDetails = transactionDetail.Transaction.TransactionDetails;

                    //Elimino transaccion unica
                    if (transactionDetails.Count == 1)
                    {
                        transactionDetailService.Delete(transactionDetails.First());
                    }
                    //Elimino una transaccion en cuotas
                    else if (transactionDetails.Max(x => x.Installment) > 1)
                    {
                        string message = "Al eliminar esta cuota, también se eliminarán todas las cuotas relacionadas. ¿Desea continuar con la eliminación?";
                        string title = "Confirmar eliminación de cuotas";

                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                            foreach (var td in transactionDetails)
                                transactionDetailService.Delete(td);
                    }
                    //Elimino un servicio
                    else if (transactionDetails.Max(x => x.Frequency) > 0)
                    {
                        string message = "Al eliminar esta transacción de servicio, se dará por finalizado el servicio y se eliminarán " +
                            "tanto la transacción seleccionada como las futuras transacciones relacionadas. ¿Desea continuar con la eliminación?";
                        string title = "Confirmar finalización de servicio";

                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                        {
                            foreach (var td in transactionDetails.Where(x => x.Id >= transactionDetail.Id).ToList())
                                transactionDetailService.Delete(td);

                            if (transactionDetails.Where(x => x.Id < transactionDetail.Id).ToList().Count == 0)
                                transactionService.Delete(transaction);
                        }
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

        private void SelectedYearChange(object? sender, EventArgs e)
        {
            GrdRefreshData();
        }

        private void ButtonEntitySearchClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                var entities = new EntityService(_databasePath).GetAll();
                var resultPickerViewDtos = entities.Select(entity => new ResultPickerViewDto
                {
                    Id = entity.Id,
                    Field1 = entity.Name
                }).ToList();

                var selectedId = new ResultPickerPresenter(resultPickerViewDtos).Show();

                if (selectedId > 0)
                    _view.EntityName = entities.Where(x => x.Id == selectedId).FirstOrDefault().Name;
            }
        }
    }
}
