using Practice.Database;
using Practice.ViewModels;
using Practice.Models;

namespace Practice.Forms
{
    public partial class BlanksInBoxForm : Form
    {
        private readonly BoxViewModel box;

        private DateTimePicker datePicker = new DateTimePicker();
        private bool datePickerVisible = false;

        private ComboBox recipientComboBox = new ComboBox();
        private bool isComboBoxShown = false;

        public BlanksInBoxForm(BoxViewModel box)
        {
            InitializeComponent();
            this.box = box;
        }

        private void BlanksInBoxForm_Load(object sender, EventArgs e)
        {
            var blanks = DatabaseHelper.GetBlanksByBox(box);
            blanksGridView.DataSource = blanks;
            datePicker.Visible = false;
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.TextChanged += DatePicker_TextChanged;
            blanksGridView.Controls.Add(datePicker);
        }

        private void blanksGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (blanksGridView.Columns[e.ColumnIndex].Name == "ProductName")
            {
                try
                {
                    int blankNumber = Convert.ToInt32(blanksGridView.Rows[e.RowIndex].Cells["BlankNumber"].Value);
                    string? productName = blanksGridView.Rows[e.RowIndex].Cells["ProductName"].Value?.ToString();

                    if (productName != null)
                        DatabaseHelper.UpdateBlankProductNameByNumber(blankNumber, productName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении названия товара: " + ex.Message);
                }
            }
        }

        private void blanksGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (recipientComboBox != null)
            {
                blanksGridView.Controls.Remove(recipientComboBox);
                recipientComboBox.Dispose();
                recipientComboBox = null;
            }

            if (e.RowIndex >= 0 && blanksGridView.Columns[e.ColumnIndex].Name == "RecipientName")
            {
                recipientComboBox = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                var recipients = DatabaseHelper.GetRecipients();
                recipientComboBox.DataSource = recipients;
                recipientComboBox.DisplayMember = "Name";
                recipientComboBox.ValueMember = "Id";

                string currentRecipient = blanksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                var selectedRecipient = recipients.FirstOrDefault(r => r.Name == currentRecipient);
                if (selectedRecipient != null)
                    recipientComboBox.SelectedItem = selectedRecipient;

                var cellRect = blanksGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                recipientComboBox.Bounds = cellRect;

                recipientComboBox.SelectedValueChanged += (s, ev) =>
                {
                    var selectedRecipient = (Recipient)recipientComboBox.SelectedItem!;
                    blanksGridView.Rows[e.RowIndex].Cells["RecipientName"].Value = selectedRecipient.Name;

                    int blankNumber = Convert.ToInt32(blanksGridView.Rows[e.RowIndex].Cells["BlankNumber"].Value);
                    DatabaseHelper.UpdateBlankRecipientByNumber(blankNumber, selectedRecipient.Id);

                    blanksGridView.Controls.Remove(recipientComboBox);
                    recipientComboBox.Dispose();
                    recipientComboBox = null;
                };

                blanksGridView.Controls.Add(recipientComboBox);
                recipientComboBox.Focus();
            }

            if (blanksGridView.Columns[e.ColumnIndex].Name == "Date" && e.RowIndex >= 0)
            {
                Rectangle cellRectangle = blanksGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                datePicker.Size = new Size(cellRectangle.Width, cellRectangle.Height);
                datePicker.Location = cellRectangle.Location;

                if (blanksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                    DateTime.TryParse(blanksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out DateTime date))
                {
                    datePicker.Value = date;
                }
                else
                    datePicker.Value = DateTime.Today;

                datePicker.Visible = true;
                datePicker.BringToFront();
                datePicker.Focus();

                datePicker.Tag = new { RowIndex = e.RowIndex, ColumnIndex = e.ColumnIndex };
                datePickerVisible = true;
            }
            else
                datePicker.Visible = false;
        }

        private void DatePicker_TextChanged(object? sender, EventArgs e)
        {
            if (!datePickerVisible) return;

            var tag = (dynamic)datePicker.Tag;
            int rowIndex = tag.RowIndex;
            int colIndex = tag.ColumnIndex;

            string selectedDate = datePicker.Value.ToString("dd.MM.yyyy");

            blanksGridView.Rows[rowIndex].Cells[colIndex].Value = selectedDate;

            int blankNumber = Convert.ToInt32(blanksGridView.Rows[rowIndex].Cells["BlankNumber"].Value);
            var blank = DatabaseHelper.GetBlankByNumber(blankNumber);

            DatabaseHelper.UpdateBlankDateInDatabase(blank.Id, selectedDate);

            datePicker.Visible = false;
            datePickerVisible = false;
        }
    }
}
