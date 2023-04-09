using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.DataAccess;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services
{
    public class CreditCardTypeService : IService<CreditCardType>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly bool _showDeleted;

        public CreditCardTypeService(string databasePath, bool showDeleted = false)
        {
            _unitOfWork = new UnitOfWork(databasePath);
            _showDeleted = showDeleted;
        }

        public List<CreditCardType> GetAll()
        {
            if (_showDeleted)
                return _unitOfWork.CreditCardTypeRepository.GetAll().ToList();
            else
                return _unitOfWork.CreditCardTypeRepository.GetAll().Where(x => x.Deleted == false).ToList();
        }

        public CreditCardType Get(int id)
        {
            var item = _unitOfWork.CreditCardTypeRepository.GetById(id);

            if (!_showDeleted && item != null && item.Deleted)
                return null;
            else
                return item;
        }

        public void Insert(CreditCardType model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.CreditCardTypeRepository.GetAll()
                .Where(x => x.Name == model.Name).FirstOrDefault();

            if (item != null)
            {
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.CreditCardTypeRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(CreditCardType model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.CreditCardTypeRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CreditCardTypeRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(CreditCardType model)
        {
            var item = _unitOfWork.CreditCardTypeRepository.GetById(model.Id);
            if (item != null)
            {
                item.Deleted = true;
                _unitOfWork.CreditCardTypeRepository.Update(item);
                _unitOfWork.Save();
            }
        }
    }
}
