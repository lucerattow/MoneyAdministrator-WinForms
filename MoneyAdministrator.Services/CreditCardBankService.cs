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
    public class CreditCardBankService : IService<CreditCardBank>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreditCardBankService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<CreditCardBank> GetAll()
        {
            return _unitOfWork.CreditCardBankRepository.GetAll().ToList();
        }

        public CreditCardBank Get(int id)
        {
            return _unitOfWork.CreditCardBankRepository.GetById(id);
        }

        public CreditCardBank GetByName(string name)
        {
            return _unitOfWork.CreditCardBankRepository.GetAll().Where(x => x.Name == name).FirstOrDefault();
        }

        public void Insert(CreditCardBank model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.CreditCardBankRepository.GetAll()
                .Where(x => x.Name == model.Name).FirstOrDefault();

            if (item != null)
            {
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.CreditCardBankRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(CreditCardBank model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.CreditCardBankRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CreditCardBankRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(CreditCardBank model)
        {
            var item = _unitOfWork.CreditCardBankRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CreditCardBankRepository.Update(item);
                _unitOfWork.Save();
            }
        }
    }
}
