using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.CustomControls
{
    public class MoneyTextBox : TextBox
    {
        private string _operatorSymbol;
        private bool _colored;

        public string OperatorSymbol 
        { 
            get => _operatorSymbol; 
            set 
            {
                if (value == "+" || value == "-")
                {
                    _operatorSymbol = value;
                    this_TextChanged(this, EventArgs.Empty);
                }
            } 
        }

        public bool Colored
        {
            get => _colored;
            set => _colored = value;
        }

        public MoneyTextBox()
        {
            this.Text = "0.00 $";
            this.TextAlign = HorizontalAlignment.Right;
            _operatorSymbol = "-";
            this.Click += this_Click;
            this.Enter += this_Enter;
            this.KeyDown += this_KeyDown;
            this.KeyPress += this_KeyPress;
            this.TextChanged += this_TextChanged;
        }

        private void this_Click(object sender, EventArgs e)
        {
            this.Select(this.Text.Length - 2, 0);
        }

        private void this_Enter(object sender, EventArgs e)
        {
            this.Select(this.Text.Length - 2, 0);
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            //Al precionar delete, lo cambio por un backspace
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true; // Indica que ya se ha manejado el evento
                e.SuppressKeyPress = true; // Evita que el evento KeyPress se dispare

                // Realiza la lógica de la tecla Backspace
                int cursorPosition = this.SelectionStart;
                if (cursorPosition > 0)
                {
                    this.Text = this.Text.Remove(cursorPosition - 1, 1);
                    this.SelectionStart = cursorPosition - 1;
                }
            }
            // Hacer handle de los caracteres "-" y "+"
            if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                    _operatorSymbol = "-";
                else
                    _operatorSymbol = "+";

                this_TextChanged(sender, EventArgs.Empty);
                e.Handled = true; // Indica que ya se ha manejado el evento
                e.SuppressKeyPress = true; // Evita que el evento KeyPress se dispare
            }
        }

        private void this_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void this_TextChanged(object sender, EventArgs e)
        {
            this.Text = FormatCurrency(this.Text);
            this.Select(this.Text.Length - 2, 0);
            Colorize();
        }

        private string FormatCurrency(string input)
        {
            var numbers = new string(input.Where(char.IsDigit).ToArray());

            decimal value = 0;
            if (!string.IsNullOrEmpty(numbers) && decimal.TryParse(numbers, out value))
                value = value != 0 ? value / 100 : 0;

            return _operatorSymbol + value.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")); ;
        }

        private void Colorize()
        {
            if (_colored)
            { 
                if (_operatorSymbol == "-")
                    this.ForeColor = Color.FromArgb(150, 0, 0);
                else if (_operatorSymbol == "+")
                    this.ForeColor = Color.Green;

                var numbers = new string(this.Text.Where(char.IsDigit).ToArray());
                decimal value = decimal.Parse(numbers) / 100;
                if (value == 0)
                    this.ForeColor = Color.FromArgb(80, 80, 80);
            }
        }
    }
}
