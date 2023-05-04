using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services.Controllers
{
    public class TransactionHistoryController
    {
        private string _databasePath;

        public TransactionHistoryController(string databasePath)
        {
            _databasePath = databasePath;
        }

        //public
        public List<Currency> GetCurrenciesList()
        {
            return new CurrencyService(_databasePath).GetAll();
        }

        public CCSummary IsCreditCardDetail(int id)
        {
            return new CCSummaryService(_databasePath).GetAll().Where(x => x.TransactionId == id).FirstOrDefault();
        }

        public TransactionDetail GetDetailModelById(int id) => new TransactionDetailService(_databasePath).Get(id);

        public TransactionViewDto GetDetailById(int id)
        {
            var detail = GetDetailModelById(id);
            //Si es una transaccion de cuotas, genero el string con formato "1 / 3"
            string installments = GetInstallmentValue(detail);
            //Genero el detalle
            return new TransactionViewDto()
            {
                Id = detail.Id,
                TransactionType = detail.Transaction.TransactionType,
                Frequency = detail.Frequency,
                Date = detail.Date,
                EntityName = detail.Transaction.Entity.Name,
                Description = detail.Transaction.Description,
                Installment = installments,
                CurrencyName = detail.Transaction.Currency.Name,
                Amount = detail.Amount,
                Concider = detail.Concider,
                Paid = detail.Paid,
            };
        }

        public List<TransactionViewDto> GetIntermediateDetailDtos()
        {
            var result = new List<TransactionViewDto>();
            //Obtengo la lista de detalles
            var details = new TransactionDetailService(_databasePath).GetAll();
            if (details.Count == 0)
                return result;
            //Obtengo los valores del dashboard para generar los servicios correctamente
            var usdList = new CurrencyValueService(_databasePath).GetAll().OrderByDescending(x => x.Date);
            var salaryList = new SalaryService(_databasePath).GetAll().OrderByDescending(x => x.Date);
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
                    string installments = GetInstallmentValue(detail, i);
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

        public int InsertNewTransaction(string entityName, int currencyId, string description, DateTime date, bool isInstallments,
            int installmentMax, bool IsService, decimal amount, int frequency)
        {
            //Inicializo los servicios
            var transactionService = new TransactionService(_databasePath);
            var entityService = new EntityService(_databasePath);
            //Transaction Inputs
            var transactionType = TransactionType.Single;
            //Si la entidad no existe la inserto
            var entity = entityService.GetByName(entityName);
            if (entity is null)
            {
                entity = new Entity
                {
                    Name = entityName,
                    EntityTypeId = 1, //General
                };
                entityService.Insert(entity);
            }
            //Determino el tipo de transaccion
            var endDate = date;
            if (isInstallments)
            {
                transactionType = TransactionType.Installments;
                //Se resta 1 ya que la cuota 1 es el mes inicial
                endDate = date.AddMonths(installmentMax - 1);
            }
            else if (IsService)
            {
                transactionType = TransactionType.Service;
                endDate = DateTime.MaxValue;
            }
            var dto = new TransactionDetailDto
            {
                EntityId = entity.Id,
                CurrencyId = currencyId,
                TransactionType = transactionType,
                Description = description,
                //details
                Date = date,
                EndDate = endDate,
                Amount = amount,
                Frequency = frequency,
            };
            return transactionService.CreateTransaction(dto);
        }

        public int UpdateTransaction(TransactionViewDto detailDto, bool overrideNextService = false)
        {
            //Inicializo los servicios
            var entityService = new EntityService(_databasePath);
            var currencyService = new CurrencyService(_databasePath);
            var transactionDetailService = new TransactionDetailService(_databasePath);
            //Si la entidad no existe la inserto
            var entity = entityService.GetByName(detailDto.EntityName);
            if (entity is null)
            {
                entity = new Entity
                {
                    Name = detailDto.EntityName,
                    EntityTypeId = 1, //General
                };
                entityService.Insert(entity);
            }
            //Si la moneda no existe lanzo error
            var currency = currencyService.GetByName(detailDto.EntityName);
            if (currency is null)
                throw new Exception("La moneda seleccionada no es valida");
            //Modifico la transaccion
            var detail = transactionDetailService.Get(detailDto.Id);
            detail.Transaction.EntityId = entity.Id;
            detail.Transaction.CurrencyId = currency.Id;
            detail.Transaction.Description = detailDto.Description;
            transactionDetailService.Update(detail);
            //variable para guardar el id del detalle
            var detailId = -1;
            //Dependiendo el tipo de transaccion, la modifico de forma diferente
            var transactionType = detail.Transaction.TransactionType;
            if (transactionType == TransactionType.Single)
                detailId = UpdateSingle(detailDto);
            else if (transactionType == TransactionType.Installments)
                detailId = UpdateInstallment(detailDto);
            else if (transactionType == TransactionType.Service)
                detailId = UpdateService(detailDto, overrideNextService);
            return detailId;
        }

        public void DeleteDetail(TransactionViewDto detailDto, DateTime date)
        {
            //Inicializo los servicios
            var transactionService = new TransactionService(_databasePath);
            var transactionDetailService = new TransactionDetailService(_databasePath);
            var creditCardSummaryService = new CCSummaryService(_databasePath);
            var detail = GetDetailModelById(detailDto.Id);
            var allDetails = detail.Transaction.TransactionDetails.ToList();
            //Elimino una transaccion en cuotas
            if (detail.Transaction.TransactionType == TransactionType.Installments)
            {
                foreach (var td in allDetails)
                    transactionDetailService.Delete(td);
            }
            //Elimino un servicio
            else if (detail.Transaction.TransactionType == TransactionType.Service)
            {
                var current = allDetails.Where(x => x.Date.Date <= date.Date && x.EndDate.Date >= date.Date).FirstOrDefault();
                if (current.Date == date)
                {
                    transactionDetailService.Delete(current);
                }
                else
                {
                    current.EndDate = date.AddMonths(-1);
                    transactionDetailService.Update(current);
                }
                //elimino detalles futuros
                foreach (var futureDetail in allDetails.Where(x => x.Date > date).ToList())
                    transactionDetailService.Delete(futureDetail);
            }
            //Elimino transaccion unica
            else if (detail.Transaction.TransactionType == TransactionType.Single)
            {
                transactionDetailService.Delete(detail);
            }
        }

        //private
        private string GetInstallmentValue(TransactionDetail detail, int currentInstallmentOffset = 0)
        {
            string installments = "";
            var details = detail.Transaction.TransactionDetails;
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
                installments = maxinst >= 1 ? $"{currentInst + currentInstallmentOffset + 1} / {maxinst + 1}" : "";
            }
            //Si es un servicio, uso la columna installments para notificar que esta transaccion es un servicio
            else if (detail.Transaction.TransactionType == TransactionType.Service)
            {
                installments = "servicio";
            }
            return installments;
        }

        private int UpdateSingle(TransactionViewDto detailDto)
        {
            var detail = GetDetailModelById(detailDto.Id);
            //Actualizo el detalle
            detail.Date = detailDto.Date;
            detail.EndDate = detailDto.Date;
            detail.Amount = detailDto.Amount;
            new TransactionDetailService(_databasePath).Update(detail);
            //Indico que hay que hacer focus en esta transaccion modificada
            return detailDto.Id;
        }

        private int UpdateInstallment(TransactionViewDto detailDto)
        {
            //Inicializo el servicio
            var service = new TransactionDetailService(_databasePath);
            //Obtengo el detalle inicial
            var detail = service.Get(detailDto.Id);
            var initDetailDate = detail.Transaction.TransactionDetails.OrderBy(x => x.Date).Select(x => x.Date).FirstOrDefault();
            //Obtengo la fecha del detalle actual
            var currentDetailDate = detailDto.Date;
            //Calculo la diferencia de meses y actualizo la fecha incial de las cuotas
            var difference = DateTimeTools.GetMonthDifference(currentDetailDate, detailDto.Date);
            var initDate = initDetailDate.AddMonths(difference);
            var dateToCompare = new DateTime(initDate.Year, initDate.Month, initDate.Day > 28 ? 28 : initDate.Day);
            //Actualizo las fechas de cada cuota
            foreach (var details in detail.Transaction.TransactionDetails.OrderBy(x => x.Date))
            {
                //Calculo la diferencia entre fechas y me dedico a sumar meses a las fechas originales
                difference = DateTimeTools.GetMonthDifference(details.Date, dateToCompare);
                var newDate = details.Date.AddMonths(difference);
                var newEndDate = details.EndDate.AddMonths(difference);
                newEndDate = new DateTime(newEndDate.Year, newEndDate.Month, dateToCompare.Day);
                //Actualizo el detalle
                details.Date = new DateTime(newDate.Year, newDate.Month, dateToCompare.Day);
                details.EndDate = newEndDate;
                details.Amount = detailDto.Amount;
                new TransactionDetailService(_databasePath).Update(details);
                //guardo la nueva fecha inicial de la proxima cuota
                dateToCompare = newEndDate.AddMonths(1);
            }
            return detailDto.Id;
        }

        private int UpdateService(TransactionViewDto detailDto, bool overrideNext)
        {
            //Inicializo el servicio
            var service = new TransactionDetailService(_databasePath);
            string errorMessage = "Ocurrió un error al actualizar el servicio";
            //var detail = transactionDetailService.Get(detailDto.Id);
            var detail = service.Get(detailDto.Id);
            var details = detail.Transaction.TransactionDetails.OrderByDescending(x => x.Date).ToList();
            var current = details.Where(x => x.Date.Date <= detailDto.Date.Date).FirstOrDefault();
            var futureDetails = details.Where(x => x.Date.Date > detailDto.Date.Date).ToList();
            var endDate = DateTime.MaxValue;
            //Manejo posibles errores
            if (current is null)
                throw new NullReferenceException(errorMessage);
            if (futureDetails is null)
                throw new NullReferenceException(errorMessage);
            if (futureDetails.Count > 0)
                if (overrideNext)
                {
                    //Elimino los detalles futuros
                    foreach (var futureDetail in futureDetails)
                    {
                        service.Delete(futureDetail);
                    }
                }
                else
                {
                    if (current.Frequency != detailDto.Frequency)
                        throw new Exception("No es posible actualizar este servicio ya que cambiaste la frecuencia del mismo y existen detalles futuros vinculados, " +
                            "se recomienda cambiarán todos los detalles futuros vinculados.");
                    var futureDetail = futureDetails.LastOrDefault();
                    endDate = futureDetail.Date.AddMonths(-current.Frequency);
                }
            if (current.Date.Date == detailDto.Date)
            {
                current.Date = detailDto.Date;
                current.EndDate = endDate;
                current.Amount = detailDto.Amount;
                current.Frequency = detailDto.Frequency;
                service.Update(current);
                return current.Id;
            }
            else
            {
                //Actualizo la transaccion que se volveria el ultimo antes de actualizar
                current.EndDate = detailDto.Date.AddMonths(-current.Frequency);
                service.Update(current);
                //Creo la transaccion nueva
                var newDetail = new TransactionDetail
                {
                    TransactionId = current.TransactionId,
                    Date = detailDto.Date,
                    EndDate = endDate,
                    Amount = detailDto.Amount,
                    Frequency = detailDto.Frequency,
                    Concider = true,
                    Paid = false,
                };
                service.Insert(newDetail);
                return newDetail.Id;
            }
        }
    }
}
