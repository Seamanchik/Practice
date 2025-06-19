namespace Practice.Forms
{
    partial class ViewItemsForm
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
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            viewItemGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)viewItemGrid).BeginInit();
            SuspendLayout();
            // 
            // viewItemGrid
            // 
            viewItemGrid.AllowUserToAddRows = false;
            viewItemGrid.AllowUserToDeleteRows = false;
            viewItemGrid.AllowUserToResizeColumns = false;
            viewItemGrid.AllowUserToResizeRows = false;
            viewItemGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            viewItemGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            viewItemGrid.BackgroundColor = Color.WhiteSmoke;

            // Header style
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.SteelBlue;
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.ForeColor = Color.White;
            headerStyle.SelectionBackColor = Color.SteelBlue;
            headerStyle.SelectionForeColor = Color.White;
            headerStyle.WrapMode = DataGridViewTriState.True;
            viewItemGrid.ColumnHeadersDefaultCellStyle = headerStyle;
            viewItemGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Cell style
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = Color.White;
            cellStyle.Font = new Font("Segoe UI", 10F);
            cellStyle.ForeColor = Color.Black;
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            cellStyle.WrapMode = DataGridViewTriState.False;
            viewItemGrid.DefaultCellStyle = cellStyle;

            viewItemGrid.Dock = DockStyle.Fill;
            viewItemGrid.EnableHeadersVisualStyles = false;
            viewItemGrid.GridColor = Color.LightGray;
            viewItemGrid.Location = new Point(0, 0);
            viewItemGrid.Name = "viewItemGrid";
            viewItemGrid.ReadOnly = true;
            viewItemGrid.RowHeadersVisible = false;
            viewItemGrid.RowHeadersWidth = 51;
            viewItemGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            viewItemGrid.Size = new Size(500, 300);
            viewItemGrid.TabIndex = 0;
            // 
            // ViewItemsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 300);
            Controls.Add(viewItemGrid);
            Font = new Font("Segoe UI", 10F);
            Name = "ViewItemsForm";
            Text = "Просмотр элементов";
            MinimumSize = new Size(400, 250);

            ((System.ComponentModel.ISupportInitialize)viewItemGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView viewItemGrid;
    }
}