namespace BelTel
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
            addButton = new Button();
            documentsComboBox = new ComboBox();
            recipientsComboBox = new ComboBox();
            StartNumberTextBox = new TextBox();
            EndNumberTextBox = new TextBox();
            dataGridView1 = new DataGridView();
            seriesComboBox = new ComboBox();
            menuStrip1 = new MenuStrip();
            menu = new ToolStripMenuItem();
            addDocumentMenuItem = new ToolStripMenuItem();
            addRecipientsMenuItem = new ToolStripMenuItem();
            addSeriesMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // addButton
            // 
            addButton.Location = new Point(680, 42);
            addButton.Name = "addButton";
            addButton.Size = new Size(94, 29);
            addButton.TabIndex = 0;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = true;
            // 
            // documentsComboBox
            // 
            documentsComboBox.FormattingEnabled = true;
            documentsComboBox.Location = new Point(284, 42);
            documentsComboBox.Name = "documentsComboBox";
            documentsComboBox.Size = new Size(151, 28);
            documentsComboBox.TabIndex = 1;
            documentsComboBox.Text = "Выбрать";
            // 
            // recipientsComboBox
            // 
            recipientsComboBox.FormattingEnabled = true;
            recipientsComboBox.Location = new Point(637, 77);
            recipientsComboBox.Name = "recipientsComboBox";
            recipientsComboBox.Size = new Size(151, 28);
            recipientsComboBox.TabIndex = 2;
            recipientsComboBox.Text = "Выбрать";
            // 
            // StartNumberTextBox
            // 
            StartNumberTextBox.Location = new Point(12, 42);
            StartNumberTextBox.Name = "StartNumberTextBox";
            StartNumberTextBox.Size = new Size(125, 27);
            StartNumberTextBox.TabIndex = 3;
            StartNumberTextBox.KeyPress += NumberTextBox_KeyPress;
            // 
            // EndNumberTextBox
            // 
            EndNumberTextBox.Location = new Point(153, 42);
            EndNumberTextBox.Name = "EndNumberTextBox";
            EndNumberTextBox.Size = new Size(125, 27);
            EndNumberTextBox.TabIndex = 4;
            EndNumberTextBox.KeyPress += NumberTextBox_KeyPress;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.Location = new Point(0, 111);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(800, 339);
            dataGridView1.TabIndex = 5;
            // 
            // seriesComboBox
            // 
            seriesComboBox.FormattingEnabled = true;
            seriesComboBox.Location = new Point(441, 41);
            seriesComboBox.Name = "seriesComboBox";
            seriesComboBox.Size = new Size(151, 28);
            seriesComboBox.TabIndex = 6;
            seriesComboBox.Text = "Выбрать";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
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
            addDocumentMenuItem.Name = "addDocumentMenuItem";
            addDocumentMenuItem.Size = new Size(264, 26);
            addDocumentMenuItem.Text = "Добавить тип документа";
            addDocumentMenuItem.Click += addDocumentMenuItem_Click;
            // 
            // addRecipientsMenuItem
            // 
            addRecipientsMenuItem.Name = "addRecipientsMenuItem";
            addRecipientsMenuItem.Size = new Size(264, 26);
            addRecipientsMenuItem.Text = "Добавить получателя";
            addRecipientsMenuItem.Click += addRecipientsMenuItem_Click;
            // 
            // addSeriesMenuItem
            // 
            addSeriesMenuItem.Name = "addSeriesMenuItem";
            addSeriesMenuItem.Size = new Size(264, 26);
            addSeriesMenuItem.Text = "Добавить серию";
            addSeriesMenuItem.Click += addSeriesMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(seriesComboBox);
            Controls.Add(dataGridView1);
            Controls.Add(EndNumberTextBox);
            Controls.Add(StartNumberTextBox);
            Controls.Add(recipientsComboBox);
            Controls.Add(documentsComboBox);
            Controls.Add(addButton);
            Controls.Add(menuStrip1);
            Name = "MainForm";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addButton;
        private ComboBox documentsComboBox;
        private ComboBox recipientsComboBox;
        private TextBox StartNumberTextBox;
        private TextBox EndNumberTextBox;
        private DataGridView dataGridView1;
        private ComboBox seriesComboBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menu;
        private ToolStripMenuItem addDocumentMenuItem;
        private ToolStripMenuItem addRecipientsMenuItem;
        private ToolStripMenuItem addSeriesMenuItem;
    }
}
