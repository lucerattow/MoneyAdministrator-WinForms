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

        public EntityTypeService(string databasePath) 
        {
            _unitOfWork = new UnitOfWork(databasePath);    
        }

        public List<EntityType> GetAll()
        {
            return _unitOfWork.EntityTypeRepository.GetAll().ToList();
        }

        public EntityType Get(int id)
        {
            return _unitOfWork.EntityTypeRepository.GetById(id);
        }

        public EntityType GetByName(string name)
        {
            return _unitOfWork.EntityTypeRepository.GetAll().Where(x => x.Name == name).FirstOrDefault();
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
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.EntityTypeRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(EntityType model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.EntityTypeRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.EntityTypeRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(EntityType model)
        {
            var item = _unitOfWork.EntityTypeRepository.GetById(model.Id);
            if (item != null )
            {
                _unitOfWork.EntityTypeRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
