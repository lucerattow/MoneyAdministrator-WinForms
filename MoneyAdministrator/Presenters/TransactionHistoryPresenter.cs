using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.DTOs.Views;
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Services.Controllers;
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
        private TransactionHistoryController _controller;
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
            _controller = new TransactionHistoryController(databasePath);
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
                _view.GrdRefreshData(_controller.GetIntermediateDetailDtos());
            }
        }

        private void GrdInsertRows(int detailId)
        {
            var dto = _controller.GetIntermediateDetailDtos().Where(x => x.Id == detailId).ToList();

            if (dto is null)
                throw new Exception("No es posible mostrar el nuevo detalle, intente recargar la pestaña");
            else
            {
                _view.GrdInsertRows(dto);
            }
        }

        private void GrdUpdateValue(List<TransactionHistoryDto> dto, DateTime date)
        {
            if (dto is null)
                throw new Exception("No es posible mostrar el detalle modificado, intente recargar la pestaña");
            else
            {
                if (dto.Count > 0 && dto[0].TransactionType == TransactionType.Service)
                {
                    dto = dto.Where(x => x.Date >= date).ToList();
                }
                _view.GrdInsertRows(dto);
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

        //events
        private void ButtonNewPayClick(object? sender, EventArgs e)
        {
            if (_view.SelectedDto is null)
                return;

            //Obtengo los datos del resumen para actualizar la grilla
            var summary = _controller.GetCCSummaryByTrxId(_view.SelectedDto.TransactionId);
            if (summary is null)
                return;

            //Guardo la transaccion de pago por las dudas de que sea eliminada
            var transactionId = summary.TransactionId;
            var transactionPayId = summary.TransactionPayId;

            //Muestro la ventana de pagos (para modificar los datos)
            CreditCardPayPresenter.Show(_databasePath, summary.Id);

            //Elimino los registros
            _view.GrdDeleteSelected(transactionId);
            _view.GrdDeleteSelected(transactionPayId);

            //Actualizo el resumen para obtener el nuevo id de pago
            summary = _controller.GetCCSummaryByTrxId(_view.SelectedDto.TransactionId);
            if (summary is null)
                return;

            //Refresco el id de pago para insertarlo
            transactionPayId = summary.TransactionPayId;

            //Inserto los nuevos datos de ser necesario
            var dtos = _controller.GetIntermediateDetailDtos().Where(x => x.TransactionId == transactionId).ToList();
            if (dtos.Count > 0)
                GrdUpdateValue(dtos, DateTime.Now);

            dtos = _controller.GetIntermediateDetailDtos().Where(x => x.TransactionId == transactionPayId).ToList();
            if (dtos.Count > 0)
                GrdUpdateValue(dtos, DateTime.Now);
        }

        private void ButtonInsertClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                try
                {
                    var type = _view.IsService ? TransactionType.Service : (_view.IsInstallment ? TransactionType.Installments : TransactionType.Single);
                    var newDetail = new TransactionHistoryDto
                    {
                        TransactionType = type,
                        EntityName = _view.EntityName,
                        CurrencyName = _view.Currency.Name,
                        Description = _view.Description,
                        Date = _view.Date,
                        Amount = _view.Amount,
                        Frequency = _view.Frequency,
                    };
                    var id = _controller.InsertNewTransaction(newDetail, _view.InstallmentMax);
                    GrdInsertRows(id);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
            }
        }

        private void ButtonUpdateClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                try
                {
                    //Guardo la fecha para graficar el update de servicios
                    var saveCurrentDate = _view.SelectedDto.Date;

                    //Compruebo que el transactionDetail existe
                    var detail = _controller.GetDetailById(_view.SelectedDto.Id);
                    if (detail == null)
                    {
                        CommonMessageBox.errorMessageShow("La transacción seleccionada no existe", MessageBoxButtons.OK);
                        return;
                    }

                    //Modifico la vista con los nuevos valores
                    detail.EntityName = _view.EntityName;
                    detail.CurrencyName = _view.Currency.Name;
                    detail.Description = _view.Description;
                    detail.Date = _view.Date;
                    detail.Amount = _view.Amount;
                    detail.Frequency = _view.Frequency;
                    var modifiedDetailId = -1;

                    //Dependiendo el tipo de transaccion, la modifico de forma diferente
                    var transactionType = _view.SelectedDto.TransactionType;
                    if (transactionType == TransactionType.Single)
                    {
                        modifiedDetailId = _controller.UpdateTransaction(detail);
                    }
                    else if (transactionType == TransactionType.Installments)
                    {
                        string message = "Al modificar esta cuota se modificarán todas las cuotas relacionadas. ¿Desea continuar?";
                        string title = "Actualización de cuotas";
                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                        {
                            modifiedDetailId = _controller.UpdateTransaction(detail);
                        }
                        else
                            return;
                    }
                    else if (transactionType == TransactionType.Service)
                    {
                        //Obtengo los objetos a editar
                        var allDetails = _controller.GetDetailModelById(detail.Id).Transaction.TransactionDetails;
                        var current = allDetails.Where(x => x.Date <= _view.Date).OrderByDescending(x => x.Date).FirstOrDefault();
                        var futureDetails = allDetails.Where(x => x.Date > _view.Date).ToList();
                        if (futureDetails.Count == 0)
                        {
                            modifiedDetailId = _controller.UpdateTransaction(detail);
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
                                    modifiedDetailId = _controller.UpdateTransaction(detail, true);
                                else if (dialogResult == DialogResult.No)
                                    modifiedDetailId = _controller.UpdateTransaction(detail);
                            }
                            else
                                return;
                        }
                    }

                    //Elimino y luego vuelvo a insertar los valores modificados
                    detail = _controller.GetDetailById(modifiedDetailId);
                    _view.GrdDeleteSelected(detail.TransactionId);

                    var dtos = _controller.GetIntermediateDetailDtos().Where(x => x.TransactionId == detail.TransactionId).ToList();
                    GrdUpdateValue(dtos, saveCurrentDate);
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
                    //Compruebo que el transactionDetail existe
                    var detail = _controller.GetDetailById(_view.SelectedDto.Id);
                    if (detail is null)
                    {
                        CommonMessageBox.errorMessageShow("La transacción seleccionada ya ha sido eliminada", MessageBoxButtons.OK);
                        return;
                    }

                    //Dependiendo el tipo de transaccion, la modifico de forma diferente
                    var transactionType = _view.SelectedDto.TransactionType;
                    if (transactionType == TransactionType.Single)
                    {
                        _controller.DeleteDetail(detail, _view.Date);
                    }
                    else if (transactionType == TransactionType.Installments)
                    {
                        string message = "Al eliminar esta cuota, también se eliminarán todas las cuotas relacionadas. ¿Desea continuar con la eliminación?";
                        string title = "Confirmar eliminación de cuotas";
                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                            _controller.DeleteDetail(detail, _view.Date);
                    }
                    else if (transactionType == TransactionType.Service)
                    {
                        string message = "Al eliminar este servicio, se dará por finalizado y se eliminarán " +
                            "tanto la transacción seleccionada como las futuras transacciones. ¿Desea continuar con la eliminación?";
                        string title = "Confirmar finalización de servicio";
                        if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                            _controller.DeleteDetail(detail, _view.Date);
                    }
                    else if (transactionType == TransactionType.CreditCardOutstanding)
                    {
                        var summary = _controller.GetCCSummaryByTrxId(_view.SelectedDto.TransactionId);
                        if (summary != null)
                        {
                            string descripcion = $"{summary.CreditCard.CreditCardBrand.Name} *{summary.CreditCard.LastFourNumbers}";
                            string message = $"Al eliminar este servicio, también se eliminará el resumen de tarjeta de crédito relacionado " +
                                $"({descripcion} : {_view.SelectedDto.Date.ToString("yyyy-MM")}). ¿Desea continuar con la eliminación?";
                            string title = "Confirmar eliminación de cuotas";
                            if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) == DialogResult.Yes)
                                _controller.DeleteDetail(detail, _view.Date);
                        }
                    }

                    _view.GrdDeleteSelected(_view.SelectedDto.TransactionId);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
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
