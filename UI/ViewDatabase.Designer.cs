namespace ElevatorApp
{
    partial class ViewDatabase
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTitle;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridView = new DataGridView();
            this.btnRefresh = new Button();
            this.btnClose = new Button();
            this.lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.BackColor = Color.FromArgb(0, 122, 204);
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(700, 50);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "📊 Elevator Activity Log";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // dataGridView
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = Color.White;
            this.dataGridView.BorderStyle = BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new Point(12, 62);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new Size(676, 326);
            this.dataGridView.TabIndex = 0;

          

            // btnClose
            this.btnClose.BackColor = Color.FromArgb(220, 53, 69);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(538, 420);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(150, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "✖ Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.BtnClose_Click);


            // ViewDatabase
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(700, 470);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewDatabase";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Elevator Activity Log";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
        }
    }
}