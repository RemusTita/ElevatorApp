namespace ElevatorApp.UI
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox logBox;
        private Label indicatorGround;
        private Label indicatorFirst;
        private Button btnCallGround;
        private Button btnCallFirst;
        private Button btnViewDB;
        private Label lblGroundFloor;
        private Label lblFirstFloor;
        private Label lblActivityLog;
        private Panel controlPanel;
        private Label lblTitle;
        private Label lblQueueStatus;
        private Elevator elevatorA;
        private Elevator elevatorB;


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
            
            lblTitle = new Label();
            controlPanel = new Panel();
            lblGroundFloor = new Label();
            indicatorGround = new Label();
            lblFirstFloor = new Label();
            indicatorFirst = new Label();
            lblActivityLog = new Label();
            lblQueueStatus = new Label();
            logBox = new TextBox();
            btnCallGround = new Button();
            btnCallFirst = new Button();
            btnViewDB = new Button();
            controlPanel.SuspendLayout();
            SuspendLayout();

            // lblTitle
            lblTitle.BackColor = Color.FromArgb(0, 122, 204);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Size = new Size(800, 50);
            lblTitle.Text = "🏢 Elevator Control System";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // elevatorA
            elevatorA = new Elevator(new Point(30, 70), "Elevator A");
            elevatorA.Name = "elevatorA";
            elevatorA.Size = new Size(240, 400);

            // elevatorB
            elevatorB = new Elevator(new Point(350, 70), "Elevator B");
            elevatorB.Name = "elevatorB";
            elevatorB.Size = new Size(240, 400);

            // controlPanel
            controlPanel.BackColor = Color.FromArgb(245, 245, 245);
            controlPanel.BorderStyle = BorderStyle.FixedSingle;
            controlPanel.Controls.Add(lblGroundFloor);
            controlPanel.Controls.Add(indicatorGround);
            controlPanel.Controls.Add(lblFirstFloor);
            controlPanel.Controls.Add(indicatorFirst);
            controlPanel.Controls.Add(lblActivityLog);
            controlPanel.Controls.Add(logBox);
            controlPanel.Controls.Add(btnCallGround);
            controlPanel.Controls.Add(btnCallFirst);
            controlPanel.Controls.Add(btnViewDB);
            controlPanel.Controls.Add(lblQueueStatus);
            controlPanel.Location = new Point(600, 70);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(260, 400);
            
            // lblQueueStatus
            lblQueueStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblQueueStatus.Location = new Point(150, 160);
            lblQueueStatus.Name = "lblQueueStatus";
            lblQueueStatus.Size = new Size(190, 20);
            lblQueueStatus.Text = "Queue: Empty";
            lblQueueStatus.ForeColor = Color.FromArgb(60, 60, 60);
            lblQueueStatus.BringToFront();

            // lblGroundFloor
            lblGroundFloor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGroundFloor.Location = new Point(15, 85);
            lblGroundFloor.Name = "lblGroundFloor";
            lblGroundFloor.Size = new Size(190, 20);
            lblGroundFloor.Text = "Ground Floor Status:";

            // indicatorGround
            indicatorGround.BackColor = Color.FromArgb(144, 238, 144);
            indicatorGround.BorderStyle = BorderStyle.FixedSingle;
            indicatorGround.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            indicatorGround.Location = new Point(15, 110);
            indicatorGround.Name = "indicatorGround";
            indicatorGround.Size = new Size(225, 35);
            indicatorGround.Text = " - ";
            indicatorGround.TextAlign = ContentAlignment.MiddleCenter;

            // lblFirstFloor
            lblFirstFloor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFirstFloor.Location = new Point(15, 15);
            lblFirstFloor.Name = "lblFirstFloor";
            lblFirstFloor.Size = new Size(190, 20);
            lblFirstFloor.Text = "First Floor Status:";

            // indicatorFirst
            indicatorFirst.BackColor = Color.FromArgb(211, 211, 211);
            indicatorFirst.BorderStyle = BorderStyle.FixedSingle;
            indicatorFirst.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            indicatorFirst.Location = new Point(15, 40);
            indicatorFirst.Name = "indicatorFirst";
            indicatorFirst.Size = new Size(225, 35);
            indicatorFirst.Text = " - ";
            indicatorFirst.TextAlign = ContentAlignment.MiddleCenter;

            // lblActivityLog
            lblActivityLog.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblActivityLog.Location = new Point(15, 160);
            lblActivityLog.Name = "lblActivityLog";
            lblActivityLog.Size = new Size(120, 20);
            lblActivityLog.Text = "Activity Log:";

            // logBox
            logBox.BackColor = Color.White;
            logBox.BorderStyle = BorderStyle.FixedSingle;
            logBox.Font = new Font("Consolas", 7.5F);
            logBox.Location = new Point(15, 185);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Size = new Size(240, 120);

            // btnCallGround
            btnCallGround.BackColor = Color.FromArgb(0, 122, 204);
            btnCallGround.FlatStyle = FlatStyle.Flat;
            btnCallGround.FlatAppearance.BorderSize = 0;
            btnCallGround.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCallGround.ForeColor = Color.White;
            btnCallGround.Location = new Point(15, 315);
            btnCallGround.Name = "btnCallGround";
            btnCallGround.Size = new Size(90, 35);
            btnCallGround.Text = "📞 Ground";
            btnCallGround.UseVisualStyleBackColor = false;
            btnCallGround.Click += BtnCallGround_Click;
            

            // btnCallFirst
            btnCallFirst.BackColor = Color.FromArgb(0, 122, 204);
            btnCallFirst.FlatStyle = FlatStyle.Flat;
            btnCallFirst.FlatAppearance.BorderSize = 0;
            btnCallFirst.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCallFirst.ForeColor = Color.White;
            btnCallFirst.Location = new Point(150, 315);
            btnCallFirst.Name = "btnCallFirst";
            btnCallFirst.Size = new Size(90, 35);
            btnCallFirst.Text = "📞 First";
            btnCallFirst.UseVisualStyleBackColor = false;
            btnCallFirst.Click += BtnCallFirst_Click;
            

            // btnViewDB
            btnViewDB.BackColor = Color.FromArgb(70, 130, 180);
            btnViewDB.FlatStyle = FlatStyle.Flat;
            btnViewDB.FlatAppearance.BorderSize = 0;
            btnViewDB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewDB.ForeColor = Color.White;
            btnViewDB.Location = new Point(15, 355);
            btnViewDB.Name = "btnViewDB";
            btnViewDB.Size = new Size(225, 35);
            btnViewDB.Text = "💾 Update and View Database"; 
            btnViewDB.UseVisualStyleBackColor = false;
            btnViewDB.Click += BtnViewDB_Click;
            

            // MainWindow
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 500);
            Controls.Add(controlPanel);
            Controls.Add(elevatorB);
            Controls.Add(elevatorA);
            Controls.Add(lblTitle);
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Elevator Control System";
            controlPanel.ResumeLayout(false);
            controlPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}