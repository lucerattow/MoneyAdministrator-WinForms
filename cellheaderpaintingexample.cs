using System.Reflection;

TextFormatFlags centerTopflags =
    TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.PreserveGraphicsClipping;
string mergedHeaderText = string.Empty;
int[] mergedColumns = null;

void SomeForm()
{
    this.InitializeComponent();
    var flags = BindingFlags.Instance | BindingFlags.NonPublic;
    dgvTest.GetType().GetProperty("DoubleBuffered", flags).SetValue(dgvTest, true);
    mergedColumns = new int[] { 20, 21 };
    mergedHeaderText = "DOOR CLOSER";
}

void dgv_db_door_Paint(object sender, PaintEventArgs e)
{
    var dgv = sender as DataGridView;
    var headerStyle = dgv.ColumnHeadersDefaultCellStyle;
    int colsWidth = -1;
    int colsLeft = 1;

    // Absolute Width of the merged Column range
    for (int i = 0; i < mergedColumns.Length; i++)
    {
        var col = dgv.Columns[mergedColumns[i]];
        colsWidth += col.Visible ? col.Width : 0;
    }

    // Absolute Left position of the first Column to merge
    if (mergedColumns[0] > 0)
    {
        colsLeft += dgv.Columns.OfType<DataGridViewColumn>()
            .Where(c => c.Visible).Take(mergedColumns[0]).Sum(c => c.Width);
    }

    // Merged Headers raw drawing  Rectangle
    var r = new Rectangle(
        dgv.RowHeadersWidth + colsLeft - dgv.HorizontalScrollingOffset, 2,
        colsWidth, dgv.ColumnHeadersHeight);

    // Measure the Height of the text to render - no wrapping
    r.Height = TextRenderer.MeasureText(e.Graphics, mergedHeaderText, headerStyle.Font, r.Size, centerTopflags).Height;

    // Draw the merged Headers only if visible on screen
    if (r.Right > dgv.RowHeadersWidth || r.X < dgv.DisplayRectangle.Right)
    {
        // Clip the drawing Region to exclude the Row Header
        var clipRect = new Rectangle(
            dgv.RowHeadersWidth + 1, 0,
            dgv.DisplayRectangle.Width - dgv.RowHeadersWidth, dgv.ColumnHeadersHeight);
        e.Graphics.SetClip(clipRect);

        using (var brush = new SolidBrush(headerStyle.BackColor)) e.Graphics.FillRectangle(brush, r);
        TextRenderer.DrawText(e.Graphics, mergedHeaderText, headerStyle.Font, r, headerStyle.ForeColor, centerTopflags);
        e.Graphics.ResetClip();
    }
}