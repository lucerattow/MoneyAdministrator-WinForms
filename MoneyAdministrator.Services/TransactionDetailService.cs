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

        public List<TransactionViewDto> GetIntermediateDetailDtos()
        {
            var result = new List<TransactionViewDto>();

            var details = GetAll();

            if (details.Count == 0)
                return result;

            //Obtengo los valores del dashboard para generar los servicios correctamente
            var usdList = new CurrencyValueService(_unitOfWork).GetAll().OrderByDescending(x => x.Date);
            var salaryList = new SalaryService(_unitOfWork).GetAll().OrderByDescending(x => x.Date);

            //Obtengo la lista de años
            var yearsTransactions = details.Select(x => x.Date.Year).Distinct().ToList();
            var yearsUsd = usdList.Select(x => x.Date.Year).Distinct().ToList();
            var yearssalary = salaryList.Select(x => x.Date.Year).Distinct().ToList();

            //Unifico la lista de años
            var allYears = new List<int>();
            allYears.AddRange(yearsTransactions);
            allYears.AddRange(yearsUsd);
            allYears.AddRange(yearssalary);

            //Genero los años intermedios si es que faltan
            var initYear = allYears.Min();
            var endYear = allYears.Max();

            allYears.Clear();
            allYears.AddRange(IntTools.GetIntermediateNumbers(initYear, endYear));

            //Genero la fecha maxima de servicios
            var maxDate = new DateTime(allYears.Max() + 1, 1, 1);
            maxDate = maxDate.AddDays(-1);

            foreach (var detail in details)
            {
                //Si el detalle es un servicio, limito la fecha donde finaliza
                var endDate = detail.EndDate;
                if (detail.Transaction.TransactionType == TransactionType.Service && detail.EndDate > maxDate)
                    endDate = maxDate;

                //Obtengo la diferencia de meses
                int months = DateTimeTools.GetMonthDifference(detail.Date, endDate);

                //Genero una transaccion por cada mes
                for (int i = 0; i <= months; i += detail.Frequency)
                {
                    //Si es una transaccion de cuotas, genero el string con formato "1 / 3"
                    string installments = "";
                    if (detail.Transaction.TransactionType == TransactionType.Installments)
                    {
                        var initInstallment = details
                            .Where(x => x.TransactionId == detail.TransactionId)
                            .OrderByDescending(x => x.Date)
                            .LastOrDefault();

                        var allInstallment = details
                            .Where(x => x.TransactionId == detail.TransactionId)
                            .OrderByDescending(x => x.Date)
                            .ToList();

                        var maxInstallment = details
                            .Where(x => x.TransactionId == detail.TransactionId)
                            .OrderByDescending(x => x.Date)
                            .FirstOrDefault();

                        int maxinst = DateTimeTools.GetMonthDifference(initInstallment.Date, maxInstallment.EndDate);
                        int currentInst = DateTimeTools.GetMonthDifference(initInstallment.Date, detail.Date);

                        //Se suma 1 ya que en todos los casos la cuota 1 es la 0 realmente
                        installments = maxinst >= 1 ? $"{currentInst + i + 1} / {maxinst + 1}" : "";
                    }

                    //Genero el detalle
                    result.Add(new TransactionViewDto()
                    {
                        Id = detail.Id,
                        TransactionType = detail.Transaction.TransactionType,
                        Frequency = detail.Frequency,
                        Date = detail.Date.AddMonths(i),
                        EntityName = detail.Transaction.Entity.Name,
                        Description = detail.Transaction.Description,
                        Installment = installments,
                        CurrencyName = detail.Transaction.Currency.Name,
                        Amount = detail.Amount,
                        Concider = detail.Concider,
                        Paid = detail.Paid,
                    });
                }
            }

            return result;
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

        public void UpdateServiceTransaction(TransactionDetail detail, DateTime date, decimal amount, bool overrideNext)
        {
            var details = detail.Transaction.TransactionDetails.OrderByDescending(x => x.Date).ToList();

            var current = details
                .Where(x => x.Date.Date <= date.Date)
                .FirstOrDefault();

            if (current.Date.Date == date.Date)
            {
                var endDate = DateTime.MaxValue;
                var futureDetails = details
                    .Where(x => x.Date.Date > date.Date)
                    .ToList();

                if (futureDetails.Count > 0)
                    if (overrideNext)
                    {
                        //Elimino los detalles futuros
                        foreach (var futureDetail in futureDetails)
                        {
                            Delete(futureDetail);
                        }
                    }
                    else
                    {
                        var futureDetail = futureDetails.LastOrDefault();
                        endDate = futureDetail.Date.AddMonths(-current.Frequency);
                    }

                current.Date = date;
                current.EndDate = endDate;
                current.Amount = amount;

                Update(current);
            }
            else
            {
                var endDate = DateTime.MaxValue;
                var futureDetails = details
                    .Where(x => x.Date.Date > date.Date)
                    .ToList();

                if (futureDetails.Count > 0)
                    if (overrideNext)
                    {
                        //Elimino los detalles futuros
                        foreach (var futureDetail in futureDetails)
                        {
                            Delete(futureDetail);
                        }
                    }
                    else
                    {
                        var futureDetail = futureDetails.LastOrDefault();
                        endDate = futureDetail.Date.AddMonths(-current.Frequency);
                    }

                //Actualizo la transaccion que se volveria el ultimo antes de actualizar
                current.EndDate = date.AddMonths(-current.Frequency);
                Update(current);

                //Creo la transaccion nueva
                Insert(new TransactionDetail
                {
                    TransactionId = current.TransactionId,
                    Date = date,
                    EndDate = endDate,
                    Amount = amount,
                    Frequency = current.Frequency,
                    Concider = true,
                    Paid = false,
                });
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
