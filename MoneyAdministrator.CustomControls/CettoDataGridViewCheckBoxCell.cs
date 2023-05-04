using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.CustomControls
{
    public class CettoDataGridViewCheckBoxCell : DataGridViewCheckBoxCell
    {
        private Image _checkedImage;
        private Image _uncheckedImage;

        public Image CheckedImage
        { 
            get => _checkedImage;
            set => _checkedImage = value;
        }

        public CettoDataGridViewCheckBoxCell()
        {
            _checkedImage = Files.Images.check_box_checked;
            _uncheckedImage = Files.Images.check_box;
        }

        protected override void OnContentClick(DataGridViewCellEventArgs e)
        {
            base.OnContentClick(e);

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Value = !(bool)Value;
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);

            if ((paintParts & DataGridViewPaintParts.ContentForeground) != 0)
            {
                bool isChecked = false;

                if (value is bool)
                {
                    isChecked = (bool)value;
                }

                Image currentImage = isChecked ? _checkedImage : _uncheckedImage;

                if (currentImage != null)
                {
                    int imageX = cellBounds.X + (cellBounds.Width - currentImage.Width) / 2;
                    int imageY = cellBounds.Y + (cellBounds.Height - currentImage.Height) / 2;

                    graphics.DrawImage(currentImage, imageX, imageY);
                }
            }
        }

        public override object Clone()
        {
            CettoDataGridViewCheckBoxCell cell = (CettoDataGridViewCheckBoxCell)base.Clone();
            cell._checkedImage = this._checkedImage;
            cell._uncheckedImage = this._uncheckedImage;
            return cell;
        }
    }

    public class CettoDataGridViewGreenCheckBoxCell : CettoDataGridViewCheckBoxCell
    {
        public CettoDataGridViewGreenCheckBoxCell()
        {
            CheckedImage = Files.Images.check_box_checked_green;
        }
    }
}
