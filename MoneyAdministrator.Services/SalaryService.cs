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
    public class SalaryService : IService<Salary>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalaryService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<Salary> GetAll()
        {
            return _unitOfWork.SalaryRepository.GetAll().OrderByDescending(x => x.Date).ToList();
        }

        public Salary Get(int id)
        {
            return _unitOfWork.SalaryRepository.GetById(id);
        }

        public Salary? GetByPeriod(DateTime period, int currencyId)
        {
            return _unitOfWork.SalaryRepository.GetAll().Where(x => x.Date == period && x.CurrencyId == currencyId).FirstOrDefault();
        }

        public void Insert(Salary model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.SalaryRepository.GetAll()
                .Where(x => x.Date == model.Date && x.CurrencyId == model.CurrencyId).FirstOrDefault();

            if (item != null)
            {
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.SalaryRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(Salary model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.SalaryRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.SalaryRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(Salary model)
        {
            var item = _unitOfWork.SalaryRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.SalaryRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
