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

        public CreditCardService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<CreditCard> GetAll()
        {
            return _unitOfWork.CreditCardRepository.GetAll().ToList();
        }

        public CreditCard Get(int id)
        {
            return _unitOfWork.CreditCardRepository.GetById(id);
        }

        public void Insert(CreditCard model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.CreditCardRepository.GetAll()
                .Where(x => x.EntityId == model.EntityId && x.CreditCardTypeId == model.CreditCardTypeId).FirstOrDefault();

            if (item != null)
            {
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
                _unitOfWork.CreditCardRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
