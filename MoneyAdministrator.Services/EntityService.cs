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
    public class EntityService : IService<Entity>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly bool _showDeleted;

        public EntityService(string databasePath, bool showDeleted = false) 
        {
            _unitOfWork = new UnitOfWork(databasePath);
            _showDeleted = showDeleted;
        }

        public List<Entity> GetAll()
        {
            if (_showDeleted)
                return _unitOfWork.EntityRepository.GetAll().ToList();
            else
                return _unitOfWork.EntityRepository.GetAll().Where(x => x.Deleted == false).ToList();
        }

        public Entity Get(int id)
        {
            var item = _unitOfWork.EntityRepository.GetById(id);

            if (!_showDeleted && item != null && item.Deleted)
                return null;
            else
                return item;
        }

        public Entity GetByName(string name)
        {
            var item = _unitOfWork.EntityRepository.GetAll().Where(x => x.Name == name).FirstOrDefault();

            if (!_showDeleted && item != null && item.Deleted)
                return null;
            else
                return item;
        }

        public void Insert(Entity model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.EntityRepository.GetAll()
                .Where(x => x.Name == model.Name).FirstOrDefault();

            if (item != null)
            {
                //Revierto el estado Deleted o Actualizo el tipo de entidad
                item.EntityTypeId = model.EntityTypeId;
                item.Deleted = false;
                _unitOfWork.EntityRepository.Update(item);

                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.EntityRepository.Insert(model);
            }
            _unitOfWork.Save();
        }

        public void Update(Entity model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.EntityRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.EntityRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(Entity model)
        {
            var creditCardService = new CreditCardService(_unitOfWork);

            var item = _unitOfWork.EntityRepository.GetById(model.Id);
            if (item != null )
            {
                var creditCard = creditCardService.GetAll().Where(x => x.EntityId == item.Id).FirstOrDefault();

                if (creditCard != null)
                    throw new Exception("No es posible eliminar esta entidad, ya que hay cargada una tarjeta de crédito con esta entidad");

                item.Deleted = true;
                _unitOfWork.EntityRepository.Update(item);
                _unitOfWork.Save();
            }
        }
    }
}
