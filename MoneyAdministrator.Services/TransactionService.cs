using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.DataAccess;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyAdministrator.Common.DTOs;

namespace MoneyAdministrator.Services
{
    public class TransactionService : IService<Transaction>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Transaction> GetAll()
        {
            return _unitOfWork.TransactionRepository.GetAll().ToList();
        }

        public Transaction Get(int id)
        {
            return _unitOfWork.TransactionRepository.GetById(id);
        }

        public void Insert(Transaction model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si la currency existe
            var currency = _unitOfWork.CurrencyRepository.GetById(model.CurrencyId);
            if (currency == null)
                throw new Exception("There is no currency with that id");

            //Compruebo si la entity existe
            var entity = _unitOfWork.EntityRepository.GetById(model.EntityId);
            if (entity == null)
                throw new Exception("There is no entity with that id");

            //Agrego el modelo a la base de datos
            _unitOfWork.TransactionRepository.Insert(model);
            _unitOfWork.Save();
        }

        public void Update(Transaction model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.TransactionRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.TransactionRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(Transaction model)
        {
            var item = _unitOfWork.TransactionRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.TransactionRepository.Delete(item);
                _unitOfWork.Save();
                UpdateCreditCardSummary();
            }
        }

        //methods
        private void UpdateCreditCardSummary()
        {
            //Inicializo los servicios
            var ccSummaryService = new CCSummaryService(_unitOfWork);

            var summaries = ccSummaryService.GetAll();
            if (summaries.Count == 0)
                return;

            foreach (var summary in summaries)
            {
                var transactions = GetAll().Where(x => x.Id == summary.TransactionPayId).FirstOrDefault();
                if (transactions is null)
                {
                    summary.TransactionPayId = 0;
                    ccSummaryService.Update(summary);
                }
            }
        }
    }
}