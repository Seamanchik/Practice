using Practice.Database;

namespace Practice.Forms
{
    public partial class ViewItemsForm : Form
    {
        private readonly AddItemType _itemType;

        public ViewItemsForm(AddItemType itemType)
        {
            InitializeComponent();
            _itemType = itemType;
            SetupForm();
            LoadData();
        }

        private void SetupForm()
        {
            switch (_itemType)
            {
                case AddItemType.Document:
                    this.Text = "Типы документов";
                    break;
                case AddItemType.Recipient:
                    this.Text = "Получатели";
                    break;
                case AddItemType.Series:
                    this.Text = "Серии";
                    break;
            }
        }

        private void LoadData()
        {
            switch (_itemType)
            {
                case AddItemType.Document:
                    viewItemGrid.DataSource = DatabaseHelper.GetDocuments();
                    break;
                case AddItemType.Recipient:
                    viewItemGrid.DataSource = DatabaseHelper.GetRecipients();
                    break;
                case AddItemType.Series:
                    viewItemGrid.DataSource = DatabaseHelper.GetSeries();
                    break;
            }
        }
    }
}
