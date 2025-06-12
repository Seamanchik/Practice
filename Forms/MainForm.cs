using BelTel.Database;
using BelTel.Forms;

namespace BelTel
{
    public partial class MainForm : Form
    {
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
    }
}
