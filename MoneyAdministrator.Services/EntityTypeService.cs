using Microsoft.EntityFrameworkCore;
using MoneyAdministrator.DataAccess;
using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services
{
    public class EntityTypeService : IService<EntityType>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly bool _showDeleted;

        public EntityTypeService(string databasePath, bool showDeleted = false) 
        {
            _unitOfWork = new UnitOfWork(databasePath);
            _showDeleted = showDeleted;
        }

        public List<EntityType> GetAll()
        {
            if (_showDeleted)
                return _unitOfWork.EntityTypeRepository.GetAll().ToList();
            else
                return _unitOfWork.EntityTypeRepository.GetAll().Where(x => x.Deleted == false).ToList();
        }

        public EntityType Get(int id)
        {
            var item = _unitOfWork.EntityTypeRepository.GetById(id);

            if (!_showDeleted && item != null && item.Deleted)
                return null;
            else
                return item;
        }

        public EntityType? GetByName(string name)
        {
            var item = _unitOfWork.EntityTypeRepository.GetAll().Where(x => x.Name == name).FirstOrDefault();

            if (!_showDeleted && item != null && item.Deleted)
                return null;
            else
                return item;
        }

        public void Insert(EntityType model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.EntityTypeRepository.GetAll()
                .Where(x => x.Name == model.Name).FirstOrDefault();

            if (item != null)
            {
                //Revierto la eliminacion
                item.Deleted = false;
                this.Update(item);

                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.EntityTypeRepository.Insert(model);
            }
            _unitOfWork.Save();
        }

        public void Update(EntityType model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.EntityTypeRepository.GetById(model.Id);
            if (item != null)
            {
                if (item.Id <= 2)
                    item.Deleted = false;

                _unitOfWork.EntityTypeRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(EntityType model)
        {
            var item = _unitOfWork.EntityTypeRepository.GetById(model.Id);
            if (item != null )
            {
                item.Deleted = true;
                _unitOfWork.EntityTypeRepository.Update(item);
                _unitOfWork.Save();
            }
        }
    }
}
