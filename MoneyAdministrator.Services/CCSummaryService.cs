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
    public class CCSummaryService : IService<CCSummary>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CCSummaryService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public CCSummaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CCSummary> GetAll()
        {
            return _unitOfWork.CCResumeRepository.GetAll().ToList();
        }

        public CCSummary Get(int id)
        {
            return _unitOfWork.CCResumeRepository.GetById(id);
        }

        public void Insert(CCSummary model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var cc_entityId = 0;
            var cc_brandId = 0;

            var creditCardService = new CreditCardService(_unitOfWork);
            var creditCard = creditCardService.Get(model.CreditCardId);
            if (creditCard != null)
            {
                cc_entityId = creditCard.EntityId;
                cc_brandId = creditCard.CreditCardBrandId;
            }

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.CCResumeRepository.GetAll()
                .Where(x => x.Period == model.Period && 
                    x.CreditCard.EntityId == cc_entityId &&
                    x.CreditCard.CreditCardBrandId == cc_brandId).FirstOrDefault();

            if (item != null)
            {
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.CCResumeRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(CCSummary model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.CCResumeRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CCResumeRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(CCSummary model)
        {
            var item = _unitOfWork.CCResumeRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CCResumeRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
