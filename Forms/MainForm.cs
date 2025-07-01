using Practice.Database;
using Practice.Export;
using Practice.Forms;
using Practice.Helper;
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
            documentForExportComboBox.DataSource = DatabaseHelper.GetDocuments();
            seriesComboBox.DataSource = DatabaseHelper.GetSeries();
            documentsComboBox.SelectedIndex = -1;
            documentForExportComboBox.SelectedIndex = -1;
            seriesComboBox.SelectedIndex = -1;
            LoadBlanksToGrid();
            DataGridHelper.SetColumnHeaders(BlanksGridView, DataGridHelper.BoxHeaders);
        }

        private void LoadBlanksToGrid()
        {
            var blanks = DatabaseHelper.GetBoxesForDataGrid();
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

        private void exportBtn_Click(object sender, EventArgs e)
        {
            bool fromDateSelected = fromDatePicker.CustomFormat != " ";
            bool toDateSelected = toDatePicker.CustomFormat != " ";

            if (fromDateSelected != toDateSelected)
            {
                MessageBox.Show("Выберите обе даты: начальную и конечную.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? fromDate = null;
            DateTime? toDate = null;

            if (fromDateSelected && toDateSelected)
            {
                fromDate = fromDatePicker.Value.Date;
                toDate = toDatePicker.Value.Date;

                if (fromDate > toDate)
                {
                    MessageBox.Show("Начальная дата не может быть позже конечной.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var documents = DatabaseHelper.GetDocuments();
            var blanks = DatabaseHelper.GetBlanks();
            var boxes = DatabaseHelper.GetBoxes();
            var recipients = DatabaseHelper.GetRecipients();

            var selectedDocumentName = documentForExportComboBox.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedDocumentName))
            {
                MessageBox.Show("Пожалуйста, выберите тип документа для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedDocument = documents.FirstOrDefault(d => d.Name == selectedDocumentName);
            if (selectedDocument == null)
            {
                MessageBox.Show("Выбранный документ не найден в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fromDate.HasValue && toDate.HasValue)
            {
                blanks = blanks
                    .Where(b => b.Date.HasValue &&
                                b.Date.Value.Date >= fromDate.Value &&
                                b.Date.Value.Date <= toDate.Value)
                    .ToList();
            }

            var filledBlanks = blanks
                .Where(b => !string.IsNullOrWhiteSpace(b.ProductName)
                         && b.Date.HasValue
                         && b.RecipientId.HasValue)
                .ToList();

            if (filledBlanks.Count == 0)
            {
                MessageBox.Show("Нет заполненных бланков в выбранном диапазоне.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var relevantBoxes = boxes.Where(b => b.DocumentId == selectedDocument.Id).ToList();
            var relevantBlanks = filledBlanks.Where(b => relevantBoxes.Any(box => box.Id == b.BoxId)).ToList();

            if (relevantBlanks.Count == 0)
            {
                MessageBox.Show("Нет заполненных бланков по выбранному типу документа.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string yearPart = "";
            if (fromDate.HasValue && toDate.HasValue)
            {
                int yearFrom = fromDate.Value.Year;
                int yearTo = toDate.Value.Year;
                yearPart = (yearFrom == yearTo) ? yearFrom.ToString() : $"{yearFrom}-{yearTo}";
            }
            else
            {
                var years = relevantBlanks
                    .Where(b => b.Date.HasValue)
                    .Select(b => b.Date!.Value.Year)
                    .Distinct()
                    .OrderBy(y => y)
                    .ToList();

                yearPart = (years.Count == 1) ? years[0].ToString() : $"{years.First()}-{years.Last()}";
            }

            string normalizedName = selectedDocumentName.Replace("-", "_");
            string defaultFileName = $"Учет_бланков_строгой_отчетности_{normalizedName}_{yearPart}.xlsx";

            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Сохранить как";
            sfd.Filter = "Excel файл (*.xlsx)|*.xlsx";
            sfd.FileName = defaultFileName;

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Учет_бланков_строгой_отчетности___.xlsx");
            if (!File.Exists(templatePath))
            {
                MessageBox.Show("Шаблон Excel-файла не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                File.Copy(templatePath, sfd.FileName, overwrite: true);

                ExcelExporter.ExportFilledBlanksByDocumentType(
                    new Dictionary<string, string> { { selectedDocumentName, sfd.FileName } },
                    relevantBlanks,
                    relevantBoxes,
                    new List<Document> { selectedDocument },
                    recipients
                );

                MessageBox.Show("Экспорт завершён.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
