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

        public TransactionDetail Get(int id)
        {
            return _unitOfWork.TransactionDetailRepository.GetById(id);
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
            //var item = _unitOfWork.TransactionDetailRepository.GetAll()
            //    .Where(x => x.TransactionId == model.TransactionId ).FirstOrDefault();

            _unitOfWork.TransactionDetailRepository.Insert(model);
            _unitOfWork.Save();
            UpdateSummaryOutstanding(model.TransactionId);
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
                UpdateSummaryOutstanding(item.TransactionId);
            }
        }

        public void Delete(TransactionDetail model)
        {
            var item = _unitOfWork.TransactionDetailRepository.GetById(model.Id);

            if (item != null)
            {
                _unitOfWork.TransactionDetailRepository.Delete(item);
                _unitOfWork.Save();
                UpdateSummaryOutstanding(item.TransactionId);
                DeleteTransactionIfNotHaveDetails();
            }
        }

        /// <summary>
        /// Crea una lista con los detalles de la transaccion, la cual luego podra ser usada para añadirse a la base de datos
        /// </summary>
        /// <param name="transactionId">Transaccion a la cual se le asociaran los TransactionDetails</param>
        /// <param name="date">Fecha inicial de los TransactionDetails</param>
        /// <param name="amount">Monto de cada TransactionDetails</param>
        /// <param name="totalMonths">Limite de meses para ingresar TransactionDetails</param>
        /// <param name="frequencyMonths">Valor que se utiliza para saltear meses, ejemplo 3, hara que se ingrese 1 TransactionDetails cada 3 meses</param>
        /// <param name="isService">Indica si la transaccion es un servicio</param>
        /// <returns></returns>
        public List<TransactionDetail> GenerateTransactionDetails(int transactionId, DateTime date, decimal amount, int totalMonths, int frequencyMonths, bool isService)
        {
            List<TransactionDetail> result = new List<TransactionDetail>();

            for (int i = 1; i <= totalMonths; i += frequencyMonths)
            {
                var transactionDetail = new TransactionDetail()
                {
                    TransactionId = transactionId,
                    Date = date,
                    Amount = amount,
                    Installment = isService ? 0 : i,
                    Frequency = isService ? frequencyMonths : 0,
                };
                date = date.AddMonths(frequencyMonths);
                result.Add(transactionDetail);
            }

            return result;
        }

        /// <summary>Analizo los resumenes de tarjeta de credito, para saber si hay que actualizar su transaccion de saldo pendiente.</summary>
        private void UpdateSummaryOutstanding(int transactionId)
        {
            //Inicializo los servicios
            var ccSummaryService = new CCSummaryService(_unitOfWork);

            var summary = ccSummaryService.GetAll().Where(x => x.TransactionPayId == transactionId).FirstOrDefault();
            if (summary is null || summary.TransactionPayId == 0)
                return;

            var transactionOutstanding = this.GetAll().Where(x => x.TransactionId == summary.TransactionId).FirstOrDefault();
            var transactionPayments = this.GetAll().Where(x => x.TransactionId == summary.TransactionPayId).ToList();

            var payedAmount = transactionPayments.Sum(x => x.Amount);

            if (transactionOutstanding != null)
            {
                var newValue = summary.TotalArs + payedAmount;
                transactionOutstanding.Amount = newValue > 0 ? 0 : newValue;
                this.Update(transactionOutstanding);
            }
        }

        private void DeleteTransactionIfNotHaveDetails()
        {
            //Inicializo los servicios
            var transactionService = new TransactionService(_unitOfWork);
            var transactions = transactionService.GetAll().Where(x => x.TransactionDetails.Count == 0);

            foreach (var transaction in transactions)
                transactionService.Delete(transaction);
        }
    }
}
