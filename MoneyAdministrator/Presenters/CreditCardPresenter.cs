using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views;
using MoneyAdministrator.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class CreditCardPresenter
    {
        //fields
        private ICreditCardView _view;
        private string _databasePath;

        //properties
        public ICreditCardView View
        {
            get { return _view; }
        }

        public CreditCardPresenter(string databasePath)
        {
            _databasePath = databasePath;
            _view = new CreditCardView();

            AssosiateEvents();
            CreditCardBankRefreshData();
            CreditCardBrandRefreshData();
            GrdRefreshData();
        }

        //methods
        public int Show()
        {
            if (_view == null)
                throw new Exception("Ocurrio un error al intentar abrir el popup");

            if (_view.ShowDialog() == DialogResult.OK)
                return _view.SelectedId;
            else
                return -1;
        }

        private void AssosiateEvents()
        {
            _view.GrdDoubleClick += GrdDoubleClick;
            _view.ButtonSelectClick += ButtonSelectClick;
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
        }

        private void CreditCardBankRefreshData()
        {
            using (new CursorWait())
            {
                var service = new CreditCardBankService(_databasePath);
                var entities = service.GetAll();
                this._view.CreditCardBankRefreshData(entities);
            }
        }

        private void CreditCardBrandRefreshData()
        {
            using (new CursorWait())
            { 
                var service = new CreditCardBrandService(_databasePath);
                var entities = service.GetAll();
                this._view.CreditCardBrandRefreshData(entities);
            }
        }

        private void GrdRefreshData()
        {
            using (new CursorWait())
            {
                var creditCards = new CreditCardService(_databasePath).GetAll();

                List<CreditCardDto> dtos = new();
                foreach (var creditCard in creditCards)
                {
                    dtos.Add(new CreditCardDto()
                    {
                        Id = creditCard.Id,
                        BankEntityName = creditCard.CreditCardBank.Name,
                        CreditCardTypeName = creditCard.CreditCardBrand.Name,
                        LastFourNumbers = $"●●●● ●●●● ●●●● {creditCard.LastFourNumbers}",
                    });
                }
                this._view.GrdRefreshData(dtos);
            }
        }

        private void SelectItem()
        {
            var creditCard = new CreditCardService(_databasePath).Get(_view.SelectedId);

            _view.CreditCardBank = creditCard.CreditCardBank;
            _view.CreditCardBrand = creditCard.CreditCardBrand;
            _view.LastFourNumbers = creditCard.LastFourNumbers;
        }

        //events
        private void GrdDoubleClick(object? sender, EventArgs e)
        {
            SelectItem();
        }

        private void ButtonSelectClick(object? sender, EventArgs e)
        {
            _view.DialogResult = DialogResult.OK;
        }

        private void ButtonInsertClick(object? sender, EventArgs e)
        {
            using (new CursorWait())
            {
                try
                {
                    //Inicializo los servicios
                    var creditCardService = new CreditCardService(_databasePath);
                    var creditCardBankService = new CreditCardBankService(_databasePath);

                    //Obtengo los valores
                    var creditCardBank = _view.CreditCardBank;
                    var creditCardBrand = _view.CreditCardBrand;
                    var lastFourNumbers = _view.LastFourNumbers;

                    //Compruebo el banco ya existe, si no existe la inserto
                    var searchBank = creditCardBankService.GetByName(creditCardBank.Name);
                    if (searchBank == null)
                        creditCardBankService.Insert(creditCardBank);
                    else
                        creditCardBank.Id = searchBank.Id;

                    //Inserto la tarjeta de credito
                    var creditCard = new CreditCard()
                    {
                        CreditCardBankId = creditCardBank.Id,
                        CreditCardBrandId = creditCardBrand.Id,
                        LastFourNumbers = lastFourNumbers,
                    };
                    creditCardService.Insert(creditCard);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                CreditCardBankRefreshData();
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
                    var creditCardService = new CreditCardService(_databasePath);
                    var creditCardBankService = new CreditCardBankService(_databasePath);

                    //Compruebo que la tarjeta de credito existe
                    var creditCard = creditCardService.Get(_view.SelectedId);
                    if (creditCard == null)
                    {
                        CommonMessageBox.errorMessageShow("La tarjeta de credito seleccionada no existe", MessageBoxButtons.OK);
                        return;
                    }

                    //Obtengo los valores
                    var creditCardBank = _view.CreditCardBank;
                    var creditCardType = _view.CreditCardBrand;
                    var lastFourNumbers = _view.LastFourNumbers;

                    //Compruebo el banco ya existe, si no existe la inserto
                    var searchBank = creditCardBankService.GetByName(creditCardBank.Name);
                    if (searchBank == null)
                        creditCardBankService.Insert(creditCardBank);
                    else
                        creditCardBank.Id = searchBank.Id;

                    //Modifico la tarjeta de credito
                    creditCard.CreditCardBankId = creditCardBank.Id;
                    creditCard.CreditCardBrandId = creditCardType.Id;
                    creditCard.LastFourNumbers = lastFourNumbers;
                    creditCardService.Update(creditCard);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                CreditCardBankRefreshData();
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
                    var creditCardService = new CreditCardService(_databasePath);

                    //Compruebo que la tarjeta de credito existe
                    var creditCard = creditCardService.Get(_view.SelectedId);
                    if (creditCard == null)
                    {
                        CommonMessageBox.errorMessageShow("La tarjeta de credito seleccionada ya ha sido eliminada", MessageBoxButtons.OK);
                        return;
                    }

                    creditCardService.Delete(creditCard);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                CreditCardBankRefreshData();
                GrdRefreshData();
            }
        }
    }
}
