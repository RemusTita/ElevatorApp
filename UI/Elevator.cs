using ElevatorApp.Events;
using ElevatorApp.States;
using Timer = System.Windows.Forms.Timer;

namespace ElevatorApp
{
    public partial class Elevator : UserControl
    {
        public string ElevatorName { get; }
        public int CurrentFloor { get; private set; }
        public int targetFloor;
        public bool IsBusy => currentState is not IdleState and not DoorsOpenState;
        private string FloorDisplay => CurrentFloor == 0 ? "G" : "1";

        // Track door open/closed status
        internal bool doorsOpen = false;



        // Door and floor position 
        private const int DOOR_CLOSED_LEFT = 0;
        private const int DOOR_CLOSED_RIGHT = 50;
        private const int DOOR_OPEN_LEFT = -50;
        private const int DOOR_OPEN_RIGHT = 100;
        private const int GROUND_FLOOR_Y = 290;
        private const int FIRST_FLOOR_Y = 0;
        private const int DOOR_ANIMATION_INTERVAL = 20;
        private const int MOVEMENT_STEP_DELAY = 20;

        // State pattern 
        private IElevatorState currentState;

        // Door animation state
        private readonly Timer doorTimer;
        private int doorStep;
        private bool opening;
        private Action? onDoorAnimationComplete;

        // Observer pattern - events for state changes
        public event EventHandler<ElevatorMovedEventArgs>? ElevatorMoved;
        public event EventHandler<ElevatorStateChangedEventArgs>? StateChanged;
        public event EventHandler? OnMovementComplete;

        public Elevator(Point location, string elevatorName)
        {
            ElevatorName = elevatorName;
            InitializeComponent();
            Location = location;
            CurrentFloor = 0;
            currentState = new IdleState();

            ResetDoors();

            doorTimer = new Timer { Interval = DOOR_ANIMATION_INTERVAL };
            doorTimer.Tick += DoorTimer_Tick;



            // Buttons Event Handlers - Clicks Effects
            this.btnGround.MouseEnter += (s, e) => btnGround.BackColor = Color.FromArgb(0, 121, 107);
            this.btnGround.MouseLeave += (s, e) => btnGround.BackColor = Color.FromArgb(0, 150, 136);
            this.btnFirst.MouseEnter += (s, e) => btnFirst.BackColor = Color.FromArgb(0, 121, 107);
            this.btnFirst.MouseLeave += (s, e) => btnFirst.BackColor = Color.FromArgb(0, 150, 136);

        }

        // Request elevator to go to specified floor
        public void GoToFloor(int floor)
        {
            targetFloor = floor;
            currentState.GoToFloor(this, floor);
        }

        // Change elevator state
        public void SetState(IElevatorState newState)
        {
            currentState = newState;
            UpdateStatusLabel();
            NotifyStateChanged(currentState.GetStateName());
        }


        // Move cabin to specified floor with animation
        internal async Task MoveCabin(int floor)
        {
            int startY = GetFloorYPosition(CurrentFloor);
            int endY = GetFloorYPosition(floor);
            int step = startY < endY ? 5 : -5;

            // Animate movement
            while ((step > 0 && cabin.Location.Y < endY) ||
                   (step < 0 && cabin.Location.Y > endY))
            {
                cabin.Location = new Point(cabin.Location.X, cabin.Location.Y + step);
                await Task.Delay(MOVEMENT_STEP_DELAY);
            }

            // Set final position
            cabin.Location = new Point(cabin.Location.X, endY);
            CurrentFloor = floor;

            UpdateStatusLabel();
            NotifyMoved(CurrentFloor);
        }

        // Door operations for states
        internal Task OpenDoorsAsync() => AnimateDoorsAsync(true);
        internal Task CloseDoorsAsync() => AnimateDoorsAsync(false);

        // Notify that movement is complete
        internal void NotifyMovementComplete()
        {
            OnMovementComplete?.Invoke(this, EventArgs.Empty);
        }

        // Get Y coordinate for floor position
        internal static int GetFloorYPosition(int floor) => floor == 0 ? GROUND_FLOOR_Y : FIRST_FLOOR_Y;

        // Animate door opening/closing
        private Task AnimateDoorsAsync(bool open)
        {
            if (doorsOpen == open)
                return Task.CompletedTask;

            var tcs = new TaskCompletionSource<bool>();

            if (open)
                ResetDoors();

            doorStep = 0;
            opening = open;
            onDoorAnimationComplete = () =>
            {
                doorsOpen = open;
                tcs.SetResult(true);
            };

            doorTimer.Start();
            return tcs.Task;
        }

        // Timer tick handler for door animation
        private void DoorTimer_Tick(object? sender, EventArgs e)
        {
            const int MAX_STEPS = 25;
            const int STEP_INCREMENT = 2;

            doorStep++;

            int leftX = opening ? DOOR_CLOSED_LEFT - doorStep * STEP_INCREMENT : DOOR_OPEN_LEFT + doorStep * STEP_INCREMENT;

            int rightX = opening ? DOOR_CLOSED_RIGHT + doorStep * STEP_INCREMENT : DOOR_OPEN_RIGHT - doorStep * STEP_INCREMENT;

            doorLeft.Location = new Point(leftX, 0);
            doorRight.Location = new Point(rightX, 0);

            // Check if animation finished
            if (doorStep >= MAX_STEPS)
            {
                doorTimer.Stop();
                doorLeft.Location = new Point(opening ? DOOR_OPEN_LEFT : DOOR_CLOSED_LEFT, 0);
                doorRight.Location = new Point(opening ? DOOR_OPEN_RIGHT : DOOR_CLOSED_RIGHT, 0);
                onDoorAnimationComplete?.Invoke();
            }
        }

        // Reset doors to closed position
        private void ResetDoors()
        {
            doorLeft.Location = new Point(DOOR_CLOSED_LEFT, 0);
            doorRight.Location = new Point(DOOR_CLOSED_RIGHT, 0);
            doorsOpen = false;
        }

        // Update UI status label with current floor and state
        private void UpdateStatusLabel()
        {
            statusLabel.Text = $"Floor: {FloorDisplay}\nStatus: {currentState.GetStateName()}";
        }

        // Notify observers that elevator moved
        private void NotifyMoved(int floor)
        {
            ElevatorMoved?.Invoke(this, new ElevatorMovedEventArgs(ElevatorName, floor));
        }

        // Notify observers of state change
        private void NotifyStateChanged(string state)
        {
            StateChanged?.Invoke(this, new ElevatorStateChangedEventArgs(ElevatorName, state));
        }

        // Button click handlers
        private void BtnGround_Click(object sender, EventArgs e) => GoToFloor(0);
        private void BtnFirst_Click(object sender, EventArgs e) => GoToFloor(1);
    }
}