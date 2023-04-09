using MoneyAdministrator.DTOs;
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
        private Action _closeView;

        //properties
        public ICreditCardView View
        {
            get { return _view; }
        }

        public CreditCardPresenter(string databasePath, Action closeView)
        {
            _databasePath = databasePath;
            _closeView = closeView;
            _view = new CreditCardView();

            AssosiateEvents();
            SetCreditCardTypeList();
            GrdRefreshData();
        }

        //methods
        public bool Show()
        {
            if (_view == null)
                throw new Exception("Ocurrio un error al intentar abrir el popup");

            return _view.ShowDialog() == DialogResult.OK;
        }

        private void AssosiateEvents()
        {
            _view.GrdDoubleClick += GrdDoubleClick;
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
            _view.ButtonEntitySearchClick += ButtonEntitySearchClick;
        }

        private void SetCreditCardTypeList()
        {
            using (new CursorWait())
            { 
                var service = new CreditCardTypeService(_databasePath);
                var entities = service.GetAll();
                this._view.SetCreditCardTypeList(entities);
            }
        }

        private void GrdRefreshData()
        {
            using (new CursorWait())
            {
                var creditCards = new CreditCardService(_databasePath).GetAll();

                List<CreditCardViewDto> dtos = new();
                foreach (var creditCard in creditCards)
                {
                    dtos.Add(new CreditCardViewDto()
                    {
                        Id = creditCard.Id,
                        BankEntityName = creditCard.Entity.Name,
                        CreditCardTypeName = creditCard.CreditCardType.Name,
                        LastFourNumbers = $"●●●● ●●●● ●●●● {creditCard.LastFourNumbers}",
                    });
                }
                this._view.GrdRefreshData(dtos);
            }
        }

        private void SelectTransaction()
        {
            var creditCard = new CreditCardService(_databasePath).Get(_view.SelectedId);

            _view.BankEntityName = creditCard.Entity.Name;
            _view.SelectedCreditCardType = creditCard.CreditCardType;
            _view.LastFourNumbers = creditCard.LastFourNumbers;
        }

        //events
        private void GrdDoubleClick(object? sender, EventArgs e)
        {
            SelectTransaction();
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
                    var creditCardnService = new CreditCardService(_databasePath);
                    var entityService = new EntityService(_databasePath);

                    //Obtengo los valores
                    var entity = new Entity() { Name = _view.BankEntityName, EntityTypeId = 2 }; //EntityType = "Banco"
                    var creditCardType = _view.SelectedCreditCardType;
                    var lastFourNumbers = _view.LastFourNumbers;

                    //Compruebo si la entidad ya existe, si no existe la inserto
                    var searchEntity = entityService.GetByName(entity.Name);
                    if (searchEntity == null)
                        entityService.Insert(entity);
                    else
                        entity.Id = searchEntity.Id;

                    //Inserto la tarjeta de credito
                    var creditCard = new CreditCard()
                    {
                        EntityId = entity.Id,
                        CreditCardTypeId = creditCardType.Id,
                        LastFourNumbers = lastFourNumbers,
                    };
                    creditCardnService.Insert(creditCard);
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
                    var creditCardService = new CreditCardService(_databasePath);
                    var entityService = new EntityService(_databasePath);

                    //Compruebo que la tarjeta de credito existe
                    var creditCard = creditCardService.Get(_view.SelectedId);
                    if (creditCard == null)
                    {
                        CommonMessageBox.errorMessageShow("La tarjeta de credito seleccionada no existe", MessageBoxButtons.OK);
                        return;
                    }

                    //Obtengo los valores
                    var entity = new Entity() { Name = _view.BankEntityName, EntityTypeId = 2 }; //EntityType = "Banco"
                    var creditCardType = _view.SelectedCreditCardType;
                    var lastFourNumbers = _view.LastFourNumbers;

                    //Compruebo si la entidad ya existe, si no existe la inserto
                    var searchEntity = entityService.GetByName(entity.Name);
                    if (searchEntity == null)
                        entityService.Insert(entity);
                    else
                        entity.Id = searchEntity.Id;

                    //Modifico la tarjeta de credito
                    creditCard.EntityId = entity.Id;
                    creditCard.CreditCardTypeId = creditCardType.Id;
                    creditCard.LastFourNumbers = lastFourNumbers;
                    creditCardService.Update(creditCard);
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
                    var creditCardService = new CreditCardService(_databasePath);
                    var entityService = new EntityService(_databasePath);

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
                GrdRefreshData();
            }
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
                    _view.BankEntityName = entities.Where(x => x.Id == selectedId).FirstOrDefault().Name;
            }
        }
    }
}
