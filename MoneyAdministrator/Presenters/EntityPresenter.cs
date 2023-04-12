using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class EntityPresenter
    {
        //fields
        private const int BankTypeId = 2;

        private readonly IEntityView? _view;
        private string _databasePath;
        private bool _onlyBanks;
        private List<EntityDto> _dataSource;

        public EntityPresenter(string databasePath, bool onlyBanks = false)
        {
            _databasePath = databasePath;
            _onlyBanks = onlyBanks;
            _view = new EntityView();

            AssosiateEvents();
            RefreshEntityTypeList();
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

        private void RefreshEntityTypeList()
        {
            using (new CursorWait())
            {
                var service = new EntityTypeService(_databasePath);

                var entities = service.GetAll();

                if (_onlyBanks)
                    entities = entities.Where(x => x.Id == BankTypeId).ToList();

                this._view.SetEntityTypeList(entities);
            }
        }

        private void GrdRefreshData()
        {
            using (new CursorWait())
            {
                var entities = new EntityService(_databasePath).GetAll();

                if (_onlyBanks)
                    entities = entities.Where(x => x.EntityTypeId == BankTypeId).ToList();

                List<EntityDto> dtos = new();
                foreach (var entity in entities)
                {
                    dtos.Add(new EntityDto()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        EntityTypeName = entity.EntityType.Name,
                    });
                }
                this._view.GrdRefreshData(dtos);
            }
        }

        private void SelectItem()
        {
            var entity = new EntityService(_databasePath).Get(_view.SelectedId);

            _view.EntityName = entity.Name;
            _view.EntityType = entity.EntityType;
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
                    var entityService = new EntityService(_databasePath);
                    var entityTypeService = new EntityTypeService(_databasePath);

                    //Obtengo los valores
                    var entityName = _view.EntityName;
                    var entityType = _view.EntityType;

                    //Compruebo si el tipo de entidad ya existe, si no existe la inserto
                    var searchEntityType = entityTypeService.GetByName(entityType.Name);
                    if (searchEntityType == null)
                        entityTypeService.Insert(entityType);
                    else
                        entityType.Id = searchEntityType.Id;

                    //Inserto la tarjeta de credito
                    var entity = new Entity()
                    {
                        Name = entityName,
                        EntityTypeId = entityType.Id,
                    };
                    entityService.Insert(entity);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                RefreshEntityTypeList();
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
                    var entityService = new EntityService(_databasePath);
                    var entityTypeService = new EntityTypeService(_databasePath);

                    //Compruebo que la tarjeta de credito existe
                    var entity = entityService.Get(_view.SelectedId);
                    if (entity == null)
                    {
                        CommonMessageBox.errorMessageShow("La entidad seleccionada no existe", MessageBoxButtons.OK);
                        return;
                    }

                    //Obtengo los valores
                    var entityName = _view.EntityName;
                    var entityType = _view.EntityType;

                    //Compruebo si el tipo de entidad ya existe, si no existe la inserto
                    var searchEntityType = entityTypeService.GetByName(entityType.Name);
                    if (searchEntityType == null)
                        entityTypeService.Insert(entityType);
                    else
                        entityType.Id = searchEntityType.Id;

                    //Modifico la tarjeta de credito
                    entity.Name = entityName;
                    entity.EntityTypeId = entityType.Id;
                    entityService.Update(entity);
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                RefreshEntityTypeList();
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
                    var entityService = new EntityService(_databasePath);
                    var entityTypeService = new EntityTypeService(_databasePath);

                    //Compruebo que la tarjeta de credito existe
                    var entity = entityService.Get(_view.SelectedId);
                    if (entity == null)
                    {
                        CommonMessageBox.errorMessageShow("La entidad seleccionada ya ha sido eliminada", MessageBoxButtons.OK);
                        return;
                    }

                    //Establezco la entidad como eliminada
                    entityService.Delete(entity);

                    //Si ninguna de las entidades restantes contiene el tipo de la entidad borrada, elimino el tipado
                    var entities = entityService.GetAll().Where(x => x.EntityTypeId == entity.EntityTypeId);
                    if (!entities.Any())
                    {
                        var entityType = entityTypeService.Get(entity.EntityTypeId);
                        entityTypeService.Delete(entityType);
                    }
                }
                catch (Exception ex)
                {
                    CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
                }
                RefreshEntityTypeList();
                GrdRefreshData();
            }
        }

    }
}
