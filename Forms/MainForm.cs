using BelTel.Database;
using BelTel.Forms;
using BelTel.Models;

namespace BelTel
{
    public partial class MainForm : Form
    {
        private DateTimePicker datePicker = new DateTimePicker();
        private bool datePickerVisible = false;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                Database.Database.OpenConnection();
                MessageBox.Show("База данных подключена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            documentsComboBox.DataSource = DatabaseHelper.GetDocuments();
            recipientsComboBox.DataSource = DatabaseHelper.GetRecipients();
            seriesComboBox.DataSource = DatabaseHelper.GetSeries();
            LoadBlanksToGrid();
            datePicker.Visible = false;
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.TextChanged += DatePicker_TextChanged;
            BlanksGridView.Controls.Add(datePicker);
        }

        private void LoadBlanksToGrid()
        {
            var blanks = DatabaseHelper.GetAllBlanks();
            BlanksGridView.DataSource = blanks;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.Database.CloseConnection();
        }

        private void NumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void addDocumentMenuItem_Click(object sender, EventArgs e) => new AddItemForm(AddItemType.Document).ShowDialog();

        private void addRecipientsMenuItem_Click(object sender, EventArgs e) => new AddItemForm(AddItemType.Recipient).ShowDialog();

        private void addSeriesMenuItem_Click(object sender, EventArgs e) => new AddItemForm(AddItemType.Series).ShowDialog();

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(StartNumberTextBox.Text.Trim(), out int startNumber) ||
                !int.TryParse(EndNumberTextBox.Text.Trim(), out int endNumber))
            {
                MessageBox.Show("Введите корректные числовые значения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (startNumber >= endNumber)
            {
                MessageBox.Show("Начальный номер должен быть меньше конечного!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (documentsComboBox.SelectedItem == null || seriesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите серию и тип документа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string? series = seriesComboBox.SelectedItem.ToString();
            Document documentId = (Document)documentsComboBox.SelectedValue!;

            if (!DatabaseHelper.AreBlanksNumberRangeAvailable(startNumber, endNumber))
            {
                MessageBox.Show("В указанном диапазоне уже есть бланки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DatabaseHelper.AddBoxWithBlanks(startNumber, endNumber, series!, documentId.Id);

            LoadBlanksToGrid();
            MessageBox.Show("Коробка и бланки успешно добавлены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            StartNumberTextBox.Clear();
            EndNumberTextBox.Clear();
            documentsComboBox.SelectedIndex = -1;
            seriesComboBox.SelectedIndex = -1;
        }

        private void BlanksGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BlanksGridView.Columns[e.ColumnIndex].Name == "Date" && e.RowIndex >= 0)
            {
                Rectangle cellRectangle = BlanksGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                datePicker.Size = new Size(cellRectangle.Width, cellRectangle.Height);
                datePicker.Location = cellRectangle.Location;

                if (BlanksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                    DateTime.TryParse(BlanksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out DateTime date))
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

            BlanksGridView.Rows[rowIndex].Cells[colIndex].Value = selectedDate;

            int blankNumber = Convert.ToInt32(BlanksGridView.Rows[rowIndex].Cells["BlankNumber"].Value);
            var blank = DatabaseHelper.GetBlankByNumber(blankNumber);

            DatabaseHelper.UpdateBlankDateInDatabase(blank.Id, selectedDate);

            datePicker.Visible = false;
            datePickerVisible = false;
        }
    }
}
