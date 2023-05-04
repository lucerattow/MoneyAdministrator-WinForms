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
    public class CurrencyService : IService<Currency>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<Currency> GetAll()
        {
            return _unitOfWork.CurrencyRepository.GetAll().ToList();
        }

        public Currency Get(int id)
        {
            return _unitOfWork.CurrencyRepository.GetById(id);
        }

        public Currency GetByName(string name)
        {
            return _unitOfWork.CurrencyRepository.GetAll().Where(x => x.Name == name).FirstOrDefault();
        }

        public void Insert(Currency model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.CurrencyRepository.GetAll()
                .Where(x => x.Name == model.Name).FirstOrDefault();

            if (item != null)
            {
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.CurrencyRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(Currency model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.CurrencyRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CurrencyRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(Currency model)
        {
            var item = _unitOfWork.CurrencyRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CurrencyRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
