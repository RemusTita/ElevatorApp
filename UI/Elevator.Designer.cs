namespace ElevatorApp.UI
{
    partial class Elevator : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        private Panel shaft;
        private Panel cabin;
        private Panel doorLeft;
        private Panel doorRight;
        private Label statusLabel;
        private Button btnGround;
        private Button btnFirst;
        private Label elevatorNameLabel;

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
            this.shaft = new Panel();
            this.cabin = new Panel();
            this.doorLeft = new Panel();
            this.doorRight = new Panel();
            this.statusLabel = new Label();
            this.btnGround = new Button();
            this.btnFirst = new Button();
            this.elevatorNameLabel = new Label();
            this.shaft.SuspendLayout();
            this.cabin.SuspendLayout();
            this.SuspendLayout();

            // shaft
            this.shaft.BackColor = Color.FromArgb(45, 52, 70);
            this.shaft.BorderStyle = BorderStyle.None;
            this.shaft.Controls.Add(this.cabin);
            this.shaft.Location = new Point(10, 10);
            this.shaft.Name = "shaft";
            this.shaft.Size = new Size(100, 380);

            // cabin
            this.cabin.BackColor = Color.FromArgb(176, 196, 222);
            this.cabin.Controls.Add(this.doorLeft);
            this.cabin.Controls.Add(this.doorRight);
            this.cabin.Location = new Point(0, 290);
            this.cabin.Name = "cabin";
            this.cabin.Size = new Size(100, 90);

            // doorLeft
            this.doorLeft.BackColor = Color.FromArgb(70, 80, 95);
            this.doorLeft.Location = new Point(0, 0);
            this.doorLeft.Name = "doorLeft";
            this.doorLeft.Size = new Size(50, 90);

            // doorRight
            this.doorRight.BackColor = Color.FromArgb(70, 80, 95);
            this.doorRight.Location = new Point(50, 0);
            this.doorRight.Name = "doorRight";
            this.doorRight.Size = new Size(50, 90);

            // elevatorNameLabel
            this.elevatorNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.elevatorNameLabel.ForeColor = Color.FromArgb(0, 150, 136);
            this.elevatorNameLabel.Location = new Point(115, 10);
            this.elevatorNameLabel.Name = "elevatorNameLabel";
            this.elevatorNameLabel.Size = new Size(80, 25);
            this.elevatorNameLabel.Text = this.ElevatorName;
            this.elevatorNameLabel.TextAlign = ContentAlignment.TopLeft;

            // statusLabel
            this.statusLabel.Font = new Font("Segoe UI", 9F);
            this.statusLabel.ForeColor = Color.FromArgb(60, 60, 60);
            this.statusLabel.Location = new Point(115, 45);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(300, 50);
            this.statusLabel.Text = "Floor: G\nStatus: Idle";
            this.statusLabel.TextAlign = ContentAlignment.TopLeft;

            // btnGround
            this.btnGround.BackColor = Color.FromArgb(0, 150, 136);
            this.btnGround.FlatStyle = FlatStyle.Flat;
            this.btnGround.FlatAppearance.BorderSize = 0;
            this.btnGround.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnGround.ForeColor = Color.White;
            this.btnGround.Location = new Point(115, 100);
            this.btnGround.Name = "btnGround";
            this.btnGround.Size = new Size(45, 45);
            this.btnGround.Text = "G";
            this.btnGround.UseVisualStyleBackColor = false;
            this.btnGround.Click += new EventHandler(this.BtnGround_Click);


            // btnFirst
            this.btnFirst.BackColor = Color.FromArgb(0, 150, 136);
            this.btnFirst.FlatStyle = FlatStyle.Flat;
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnFirst.ForeColor = Color.White;
            this.btnFirst.Location = new Point(165, 100);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new Size(45, 45);
            this.btnFirst.Text = "1";
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new EventHandler(this.BtnFirst_Click);



            // Elevator
            this.BackColor = Color.White;
            this.Controls.Add(this.elevatorNameLabel);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnGround);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.shaft);
            this.shaft.ResumeLayout(false);
            this.cabin.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}