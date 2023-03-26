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
        private int _selectedId = 0;

        public event EventHandler FormClosed;
        public bool TopMost { get; set; }

        public ResultPickerView()
        {
            InitializeComponent();
            AssosiateEvents();
            ControlConfig.DataGridViewSetup(_grd);

            this.Text = $"{ConfigurationManager.AppSettings["AppTitle"]}";
        }

        #region methdos
        //private
        private void AssosiateEvents()
        {
            this.FormClosed += delegate
            {
                FormClosed?.Invoke(this, EventArgs.Empty);
            };
        }
        private void ButtonsLogic()
        {
            _tsbInsert.Enabled = _selectedId == 0;
            _tsbUpdate.Enabled = _selectedId != 0;
            _tsbDelete.Enabled = _selectedId != 0;
        }
        //public
        public void GrdRefreshData<T>(List<T> dataSource) where T : class
        {
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
        #endregion
    }
}
