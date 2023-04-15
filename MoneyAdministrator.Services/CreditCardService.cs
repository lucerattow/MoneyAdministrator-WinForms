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
    public class CreditCardService : IService<CreditCard>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly bool _showDeleted;

        public CreditCardService(string databasePath, bool showDeleted = false)
        {
            _unitOfWork = new UnitOfWork(databasePath);
            _showDeleted = showDeleted;
        }

        public CreditCardService(IUnitOfWork unitOfWork, bool showDeleted = false)
        {
            _unitOfWork = unitOfWork;
            _showDeleted = showDeleted;
        }

        public List<CreditCard> GetAll()
        {
            if (_showDeleted)
                return _unitOfWork.CreditCardRepository.GetAll().ToList();
            else
                return _unitOfWork.CreditCardRepository.GetAll().Where(x => x.Deleted == false).ToList();
        }

        public CreditCard Get(int id)
        {
            var item = _unitOfWork.CreditCardRepository.GetById(id);

            if (!_showDeleted && item != null && item.Deleted)
                return null;
            else
                return item;
        }

        public void Insert(CreditCard model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.CreditCardRepository.GetAll()
                .Where(x => x.CreditCardBankId == model.CreditCardBankId && x.CreditCardBrandId == model.CreditCardBrandId).FirstOrDefault();

            if (item != null)
            {
                //Revierto el estado Deleted o Actualizo el tipo de entidad
                item.LastFourNumbers = model.LastFourNumbers;
                item.Deleted = false;
                _unitOfWork.CreditCardRepository.Update(item);

                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.CreditCardRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(CreditCard model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.CreditCardRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CreditCardRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(CreditCard model)
        {
            var item = _unitOfWork.CreditCardRepository.GetById(model.Id);
            if (item != null)
            {
                item.Deleted = true;
                _unitOfWork.CreditCardRepository.Update(item);
                _unitOfWork.Save();
            }
        }
    }
}
