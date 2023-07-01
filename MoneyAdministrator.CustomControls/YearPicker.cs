using System.ComponentModel;
using System.Data;

namespace MoneyAdministrator.CustomControls
{
    public partial class YearPicker : UserControl
    {
        //fields
        private int _value = DateTime.Now.Year;
        private List<int> _years = new List<int>();
        private int _height = 25;

        private string _buttonNextText = ">";
        private string _buttonPreviousText = "<";
        private Image? _buttonNextImage = null;
        private Image? _buttonPreviousImage = null;

        //properties
        public Image? ButtonNextImage
        {
            get => _buttonNextImage;
            set
            {
                _buttonNextImage = value;
                ButtonsIconSet();
            }
        }
        public Image? ButtonPreviousImage
        {
            get => _buttonPreviousImage;
            set
            {
                _buttonPreviousImage = value;
                ButtonsIconSet();
            }
        }
        public List<int> AvailableYears
        {
            get => _years;
            set
            {
                _years = value;

                if (_years.Count > 0 && !_years.Contains(Value))
                    Value = _years.FirstOrDefault();
                else if (_years.Count == 0)
                    _years.Add(Value);

                YearsSort();
                ButtonsSetEnabled();
            }
        }
        public int Value
        {
            get => _value;
            set
            {
                _value = value;

                if (!_years.Contains(_value))
                {
                    _years.Add(_value);
                    YearsSort();
                }
                _btnYearPicker.Text = value.ToString();
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

        //setteable events
        public event EventHandler ButtonNextClick;
        public event EventHandler ButtonPreviousClick;
        public event EventHandler ValueChange;

        public YearPicker()
        {
            InitializeComponent();

            this.MaximumSize = new Size(99999, _height);
            this.MinimumSize = new Size(90, _height);
            this._btnYearPicker.Text = _value.ToString();

            ButtonsIconSet();
            ButtonsSetEnabled();
            AssosiateEvents();
        }

        //methods
        private void YearsSort() => _years = _years.OrderByDescending(x => x).ToList();

        private void AssosiateEvents()
        {
            _btnPrevius.Click += delegate
            {
                ButtonPreviousClick?.Invoke(this, EventArgs.Empty);
            };
            _btnNext.Click += delegate
            {
                ButtonNextClick?.Invoke(this, EventArgs.Empty);
            };
            _btnYearPicker.TextChanged += delegate
            {
                ValueChange?.Invoke(this, EventArgs.Empty);
            };
        }

        private void ButtonsIconSet()
        {
            if (_buttonPreviousImage != null)
            {
                _btnPrevius.Image = _buttonPreviousImage;
                _btnPrevius.Text = "";
            }
            else
            {
                _btnPrevius.Image = null;
                _btnPrevius.Text = _buttonPreviousText;
            }

            if (_buttonNextImage != null)
            {
                _btnNext.Image = _buttonNextImage;
                _btnNext.Text = "";
            }
            else
            {
                _btnNext.Image = null;
                _btnNext.Text = _buttonNextText;
            }
        }

        private void ButtonsSetEnabled()
        {
            if (_years.Count == 0)
            {
                _btnPrevius.Enabled = false;
                _btnNext.Enabled = false;
                return;
            }

            //Compruebo si debo habilitar el boton Previous
            if (_years.OrderByDescending(x => x).LastOrDefault() == _value)
            {
                _btnPrevius.Enabled = false;
            }
            else
            {
                _btnPrevius.Enabled = true;
            }

            //Compruebo si debo habilitar el boton Next
            if (_years.OrderByDescending(x => x).First() == _value)
            {
                _btnNext.Enabled = false;
            }
            else
            {
                _btnNext.Enabled = true;
            }
        }

        //functions
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

        //events
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
            _btnYearPicker.Text = (sender as ToolStripMenuItem).Text;
        }

    }
}
