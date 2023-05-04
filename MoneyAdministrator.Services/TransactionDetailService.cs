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
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Common.Utilities.TypeTools;

namespace MoneyAdministrator.Services
{
    public class TransactionDetailService : IService<TransactionDetail>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionDetailService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public TransactionDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public void UpdateCheckBox(TransactionDetail detail, DateTime date, bool concider, bool paid)
        {
            var details = detail.Transaction.TransactionDetails.OrderByDescending(x => x.Date).ToList();

            var current = details
                .Where(x => x.Date.Date <= date.Date)
                .FirstOrDefault();

            if (current.Date.Date == date.Date)
            {
                //Si el detalle del siguiente mes no existe, lo creo
                var endDate = details.Select(x => x.EndDate).Max();

                //Si ya existen detalles futuros, obtengo la fecha del siguiente
                var futureDetails = details
                    .Where(x => x.Date.Date >= date.Date.AddMonths(detail.Frequency))
                    .ToList();
                if (futureDetails.Count > 0)
                {
                    var futureDetail = futureDetails.LastOrDefault();
                    endDate = futureDetail.Date.AddMonths(-current.Frequency);
                }

                //Si la fecha final no es la actual, creo un nuevo detalle
                if (endDate.Date != date.Date)
                {
                    //Creo el proximo detalle
                    var newFutureDetail = new TransactionDetail
                    {
                        TransactionId = current.TransactionId,
                        Date = date.AddMonths(current.Frequency),
                        EndDate = endDate,
                        Amount = current.Amount,
                        Frequency = current.Frequency,
                        Concider = true,
                        Paid = false,
                    };
                    Insert(newFutureDetail);
                }

                //Modifico el detalle actual
                current.Concider = concider;
                current.Paid = paid;
                current.EndDate = date;
                Update(current);
            }
            else
            {
                //Si el detalle del siguiente mes no existe, lo creo
                var endDate = details.Select(x => x.EndDate).Max();

                //Si ya existen detalles futuros, obtengo la fecha del siguiente
                var futureDetails = details
                    .Where(x => x.Date.Date >= date.Date.AddMonths(detail.Frequency))
                    .ToList();
                if (futureDetails.Count > 0)
                {
                    var futureDetail = futureDetails.LastOrDefault();
                    endDate = futureDetail.Date.AddMonths(-current.Frequency);
                }

                //Si la fecha final no es la actual, creo un nuevo detalle
                if (endDate.Date != date.Date)
                {
                    //Creo el proximo detalle
                    var newFutureDetail = new TransactionDetail
                    {
                        TransactionId = current.TransactionId,
                        Date = date.AddMonths(current.Frequency),
                        EndDate = endDate,
                        Amount = current.Amount,
                        Frequency = current.Frequency,
                        Concider = true,
                        Paid = false,
                    };
                    Insert(newFutureDetail);
                }

                //Si ya existen detalles pasados
                var pastDetails = details
                    .Where(x => x.Date.Date <= date.Date.AddMonths(-current.Frequency))
                    .ToList();
                if (pastDetails.Count > 0)
                {
                    //Si existen detalles pasados, tomo el ultimo y lo actualizo
                    var pastDetail = pastDetails.FirstOrDefault();

                    //Si la fecha final es distinta a la que deberia, lo actualizo
                    if (pastDetail.EndDate != date.AddMonths(-current.Frequency))
                    {
                        pastDetail.EndDate = date.AddMonths(-current.Frequency);
                        Update(pastDetail);
                    }
                }

                //Creo la nueva transaccion actual
                var newCurrent = new TransactionDetail
                {
                    TransactionId = current.TransactionId,
                    Date = date,
                    EndDate = date,
                    Amount = current.Amount,
                    Frequency = current.Frequency,
                    Concider = concider,
                    Paid = paid,
                };
                Insert(newCurrent);
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
                var newValue = summary.TotalArs + Math.Abs(payedAmount);
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
