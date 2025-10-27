using ElevatorApp.Data;
using ElevatorApp.Events;
using ElevatorApp.Models;
using System.ComponentModel;

namespace ElevatorApp
{
    public partial class MainWindow : Form
    {
        private readonly Database database;
        private readonly Queue<ElevatorRequest> requestQueue = new();


        // Initialize form and subscribe to elevator events
        public MainWindow()
        {
            InitializeComponent();
            database = new Database();

            // Buttons Event Handlers - Clicks Effects
            btnCallGround.MouseEnter += (s, e) => btnCallGround.BackColor = Color.FromArgb(0, 100, 180);
            btnCallGround.MouseLeave += (s, e) => btnCallGround.BackColor = Color.FromArgb(0, 122, 204);
            btnCallFirst.MouseEnter += (s, e) => btnCallFirst.BackColor = Color.FromArgb(0, 100, 180);
            btnCallFirst.MouseLeave += (s, e) => btnCallFirst.BackColor = Color.FromArgb(0, 122, 204);
            btnViewDB.MouseEnter += (s, e) => btnViewDB.BackColor = Color.FromArgb(60, 110, 160);
            btnViewDB.MouseLeave += (s, e) => btnViewDB.BackColor = Color.FromArgb(70, 130, 180);



            // Subscribe to Elevator A events
            elevatorA.ElevatorMoved += OnElevatorMoved;
            elevatorA.StateChanged += OnElevatorStateChanged;
            elevatorA.OnMovementComplete += (o, e) => ProcessQueue();

            // Subscribe to Elevator B events
            elevatorB.ElevatorMoved += OnElevatorMoved;
            elevatorB.StateChanged += OnElevatorStateChanged;
            elevatorB.OnMovementComplete += (o, e) => ProcessQueue();

            UpdateFloorIndicators();
            UpdateQueueDisplay();
            AddLog("System initialized");
        }

        // Handle elevator arrival at floor
        private void OnElevatorMoved(object? sender, ElevatorMovedEventArgs e)
        {
            UpdateFloorIndicators();
            string floorName = e.Floor == 0 ? "Ground" : "First";
            AddLog($"{e.ElevatorName} arrived at {floorName}");
            database.LogEvent(e.ElevatorName, "Arrived", floorName);
        }

        // Handle elevator state changes
        private void OnElevatorStateChanged(object? sender, ElevatorStateChangedEventArgs e)
        {
            AddLog($"{e.ElevatorName}: {e.State}");
        }

        // Update floor indicator labels
        private void UpdateFloorIndicators()
        {
            UpdateFloorIndicator(indicatorGround, 0);
            UpdateFloorIndicator(indicatorFirst, 1);
        }

        // Update individual floor indicator with elevator presence
        private void UpdateFloorIndicator(Label indicator, int floor)
        {
            var elevators = new List<string>();
            if (elevatorA.CurrentFloor == floor) elevators.Add("Elevator A");
            if (elevatorB.CurrentFloor == floor) elevators.Add("Elevator B");

            indicator.BackColor = elevators.Any() ? Color.FromArgb(144, 238, 144) : Color.FromArgb(211, 211, 211);
            indicator.Text = elevators.Any() ? string.Join(", ", elevators) : "-";
        }

        // Process next request in queue when elevator becomes available
        private void ProcessQueue()
        {
            UpdateFloorIndicators();

            if (requestQueue.Count == 0 || (elevatorA.IsBusy && elevatorB.IsBusy))
                return;

            var request = requestQueue.Dequeue();
            string floorName = request.Floor == 0 ? "Ground" : "First";
            AddLog($"Processing queued request: {floorName}");
            UpdateQueueDisplay();
            CallElevator(request.Floor);
        }

        // Update queue status display
        private void UpdateQueueDisplay()
        {
            if (lblQueueStatus.InvokeRequired)
            {
                lblQueueStatus.Invoke(UpdateQueueDisplay);
                return;
            }

            var hasRequests = requestQueue.Count > 0;
            lblQueueStatus.ForeColor = hasRequests ? Color.FromArgb(220, 53, 69) : Color.Gray;
            lblQueueStatus.Text = hasRequests
                ? $"Queue ({requestQueue.Count}): {string.Join(", ", requestQueue.Select(r => r.Floor == 0 ? "G" : "1"))}"
                : "Queue: Empty";
        }

        // Add floor request to queue
        private void AddToQueue(int floor)
        {
            var floorName = floor == 0 ? "Ground" : "First";

            if (requestQueue.Any(r => r.Floor == floor))
            {
                AddLog($"{floorName} already in queue");
                return;
            }

            requestQueue.Enqueue(new ElevatorRequest(floor));
            AddLog($"Added to queue: {floorName}");
            UpdateQueueDisplay();
        }

        // Dispatch elevator to requested floor
        private void CallElevator(int floor)
        {
            var floorName = floor == 0 ? "Ground" : "First";

            if (IsFloorBeingServiced(floor))
            {
                AddLog($"{floorName} already being serviced");
                return;
            }

            var elevator = GetAvailableElevator(floor);

            if (elevator == null)
            {
                AddToQueue(floor);
                return;
            }

            if (elevator.CurrentFloor == floor)
            {
                AddLog($"{elevator.ElevatorName} already at {floorName}");
                elevator.GoToFloor(floor);
                return;
            }

            AddLog($"Dispatching {elevator.ElevatorName} to {floorName}");
            database.LogEvent(elevator.ElevatorName, "Called", floorName);
            elevator.GoToFloor(floor);
        }

        // Find available elevator, preferring closest one
        private Elevator? GetAvailableElevator(int floor)
        {
            var aAvailable = !elevatorA.IsBusy;
            var bAvailable = !elevatorB.IsBusy;

            if (aAvailable && bAvailable)
                return Math.Abs(elevatorA.CurrentFloor - floor) <= Math.Abs(elevatorB.CurrentFloor - floor)
                    ? elevatorA : elevatorB;

            return aAvailable ? elevatorA : bAvailable ? elevatorB : null;
        }

        // Check if any elevator is already heading to floor
        private bool IsFloorBeingServiced(int floor)
        {
            return (elevatorA.IsBusy && elevatorA.targetFloor == floor) ||
                   (elevatorB.IsBusy && elevatorB.targetFloor == floor);
        }

        // Add timestamped message to log
        private void AddLog(string message)
        {
            if (logBox.InvokeRequired)
            {
                logBox.Invoke(() => AddLog(message));
                return;
            }

            logBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }

        // Button click handlers
        private void BtnCallGround_Click(object sender, EventArgs e) => CallElevator(0);
        private void BtnCallFirst_Click(object sender, EventArgs e) => CallElevator(1);

        // Update database and show viewer
        private void BtnViewDB_Click(object sender, EventArgs e)
        {
            try
            {
                var worker = new BackgroundWorker();
                worker.DoWork += (s, args) =>
                {
                    AddLog("Updating database...");
                    database.UpdateDatabase();
                };
                worker.RunWorkerCompleted += (s, args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show($"Error updating database: {args.Error.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    AddLog($"Database updated successfully. {database.GetRecordCount()} records.");
                    new ViewDatabase(database).ShowDialog();
                };
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update and view database: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}