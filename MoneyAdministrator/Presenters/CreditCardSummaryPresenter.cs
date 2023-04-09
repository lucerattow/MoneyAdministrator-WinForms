using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Module.ImportHsbcSummary;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views;
using MoneyAdministrator.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class CreditCardSummaryPresenter
    {
        //fields
        private ICreditCardSummaryView _view;
        private string _databasePath;
        private Action _closeView;

        //properties
        public ICreditCardSummaryView View
        {
            get { return _view; }
        }

        public CreditCardSummaryPresenter(string databasePath, Action closeView)
        {
            _view = new CreditCardResumesView();
            _databasePath = databasePath;
            _closeView = closeView;

            AssosiateEvents();
        }

        //methods
        private void AssosiateEvents()
        {
            _view.ButtonImportClick += ButtonImportClick;
        }

        //events
        private void ButtonImportClick(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog()
            {
                Filter = $"{ConfigurationManager.AppSettings["AppTitle"]} Credit Card Summary (*.pdf)|*.pdf",
                Title = $"Abrir {ConfigurationManager.AppSettings["AppTitle"]} Credit Card Summary",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (new CursorWait())
                {
                    try
                    {
                        var creditCardSummaryDto = Import.GetDataFromPdf(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message, ConfigurationManager.AppSettings["AppTitle"],
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
