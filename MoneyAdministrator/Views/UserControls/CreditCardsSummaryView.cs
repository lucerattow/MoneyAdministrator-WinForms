using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MoneyAdministrator.Views
{
    public partial class CreditCardResumesView : UserControl, ICreditCardsSummaryView
    {
        //fields
        private int _selectedId = 0;
        private const int _colWidthDate = 90;
        private const int _colWidthEntity = 210;
        private const int _colWidthInstall = 60;
        private const int _colWidthCurrency = 70;
        private const int _colWidthAmount = 120;
        private const int _colWidthTotal = _colWidthDate + _colWidthEntity + _colWidthInstall + _colWidthCurrency + _colWidthAmount;

        //properties
        public int SelectedId
        {
            get => _selectedId;
            set => _selectedId = value;
        }

        public CreditCardResumesView()
        {
            this.Visible = false;

            using (new CursorWait())
            {
                Dock = DockStyle.Fill;
                InitializeComponent();

                AssosiateEvents();
            }

            //Muestro la ventana ya cargada
            this.Visible = true;
        }

        //methods
        private void AssosiateEvents()
        {
            _tsbImport.Click += (sender, e) => ButtonImportClick?.Invoke(sender, e);
        }

        //events
        public event EventHandler ButtonImportClick;
    }
}
