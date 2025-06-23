namespace Practice.Forms
{
    partial class BlanksInBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            blanksGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)blanksGridView).BeginInit();
            SuspendLayout();
            // 
            // blanksGridView
            // 
            blanksGridView.AllowUserToAddRows = false;
            blanksGridView.AllowUserToDeleteRows = false;
            blanksGridView.AllowUserToResizeColumns = false;
            blanksGridView.AllowUserToResizeRows = false;
            blanksGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            blanksGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            blanksGridView.BackgroundColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.SteelBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            blanksGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            blanksGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            blanksGridView.DefaultCellStyle = dataGridViewCellStyle2;
            blanksGridView.EnableHeadersVisualStyles = false;
            blanksGridView.GridColor = Color.LightGray;
            blanksGridView.Location = new Point(0, 0);
            blanksGridView.Name = "blanksGridView";
            blanksGridView.RowHeadersVisible = false;
            blanksGridView.RowHeadersWidth = 51;
            blanksGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            blanksGridView.Size = new Size(900, 400);
            blanksGridView.TabIndex = 0;
            blanksGridView.CellClick += blanksGridView_CellClick;
            blanksGridView.CellValueChanged += blanksGridView_CellValueChanged;
            // 
            // BlanksInBoxForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 255);
            ClientSize = new Size(900, 400);
            Controls.Add(blanksGridView);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(918, 440);
            Name = "BlanksInBoxForm";
            Text = "Бланки в коробке";
            Load += BlanksInBoxForm_Load;
            ((System.ComponentModel.ISupportInitialize)blanksGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView blanksGridView;
    }
}