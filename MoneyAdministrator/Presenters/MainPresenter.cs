using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Services;
using MoneyAdministrator.Views;
using System.Configuration;

namespace MoneyAdministrator.Presenters
{
    public class MainPresenter
    {
        private IMainView _mainView;
        private string _databasePath;

        public MainPresenter(IMainView mainView)
        {
            this._mainView = mainView;
            this._mainView.ShowTransactionHistory += ShowTransactionHistory;
            this._mainView.FileNew += FileNew;
            this._mainView.FileOpen += FileOpen;
            this._mainView.FileClose += FileClose;
            this._mainView.IsFileOpened = false;
        }

        #region events
        private void ShowTransactionHistory(object? sender, EventArgs e)
        {
            ITransactionHistoryView transactionHistoryView = new TransactionHistoryView();
            var transactionHistoryPresenter = new TransactionHistoryPresenter(transactionHistoryView, _databasePath);
            this._mainView.OpenChildren((UserControl)transactionHistoryView);
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
                    this._mainView.IsFileOpened = true;
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
                    this._mainView.IsFileOpened = true;
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
            this._mainView.CloseChildrens();
            this._mainView.IsFileOpened = false;
        }
        #endregion
    }
}
