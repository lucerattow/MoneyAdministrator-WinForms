using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Services;
using MoneyAdministrator.Views;
using MyMoneyAdmin;
using System.Configuration;

namespace MoneyAdministrator.Presenters
{
    public class MainPresenter
    {
        //fields
        private readonly IMainView _view;
        private string _databasePath;

        //properties
        public IMainView View
        {
            get { return _view; }
        }

        public MainPresenter()
        {
            _view = new MainView();
            _view.IsFileOpened = false;
            AssosiateEvents();
        }

        //methods
        private void AssosiateEvents()
        {
            _view.ShowTransactionHistory += ShowTransactionHistory;
            _view.FileNew += FileNew;
            _view.FileOpen += FileOpen;
            _view.FileClose += FileClose;
        }

        //events
        private void ShowTransactionHistory(object? sender, EventArgs e)
        {
            var transactionHistoryPresenter = new TransactionHistoryPresenter(_databasePath, _view.CloseChildrens);
            this._view.OpenChildren((UserControl)transactionHistoryPresenter.View);
        }

        private void FileNew(object? sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog()
            {
                Filter = $"{ConfigurationManager.AppSettings["AppTitle"]} Database File (*.mmdf)|*.mmdf",
                Title = $"Guardar {ConfigurationManager.AppSettings["AppTitle"]} Database File",
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                try
                {
                    _databasePath = saveFileDialog.FileName;
                    DbFileService.CreateDatabase(_databasePath);
                    this._view.IsFileOpened = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, ConfigurationManager.AppSettings["AppTitle"],
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FileOpen(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog()
            {
                Filter = $"{ConfigurationManager.AppSettings["AppTitle"]} Database File (*.mmdf)|*.mmdf",
                Title = $"Abrir {ConfigurationManager.AppSettings["AppTitle"]} Database File",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _databasePath = openFileDialog.FileName;
                    this._view.IsFileOpened = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, ConfigurationManager.AppSettings["AppTitle"],
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FileClose(object? sender, EventArgs e) 
        {
            _databasePath = "";
            this._view.CloseChildrens();
            this._view.IsFileOpened = false;
        }
    }
}
