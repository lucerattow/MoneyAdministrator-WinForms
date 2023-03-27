using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;

namespace MoneyAdministrator.Views.Modals
{
    public partial class ResultPickerView : Form, IResultPickerView
    {
        //fields
        private int _selectedId = 0;

        //properties
        public int SelectedId
        { 
            get => _selectedId;
            set => _selectedId = value;
        }
        public string SelectedName
        {
            get => _txtName.Text;
            set => _txtName.Text = value;
        }
        public new DialogResult DialogResult
        {
            get => base.DialogResult;
            set => base.DialogResult = value;
        }

        //public events
        public event EventHandler<DataGridViewCellMouseEventArgs> GrdDoubleClick;
        public event EventHandler ButtonSelectClick;
        public event EventHandler ButtonSearchClick;
        public event EventHandler ButtonClearClick;
        public event EventHandler TxtNameTextChange;

        public ResultPickerView()
        {
            InitializeComponent();
            AssosiateEvents();
            ButtonsLogic();

            this.Text = $"{ConfigurationManager.AppSettings["AppTitle"]} : Seleccionar registro";

            //Configuro el Grd
            ControlConfig.DataGridViewSetup(_grd);
        }

        //public methods
        public void GrdRefreshData(List<ResultPickerViewDto> dataSource)
        {
            _grd.Visible = false;

            _grd.DataSource = dataSource;
            _grd.AutoGenerateColumns = true;

            _grd.Columns[0].Visible = false;
            _grd.Columns[1].Width = 400;

            _grd.Visible = true;
        }
        public void ButtonsLogic()
        { 
            _tsbSelect.Enabled = _selectedId > 0;
        }

        //private methods
        private void AssosiateEvents()
        {
            _grd.CellMouseDoubleClick += (sender, e) => GrdDoubleClick?.Invoke(sender, e);
            _tsbSelect.Click += (sender, e) => ButtonSelectClick?.Invoke(sender, e);
            _tsbSearch.Click += (sender, e) => ButtonSearchClick?.Invoke(sender, e);
            _tsbClear.Click += (sender, e) => ButtonClearClick?.Invoke(sender, e);
            _txtName.TextChanged += (sender, e) => TxtNameTextChange?.Invoke(sender, e);
        }
    }
}
