using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MoneyAdministrator.Interfaces;
using System.CodeDom;

namespace MyMoneyAdmin
{
    public partial class MainView : Form, IMainView
    {
        //Fields
        private bool _isFileOpened;

        //Events
        public event EventHandler ShowTransactionHistory;
        public event EventHandler FileNew;
        public event EventHandler FileOpen;
        public event EventHandler FileClose;

        //Properties
        public bool IsFileOpened
        {
            get => _isFileOpened;
            set
            { 
                _isFileOpened = value;

                //Cambio el estado de los botones
                _tsbFileClose.Enabled = value;
                _btnTransactions.Enabled = value;
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

        public void CloseChildrens()
        {
            _pnlContainer.Controls.Clear();
        }

        public void OpenChildren(UserControl children)
        {
            //Controlo que el nuevo children no este abierto actualmente
            foreach (var control in _pnlContainer.Controls)
                if (control.GetType() == children.GetType())
                    return;

            _pnlContainer.Controls.Clear();
            _pnlContainer.Controls.Add(children);
        }

        //Todo comentado:
        #region events
        private void TsbFileNew_Click(object sender, EventArgs e)
        {
            //if (_dbController.CheckDataBaseHasChanges())
            //{
            //    //var dialog = MessageBox.Show("Tienes cambios pendientes sin guardar, desea guardarlos antes de crear un nuevo archivo?",
            //    //    GlobalConfigs.appName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            //    //if (dialog == DialogResult.Yes)
            //    //    _dbController.SaveDataBase();
            //    //if (dialog == DialogResult.Cancel)
            //    //    return;
            //}

            //SaveFileDialog o = new SaveFileDialog();
            //o.Filter = $"{Logic.GlobalConfigs.appName} Database File (*.mmdf)|*.mmdf";
            //o.Title = $"Guardar {Logic.GlobalConfigs.appName} Database File";
            //o.ShowDialog();

            //if (o.FileName != "")
            //{
            //    try
            //    {
            //        //Creo y cargo el archivo de base de datos
            //        _dbController.CreateDataBase(o.FileName);
            //        _dbController.OpenDataBase(o.FileName);

            //        _openedFilePath = Path.GetFileName(o.FileName);
            //        ButtonsSetEnabled();
            //        HomeTitleUpdate(_openedFilePath);

            //        MessageBox.Show("Archivo creado correctamente!", Logic.GlobalConfigs.appName,
            //            MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Ocurrio un error al crear la base de datos:\n{ex.Message}", Logic.GlobalConfigs.appName,
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
        private void TsbFileConnect_Click(object sender, EventArgs e)
        {
            //OpenFileDialog o = new();
            //o.Filter = $"{Logic.GlobalConfigs.appName} Database File (*.mmdf)|*.mmdf";
            //o.Title = $"Connect to {Logic.GlobalConfigs.appName} Database File";

            //if (o.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        _dbController.OpenDataBase(o.FileName);

            //        _openedFilePath = Path.GetFileName(o.FileName);
            //        ButtonsSetEnabled();
            //        HomeTitleUpdate();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        private void TsbFileClose_Click(object sender, EventArgs e)
        {
            //if (_dbController.CheckDataBaseHasChanges())
            //{
            //    var dialog = MessageBox.Show("Tienes cambios pendientes sin guardar, desea guardarlos antes de cerrar el archivo?",
            //        GlobalConfigs.appName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            //    if (dialog == DialogResult.Yes)
            //        _dbController.SaveDataBase();
            //    if (dialog == DialogResult.Cancel)
            //        return;
            //}

            //_dbController.CloseDataBase();
            //_openedFilePath = "";
            //CloseChildrens();
            //ButtonsSetEnabled();
            //HomeTitleUpdate();
        }
        private void TmHaveChanges_Tick(object sender, EventArgs e)
        {
            //if (_dbController.CheckDataBaseHasChanges())
            //    HomeTitleUpdate(hasChanges: true);
        }
        #endregion
    }
}