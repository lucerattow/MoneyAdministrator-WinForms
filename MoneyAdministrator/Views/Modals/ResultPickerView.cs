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
        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonDeleteClick;
        public event EventHandler ButtonClearClick;

        public ResultPickerView()
        {
            InitializeComponent();
            AssosiateEvents();
            ButtonsLogic();

            this.Text = $"{ConfigurationManager.AppSettings["AppTitle"]} : ";

            //Configuro el Grd
            ControlConfig.DataGridViewSetup(_grd);
        }

        //public methods
        public void GrdRefreshData<T>(List<T> dataSource) where T : class
        {
            this.Text += typeof(T).ToString();
            _grd.Visible = false;

            _grd.DataSource = dataSource;
            _grd.AutoGenerateColumns = true;

            if (dataSource != null && dataSource.Any())
            {
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    // Ocultar columnas en función del atributo Display
                    var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                    if (displayAttribute != null && !displayAttribute.AutoGenerateField)
                    {
                        if (_grd.Columns.Contains(property.Name))
                        {
                            _grd.Columns[property.Name].Visible = false;
                        }
                    }

                    // Ajustar el ancho de las columnas en función del atributo StringLength
                    var stringLengthAttribute = property.GetCustomAttribute<StringLengthAttribute>();
                    if (stringLengthAttribute != null)
                    {
                        if (_grd.Columns.Contains(property.Name))
                        {
                            int characterWidth = 8; // Estimación aproximada del ancho de un carácter en píxeles
                            int padding = 20; // Espaciado adicional para la columna
                            _grd.Columns[property.Name].Width = stringLengthAttribute.MaximumLength * characterWidth + padding;
                        }
                    }
                }
            }

            _grd.Visible = true;
        }
        public void ButtonsLogic()
        {
            _tsbInsert.Enabled = _selectedId == 0;
            _tsbUpdate.Enabled = _selectedId != 0;
            _tsbDelete.Enabled = _selectedId != 0;
        }

        //private methods
        private void AssosiateEvents()
        {
            _grd.CellMouseDoubleClick += (sender, e) => GrdDoubleClick?.Invoke(sender, e);
            _tsbSelect.Click += (sender, e) => ButtonSelectClick?.Invoke(sender, e);
            _tsbSearch.Click += (sender, e) => ButtonSearchClick?.Invoke(sender, e);
            _tsbInsert.Click += (sender, e) => ButtonInsertClick?.Invoke(sender, e);
            _tsbUpdate.Click += (sender, e) => ButtonUpdateClick?.Invoke(sender, e);
            _tsbDelete.Click += (sender, e) => ButtonDeleteClick?.Invoke(sender, e); 
            _tsbClear.Click += (sender, e) => ButtonClearClick?.Invoke(sender, e); 
        }
    }
}
