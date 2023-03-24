using MoneyAdministrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class TransactionHistoryPresenter
    {
        private ITransactionHistoryView _transactionHistoryView;
        private string _databasePath;

        public TransactionHistoryPresenter(ITransactionHistoryView transactionHistoryView, string databasePath) 
        {
            _transactionHistoryView = transactionHistoryView;
            _databasePath = databasePath;
        }
    }
}
