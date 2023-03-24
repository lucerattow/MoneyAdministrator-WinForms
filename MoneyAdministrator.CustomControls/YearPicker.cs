using System.ComponentModel;
using System.Data;

namespace MoneyAdministrator.CustomControls
{
    public partial class YearPicker : UserControl
    {
        //General Fields
        private int _value = DateTime.Now.Year;
        private List<int> _years = new List<int>();
        private int _height = 25;

        //Buttons Fields
        private string _previousButtonText = "<";
        private string _nextButtonText = ">";
        private Image? _buttonPreviousImage = null;
        private Image? _buttonNextImage = null;

        //Properties
        public Image? ButtonPreviousImage
        { 
            get
            {
                return _buttonPreviousImage;
            }
            set
            {
                _buttonPreviousImage = value;
                ButtonsIconSet();
                //this.Invalidate();
            }
        }

        public Image? ButtonNextImage
        {
            get
            {
                return _buttonNextImage;
            }
            set
            {
                _buttonNextImage = value;
                ButtonsIconSet();
            }
        }

        public List<int> AvailableYears
        {
            get { return _years; }
            set { 
                _years = value;
                YearsSort();
                ButtonsSetEnabled();
            }
        }

        public int Value
        { 
            get { return _value; }
            set 
            { 
                _value = value;

                if (!_years.Contains(_value))
                {
                    _years.Add(_value);
                    YearsSort();
                }
                BtnYearPicker.Text = value.ToString();
                ButtonsSetEnabled();
            }
        }

        #region Hide Properties

        [Browsable(false)]
        public new bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        [Browsable(false)]
        public new AutoSizeMode AutoSizeMode
        {
            get { return base.AutoSizeMode; }
            set { base.AutoSizeMode = value; }
        }

        [Browsable(false)]
        public new Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        [Browsable(false)]
        public new DockStyle Dock
        { 
            get { return base.Dock; }
            set { base.Dock = value; }
        }

        [Browsable(false)]
        public new Size MaximumSize
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        [Browsable(false)]
        public new Size MinimumSize
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
        }

        [Browsable(false)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        #endregion

        public YearPicker()
        {
            InitializeComponent();

            this.MaximumSize = new Size(99999, _height);
            this.MinimumSize = new Size(90, _height);
            this.BtnYearPicker.Text = _value.ToString();

            ButtonsIconSet();
            ButtonsSetEnabled();
        }

        //Events
        private void BtnPrevius_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _years.Count; i++)
            {
                if (_years[i] == Value) 
                {
                    if (i + 1 < _years.Count)
                    {
                        Value = _years[i + 1];
                    }
                    break;
                }
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _years.Count; i++)
            {
                if (_years[i] == Value)
                {
                    if (i - 1 >= 0)
                    {
                        Value = _years[i - 1];
                    }
                    break;
                }
            }
        }

        private void BtnYearPicker_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            var contextMenu = CreateContextMenu();
            contextMenu.Width = button.Width;

            //Cambio la posicion en la que se abre el menu contextual
            Point menuLocation = new Point(button.Left, button.Bottom);
            menuLocation = button.Parent.PointToScreen(menuLocation);
            contextMenu.Show(menuLocation);
        }

        private void BtnYearPicker_TextChanged(object sender, EventArgs e)
        {
            Value = int.Parse((sender as Button).Text);
        }

        private void TbItem_Click(object sender, EventArgs e)
        {
            BtnYearPicker.Text = (sender as ToolStripMenuItem).Text;
        }

        //Methods
        private void YearsSort() => 
            _years = _years.OrderByDescending(x => x).ToList();

        private void ButtonsIconSet()
        {
            if (_buttonPreviousImage != null)
            {
                BtnPrevius.Image = _buttonPreviousImage;
                BtnPrevius.Text = "";
            }
            else
            {
                BtnPrevius.Image = null;
                BtnPrevius.Text = _previousButtonText;
            }

            if (_buttonNextImage != null)
            {
                BtnNext.Image = _buttonNextImage;
                BtnNext.Text = "";
            }
            else
            {
                BtnNext.Image = null;
                BtnNext.Text = _nextButtonText;
            }
        }

        private void ButtonsSetEnabled()
        {
            if (_years.Count == 0)
            {
                BtnPrevius.Enabled = false;
                BtnNext.Enabled = false;
                return;
            }

            //Compruebo si debo habilitar el boton Previous
            if (_years.OrderByDescending(x => x).LastOrDefault() == _value)
            {
                BtnPrevius.Enabled = false;
            }
            else
            {
                BtnPrevius.Enabled = true;
            }

            //Compruebo si debo habilitar el boton Next
            if (_years.OrderByDescending(x => x).First() == _value)
            {
                BtnNext.Enabled = false;
            }
            else
            {
                BtnNext.Enabled = true;
            }
        }

        //Functions
        private ContextMenuStrip CreateContextMenu()
        {
            ContextMenuStrip contextMenu = new();

            //Genero los items
            List<ToolStripItem> TbItems = new();
            if (!_years.Contains(_value))
            {
                ToolStripMenuItem TsItem = new ToolStripMenuItem(_value.ToString());
                TsItem.Click += TbItem_Click;
                TbItems.Add(TsItem);
            }
            foreach (var year in _years.OrderByDescending(x => x))
            {
                ToolStripMenuItem TsItem = new ToolStripMenuItem(year.ToString());
                TsItem.Click += TbItem_Click;
                TbItems.Add(TsItem);
            }

            //Añado los items al menu contextual
            contextMenu.Items.AddRange(TbItems.OrderByDescending(x => x.Text).ToArray());

            return contextMenu;
        }
    }
}
