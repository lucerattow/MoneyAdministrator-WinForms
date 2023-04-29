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
            SetSelectedButton();
        }

        public void SelectDashboardButton()
        {
            SetSelectedButton();
            _btnDashboard.BackColor = Color.Gainsboro;
        }

        private void AssociateEvents()
        {
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

        private void SetSelectedButton(Button button = null)
        {
            //backcolor
            _btnDashboard.BackColor = Color.White;
            _btnTransactions.BackColor = Color.White;
            _btnCreditCards.BackColor = Color.White;

            if (button != null)
                button.BackColor = Color.Gainsboro;
        }

        //Events
        private void _btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard?.Invoke(sender, e);
            SetSelectedButton((Button)sender);
        }

        private void _btnTransactions_Click(object sender, EventArgs e)
        {
            ShowTransactionHistory?.Invoke(sender, e);
            SetSelectedButton((Button)sender);
        }

        private void _btnCreditCards_Click(object sender, EventArgs e)
        {
            ShowCreditCardSummary?.Invoke(sender, e);
            SetSelectedButton((Button)sender);
        }

        public event EventHandler ShowDashboard;
        public event EventHandler ShowTransactionHistory;
        public event EventHandler ShowCreditCardSummary;
        public event EventHandler FileNew;
        public event EventHandler FileOpen;
        public event EventHandler FileClose;
    }
}