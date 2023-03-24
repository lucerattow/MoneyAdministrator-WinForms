using Microsoft.VisualBasic.Logging;
using MoneyAdministrator.Services;
using MoneyAdministrator.Views.Interfaces;
using MyMoneyAdmin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MoneyAdministrator.Presenters
{
    public class MainPresenter
    {
        private string _databasePath;

        private IMainView mainView;

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
            //OpenChildren(new Views.UsTransacctions(_dbController));
            //IPetView view = PetView.GetInstance((Form)mainView);
            //IPetRepository repository = new PetRepository(connectionString);
            //new PetPresenter(view, repository);
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
