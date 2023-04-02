using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MoneyAdministrator.Interfaces;
using System.CodeDom;

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
            _btnTransactions.Click += delegate
            {
                ShowTransactionHistory?.Invoke(this, EventArgs.Empty);
            };
            _tsbFileNew.Click += delegate
            {
                FileNew?.Invoke(this, EventArgs.Empty);
            };
            _tsbFileOpen.Click += delegate
            {
                FileOpen?.Invoke(this, EventArgs.Empty);
            };
            _tsbFileClose.Click += delegate
            {
                FileClose?.Invoke(this, EventArgs.Empty);
            };
        }

        private void ButtonsLogic()
        {
            _tsbFileClose.Enabled = _isFileOpened;
            _btnDashboard.Enabled = _isFileOpened;
            _btnTransactions.Enabled = _isFileOpened;
            _btnCreditCards.Enabled = _isFileOpened;
        }

        //Events
        public event EventHandler ShowTransactionHistory;
        public event EventHandler FileNew;
        public event EventHandler FileOpen;
        public event EventHandler FileClose;
    }
}