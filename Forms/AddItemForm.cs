using Practice.Database;
using System.Text.RegularExpressions;

namespace Practice.Forms
{
    public partial class AddItemForm : Form
    {
        private readonly AddItemType _itemType;

        public AddItemForm(AddItemType itemType)
        {
            InitializeComponent();
            _itemType = itemType;
            SetupForm();
        }

        private void SetupForm()
        {
            switch (_itemType)
            {
                case AddItemType.Document:
                    lblTitle.Text = "Введите название типа документа:";
                    break;
                case AddItemType.Recipient:
                    lblTitle.Text = "Введите имя получателя:";
                    break;
                case AddItemType.Series:
                    lblTitle.Text = "Введите серию:";
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Поле не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (_itemType)
            {
                case AddItemType.Document:
                    if (!Regex.IsMatch(input, @"^[a-zA-Z0-9А-Яа-я\-]+$"))
                    {
                        MessageBox.Show("Название может содержать только буквы и цифры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DatabaseHelper.AddDocument(input);
                    break;

                case AddItemType.Recipient:
                    if (!Regex.IsMatch(input, @"^[a-zA-Zа-яА-Я\s]+$"))
                    {
                        MessageBox.Show("Имя может содержать только буквы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DatabaseHelper.AddRecipient(input);
                    break;

                case AddItemType.Series:
                    if (!Regex.IsMatch(input, @"^[a-zA-Zа-яА-Я]+$"))
                    {
                        MessageBox.Show("Серия может содержать только буквы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DatabaseHelper.AddSeries(input);
                    break;
            }

            MessageBox.Show("Элемент добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}

    public enum AddItemType
    {
        Document,
        Recipient,
        Series
    }
