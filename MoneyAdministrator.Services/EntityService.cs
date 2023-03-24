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

        public EntityService(string databasePath) 
        {
            _unitOfWork = new UnitOfWork(databasePath);    
        }

        public List<Entity> GetAll()
        {
            return _unitOfWork.EntityRepository.GetAll().ToList();
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
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.EntityRepository.Insert(model);
                _unitOfWork.Save();
            }
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
            var item = _unitOfWork.EntityRepository.GetById(model.Id);
            if (item != null )
            {
                _unitOfWork.EntityRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
