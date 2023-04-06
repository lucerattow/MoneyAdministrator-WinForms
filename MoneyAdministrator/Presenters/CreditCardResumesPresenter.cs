using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Views;
using MoneyAdministrator.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class CreditCardResumesPresenter
    {
        //fields
        private ICreditCardResumesView _view;
        private string _databasePath;
        private Action _closeView;

        //properties
        public ICreditCardResumesView View
        {
            get { return _view; }
        }

        public CreditCardResumesPresenter(string databasePath, Action closeView)
        {
            _view = new CreditCardResumesView();
            _databasePath = databasePath;
            _closeView = closeView;
        }
    }
}
