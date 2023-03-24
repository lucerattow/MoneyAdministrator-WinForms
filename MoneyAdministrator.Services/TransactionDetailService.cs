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
    public class TransactionDetailService : IService<TransactionDetail>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionDetailService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<TransactionDetail> GetAll()
        {
            return _unitOfWork.TransactionDetailRepository.GetAll().ToList();
        }

        public void Insert(TransactionDetail model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si la transaccion existe
            var transaction = _unitOfWork.TransactionRepository.GetById(model.TransactionId);
            if (transaction == null)
                throw new Exception("There is no transaction with that id");

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.TransactionDetailRepository.GetAll()
                .Where(x => x.TransactionId == model.TransactionId && x.Date == model.Date).FirstOrDefault();

            if (item != null)
            {
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.TransactionDetailRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(TransactionDetail model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.TransactionDetailRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.TransactionDetailRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(TransactionDetail model)
        {
            var item = _unitOfWork.TransactionDetailRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.TransactionDetailRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
