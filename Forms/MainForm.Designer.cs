using System.Windows.Forms;

namespace Practice
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            addButton = new Button();
            documentsComboBox = new ComboBox();
            StartNumberTextBox = new TextBox();
            EndNumberTextBox = new TextBox();
            BlanksGridView = new DataGridView();
            seriesComboBox = new ComboBox();
            menuStrip1 = new MenuStrip();
            menu = new ToolStripMenuItem();
            addDocumentMenuItem = new ToolStripMenuItem();
            viewDocumentMenuItem = new ToolStripMenuItem();
            addRecipientsMenuItem = new ToolStripMenuItem();
            viewRecipientsMenuItem = new ToolStripMenuItem();
            addSeriesMenuItem = new ToolStripMenuItem();
            viewSeriesMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            exportBtn = new Button();
            datePanel = new TableLayoutPanel();
            fromDatePicker = new DateTimePicker();
            toDatePicker = new DateTimePicker();
            documentForExportComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)BlanksGridView).BeginInit();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            datePanel.SuspendLayout();
            SuspendLayout();
            // 
            // addButton
            // 
            addButton.BackColor = Color.FromArgb(70, 130, 180);
            addButton.Dock = DockStyle.Fill;
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(602, 10);
            addButton.Margin = new Padding(5);
            addButton.Name = "addButton";
            addButton.Size = new Size(138, 30);
            addButton.TabIndex = 4;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            // 
            // documentsComboBox
            // 
            documentsComboBox.Dock = DockStyle.Fill;
            documentsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            documentsComboBox.Location = new Point(306, 10);
            documentsComboBox.Margin = new Padding(5);
            documentsComboBox.Name = "documentsComboBox";
            documentsComboBox.Size = new Size(138, 31);
            documentsComboBox.TabIndex = 2;
            // 
            // StartNumberTextBox
            // 
            StartNumberTextBox.Dock = DockStyle.Fill;
            StartNumberTextBox.Location = new Point(10, 10);
            StartNumberTextBox.Margin = new Padding(5);
            StartNumberTextBox.Name = "StartNumberTextBox";
            StartNumberTextBox.Size = new Size(138, 30);
            StartNumberTextBox.TabIndex = 0;
            StartNumberTextBox.KeyPress += NumberTextBox_KeyPress;
            // 
            // EndNumberTextBox
            // 
            EndNumberTextBox.Dock = DockStyle.Fill;
            EndNumberTextBox.Location = new Point(158, 10);
            EndNumberTextBox.Margin = new Padding(5);
            EndNumberTextBox.Name = "EndNumberTextBox";
            EndNumberTextBox.Size = new Size(138, 30);
            EndNumberTextBox.TabIndex = 1;
            EndNumberTextBox.KeyPress += NumberTextBox_KeyPress;
            // 
            // BlanksGridView
            // 
            BlanksGridView.AllowUserToAddRows = false;
            BlanksGridView.AllowUserToDeleteRows = false;
            BlanksGridView.AllowUserToResizeColumns = false;
            BlanksGridView.AllowUserToResizeRows = false;
            BlanksGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BlanksGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BlanksGridView.BackgroundColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.SteelBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            BlanksGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            BlanksGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            BlanksGridView.DefaultCellStyle = dataGridViewCellStyle2;
            BlanksGridView.EnableHeadersVisualStyles = false;
            BlanksGridView.GridColor = Color.LightGray;
            BlanksGridView.Location = new Point(0, 151);
            BlanksGridView.Name = "BlanksGridView";
            BlanksGridView.RowHeadersVisible = false;
            BlanksGridView.RowHeadersWidth = 51;
            BlanksGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BlanksGridView.Size = new Size(900, 390);
            BlanksGridView.TabIndex = 5;
            BlanksGridView.CellClick += BlanksGridView_CellClick;
            // 
            // seriesComboBox
            // 
            seriesComboBox.Dock = DockStyle.Fill;
            seriesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            seriesComboBox.Location = new Point(454, 10);
            seriesComboBox.Margin = new Padding(5);
            seriesComboBox.Name = "seriesComboBox";
            seriesComboBox.Size = new Size(138, 31);
            seriesComboBox.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(900, 28);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // menu
            // 
            menu.DropDownItems.AddRange(new ToolStripItem[] { addDocumentMenuItem, addRecipientsMenuItem, addSeriesMenuItem });
            menu.Name = "menu";
            menu.Size = new Size(65, 24);
            menu.Text = "Меню";
            // 
            // addDocumentMenuItem
            // 
            addDocumentMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewDocumentMenuItem });
            addDocumentMenuItem.Name = "addDocumentMenuItem";
            addDocumentMenuItem.Size = new Size(264, 26);
            addDocumentMenuItem.Text = "Добавить тип документа";
            addDocumentMenuItem.Click += addDocumentMenuItem_Click;
            // 
            // viewDocumentMenuItem
            // 
            viewDocumentMenuItem.Name = "viewDocumentMenuItem";
            viewDocumentMenuItem.Size = new Size(310, 26);
            viewDocumentMenuItem.Text = "Просмотреть типы документов";
            viewDocumentMenuItem.Click += viewDocumentMenuItem_Click;
            // 
            // addRecipientsMenuItem
            // 
            addRecipientsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewRecipientsMenuItem });
            addRecipientsMenuItem.Name = "addRecipientsMenuItem";
            addRecipientsMenuItem.Size = new Size(264, 26);
            addRecipientsMenuItem.Text = "Добавить получателя";
            addRecipientsMenuItem.Click += addRecipientsMenuItem_Click;
            // 
            // viewRecipientsMenuItem
            // 
            viewRecipientsMenuItem.Name = "viewRecipientsMenuItem";
            viewRecipientsMenuItem.Size = new Size(277, 26);
            viewRecipientsMenuItem.Text = "Просмотреть получателей";
            viewRecipientsMenuItem.Click += viewRecipientsMenuItem_Click;
            // 
            // addSeriesMenuItem
            // 
            addSeriesMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewSeriesMenuItem });
            addSeriesMenuItem.Name = "addSeriesMenuItem";
            addSeriesMenuItem.Size = new Size(264, 26);
            addSeriesMenuItem.Text = "Добавить серию";
            addSeriesMenuItem.Click += addSeriesMenuItem_Click;
            // 
            // viewSeriesMenuItem
            // 
            viewSeriesMenuItem.Name = "viewSeriesMenuItem";
            viewSeriesMenuItem.Size = new Size(231, 26);
            viewSeriesMenuItem.Text = "Просмотреть серии";
            viewSeriesMenuItem.Click += viewSeriesMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(StartNumberTextBox, 0, 0);
            tableLayoutPanel1.Controls.Add(EndNumberTextBox, 1, 0);
            tableLayoutPanel1.Controls.Add(documentsComboBox, 2, 0);
            tableLayoutPanel1.Controls.Add(seriesComboBox, 3, 0);
            tableLayoutPanel1.Controls.Add(addButton, 4, 0);
            tableLayoutPanel1.Controls.Add(exportBtn, 5, 0);
            tableLayoutPanel1.Controls.Add(datePanel, 5, 1);
            tableLayoutPanel1.Controls.Add(documentForExportComboBox, 4, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 28);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(5);
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.Size = new Size(900, 125);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // exportBtn
            // 
            exportBtn.BackColor = Color.FromArgb(70, 130, 180);
            exportBtn.FlatAppearance.BorderSize = 0;
            exportBtn.FlatStyle = FlatStyle.Flat;
            exportBtn.ForeColor = Color.White;
            exportBtn.Location = new Point(750, 10);
            exportBtn.Margin = new Padding(5);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(101, 30);
            exportBtn.TabIndex = 8;
            exportBtn.Text = "Экспорт";
            exportBtn.UseVisualStyleBackColor = false;
            exportBtn.Click += exportBtn_Click;
            // 
            // datePanel
            // 
            datePanel.ColumnCount = 1;
            datePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            datePanel.Controls.Add(fromDatePicker, 0, 0);
            datePanel.Controls.Add(toDatePicker, 0, 1);
            datePanel.Dock = DockStyle.Fill;
            datePanel.Location = new Point(748, 48);
            datePanel.Name = "datePanel";
            datePanel.RowCount = 2;
            datePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            datePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            datePanel.Size = new Size(144, 69);
            datePanel.TabIndex = 9;
            // 
            // fromDatePicker
            // 
            fromDatePicker.Dock = DockStyle.Fill;
            fromDatePicker.Format = DateTimePickerFormat.Short;
            fromDatePicker.Location = new Point(3, 3);
            fromDatePicker.Name = "fromDatePicker";
            fromDatePicker.Size = new Size(138, 30);
            fromDatePicker.TabIndex = 0;
            // 
            // toDatePicker
            // 
            toDatePicker.Dock = DockStyle.Fill;
            toDatePicker.Format = DateTimePickerFormat.Short;
            toDatePicker.Location = new Point(3, 37);
            toDatePicker.Name = "toDatePicker";
            toDatePicker.Size = new Size(138, 30);
            toDatePicker.TabIndex = 1;
            // 
            // documentForExportComboBox
            // 
            documentForExportComboBox.FormattingEnabled = true;
            documentForExportComboBox.Location = new Point(600, 48);
            documentForExportComboBox.Name = "documentForExportComboBox";
            documentForExportComboBox.Size = new Size(142, 31);
            documentForExportComboBox.TabIndex = 10;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 255);
            ClientSize = new Size(900, 518);
            Controls.Add(BlanksGridView);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(918, 565);
            Name = "MainForm";
            Text = "Учет Бланков";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)BlanksGridView).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            datePanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addButton;
        private ComboBox documentsComboBox;
        private TextBox StartNumberTextBox;
        private TextBox EndNumberTextBox;
        private DataGridView BlanksGridView;
        private ComboBox seriesComboBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menu;
        private ToolStripMenuItem addDocumentMenuItem;
        private ToolStripMenuItem addRecipientsMenuItem;
        private ToolStripMenuItem addSeriesMenuItem;
        private ToolStripMenuItem viewDocumentMenuItem;
        private ToolStripMenuItem viewRecipientsMenuItem;
        private ToolStripMenuItem viewSeriesMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private Button exportBtn;
        private DateTimePicker fromDatePicker;
        private DateTimePicker toDatePicker;
        private TableLayoutPanel datePanel;
        private ComboBox documentForExportComboBox;
    }
}
