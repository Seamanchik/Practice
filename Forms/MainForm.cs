using Practice.Database;
using Practice.Forms;
using Practice.Models;
using Practice.ViewModels;

namespace Practice
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            documentsComboBox.DataSource = DatabaseHelper.GetDocuments();
            seriesComboBox.DataSource = DatabaseHelper.GetSeries();
            LoadBlanksToGrid();
        }

        private void LoadBlanksToGrid()
        {
            var blanks = DatabaseHelper.GetBoxes();
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
            if (e.RowIndex >= 0)
            {
                var row = BlanksGridView.Rows[e.RowIndex];
                var box = new BoxViewModel
                {
                    StartNumber = Convert.ToInt32(row.Cells["StartNumber"].Value),
                    EndNumber = Convert.ToInt32(row.Cells["EndNumber"].Value),
                    Series = row.Cells["Series"].Value.ToString(),
                    DocumentName = row.Cells["DocumentName"].Value.ToString()
                };

                var form = new BlanksInBoxForm(box);
                form.ShowDialog();
            }
        }

        private void viewDocumentMenuItem_Click(object sender, EventArgs e) => new ViewItemsForm(AddItemType.Document).ShowDialog();

        private void viewRecipientsMenuItem_Click(object sender, EventArgs e) => new ViewItemsForm(AddItemType.Recipient).ShowDialog();

        private void viewSeriesMenuItem_Click(object sender, EventArgs e) => new ViewItemsForm(AddItemType.Series).ShowDialog();

        private void addDocumentMenuItem_Click(object sender, EventArgs e) => new AddItemForm(AddItemType.Document).ShowDialog();

        private void addRecipientsMenuItem_Click(object sender, EventArgs e) => new AddItemForm(AddItemType.Recipient).ShowDialog();

        private void addSeriesMenuItem_Click(object sender, EventArgs e) => new AddItemForm(AddItemType.Series).ShowDialog();
    }
}
