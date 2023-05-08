using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services.Controllers
{
    public class CreditCardPayController
    {
        private string _databasePath;

        public CreditCardPayController(string databasePath)
        {
            _databasePath = databasePath;
        }

        public TransactionDetail GetTransactionDetail(int transactionDetailId)
        {
            return new TransactionDetailService(_databasePath).Get(transactionDetailId);
        }

        public CCSummary GetSummaryById(int id)
        {
            return new CCSummaryService(_databasePath).Get(id);
        }
    }
}
