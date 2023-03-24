using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Services;
using MoneyAdministrator.Views;
using System.Configuration;

namespace MoneyAdministrator.Presenters
{
    public class MainPresenter
    {
        private IMainView mainView;
        private string _databasePath;

        public MainPresenter(IMainView mainView)
        {
            this.mainView = mainView;
            this.mainView.ShowTransactionHistory += ShowTransactionHistory;
            this.mainView.FileNew += FileNew;
            this.mainView.FileOpen += FileOpen;
            this.mainView.FileClose += FileClose;
            this.mainView.IsFileOpened = false;
        }

        //Events
        private void ShowTransactionHistory(object? sender, EventArgs e)
        {
            ITransactionHistoryView transactionHistoryView = new TransactionHistoryView();
            var transactionHistoryPresenter = new TransactionHistoryPresenter(transactionHistoryView, _databasePath);
            this.mainView.OpenChildren((UserControl)transactionHistoryView);
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
                    this.mainView.IsFileOpened = true;
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
                    this.mainView.IsFileOpened = true;
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
            this.mainView.CloseChildrens();
            this.mainView.IsFileOpened = false;
        }
    }
}
