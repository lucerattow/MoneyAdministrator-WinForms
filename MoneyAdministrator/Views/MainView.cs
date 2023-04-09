using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MoneyAdministrator.Interfaces;
using System.CodeDom;
using System.Configuration;

namespace MyMoneyAdmin
{
    public partial class MainView : Form, IMainView
    {
        //Fields
        private bool _isFileOpened;

        //Properties
        public bool IsFileOpened
        {
            get => _isFileOpened;
            set
            { 
                _isFileOpened = value;
                ButtonsLogic();
            }
        }

        public MainView()
        {
            //metodos
            InitializeComponent();
            AssociateEvents();

            //configurations
            this.Height = Screen.PrimaryScreen.Bounds.Height - 200;
            this.Width = Screen.PrimaryScreen.Bounds.Width - 200;
            this.Text = ConfigurationManager.AppSettings["AppTitle"];
        }

        //methods
        public void OpenChildren(UserControl children)
        {
            //Controlo que el nuevo children no este abierto actualmente
            foreach (var control in _pnlContainer.Controls)
                if (control.GetType() == children.GetType())
                    return;

            _pnlContainer.Controls.Clear();
            _pnlContainer.Controls.Add(children);
        }

        public void CloseChildrens()
        {
            _pnlContainer.Controls.Clear();
        }

        private void AssociateEvents()
        {
            _btnDashboard.Click += (sender, e) => ShowDashboard?.Invoke(sender, e);
            _btnTransactions.Click += (sender, e) => ShowTransactionHistory?.Invoke(sender, e);
            _btnCreditCards.Click += (sender, e) => ShowCreditCardSummary?.Invoke(sender, e);

            _tsbFileNew.Click += (sender, e) => FileNew?.Invoke(sender, e);
            _tsbFileOpen.Click += (sender, e) => FileOpen?.Invoke(sender, e);
            _tsbFileClose.Click += (sender, e) => FileClose?.Invoke(sender, e);
        }

        private void ButtonsLogic()
        {
            _tsbFileClose.Enabled = _isFileOpened;
            _btnDashboard.Enabled = _isFileOpened;
            _btnTransactions.Enabled = _isFileOpened;
            _btnCreditCards.Enabled = _isFileOpened;
        }

        //Events
        public event EventHandler ShowDashboard;
        public event EventHandler ShowTransactionHistory;
        public event EventHandler ShowCreditCardSummary;
        public event EventHandler FileNew;
        public event EventHandler FileOpen;
        public event EventHandler FileClose;
    }
}