using ElevatorApp.Events;
using ElevatorApp.States;
using Timer = System.Windows.Forms.Timer;

namespace ElevatorApp.UI
{
    public partial class Elevator
    {
        public string ElevatorName { get; }
        public int CurrentFloor { get; private set; }
        public int TargetFloor;
        public bool IsBusy => _currentState is not IdleState and not DoorsOpenState;
        private string FloorDisplay => CurrentFloor == 0 ? "G" : "1";

        // Track door open/closed status
        internal bool DoorsOpen = false;



        // Door and floor position 
        private const int DoorClosedLeft = 0;
        private const int DoorClosedRight = 50;
        private const int DoorOpenLeft = -50;
        private const int DoorOpenRight = 100;
        private const int GroundFloorY = 290;
        private const int FirstFloorY = 0;
        private const int DoorAnimationInterval = 20;
        private const int MovementStepDelay = 20;

        // State pattern 
        private IElevatorState _currentState;

        // Door animation state
        private readonly Timer _doorTimer;
        private int _doorStep;
        private bool _opening;
        private Action? _onDoorAnimationComplete;

        // Movement animation state
        private readonly Timer _movementTimer;
        private int _targetFloorY;
        private int _movementStep;
        private Action? _onMovementComplete;

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
            _currentState = new IdleState();

            ResetDoors();

            _doorTimer = new Timer { Interval = DoorAnimationInterval };
            _doorTimer.Tick += DoorTimer_Tick;

            _movementTimer = new Timer { Interval = MovementStepDelay };
            _movementTimer.Tick += MovementTimer_Tick;

            // Buttons Event Handlers - Clicks Effects
            this.btnGround.MouseEnter += (s, e) => btnGround.BackColor = Color.FromArgb(0, 121, 107);
            this.btnGround.MouseLeave += (s, e) => btnGround.BackColor = Color.FromArgb(0, 150, 136);
            this.btnFirst.MouseEnter += (s, e) => btnFirst.BackColor = Color.FromArgb(0, 121, 107);
            this.btnFirst.MouseLeave += (s, e) => btnFirst.BackColor = Color.FromArgb(0, 150, 136);

        }

        // Request elevator to go to specified floor
        public void MoveToFloor(int floor)
        {
            TargetFloor = floor;
            _currentState.GoToFloor(this, floor);
        }

        // Change elevator state
        public void SetState(IElevatorState newState)
        {
            _currentState = newState;
            UpdateStatusLabel();
            NotifyStateChanged(_currentState.GetStateName());
        }


        // Move cabin to specified floor with animation
        internal void MoveCabin(int floor, Action? onComplete = null)
        {
            int startY = GetFloorYPosition(CurrentFloor);
            _targetFloorY = GetFloorYPosition(floor);
            _movementStep = startY < _targetFloorY ? 5 : -5;
            _onMovementComplete = () =>
            {
                CurrentFloor = floor;
                UpdateStatusLabel();
                NotifyMoved(CurrentFloor);
                onComplete?.Invoke();
            };

            _movementTimer.Start();
        }

        // Door operations for states
        internal void OpenDoors(Action? onComplete = null) => AnimateDoors(true, onComplete);
        internal void CloseDoors(Action? onComplete = null) => AnimateDoors(false, onComplete);

        // Notify that movement is complete
        internal void NotifyMovementComplete()
        {
            OnMovementComplete?.Invoke(this, EventArgs.Empty);
        }

        // Get Y coordinate for floor position
        internal static int GetFloorYPosition(int floor) => floor == 0 ? GroundFloorY : FirstFloorY;

        // Animate door opening/closing
        private void AnimateDoors(bool open, Action? onComplete = null)
        {
            if (DoorsOpen == open)
            {
                onComplete?.Invoke();
                return;
            }

            if (open)
                ResetDoors();

            _doorStep = 0;
            _opening = open;
            _onDoorAnimationComplete = () =>
            {
                DoorsOpen = open;
                onComplete?.Invoke();
            };

            _doorTimer.Start();
        }

        // Timer tick handler for door animation
        private void DoorTimer_Tick(object? sender, EventArgs e)
        {
            const int MAX_STEPS = 25;
            const int STEP_INCREMENT = 2;

            _doorStep++;

            int leftX = _opening ? DoorClosedLeft - _doorStep * STEP_INCREMENT : DoorOpenLeft + _doorStep * STEP_INCREMENT;

            int rightX = _opening ? DoorClosedRight + _doorStep * STEP_INCREMENT : DoorOpenRight - _doorStep * STEP_INCREMENT;

            doorLeft.Location = new Point(leftX, 0);
            doorRight.Location = new Point(rightX, 0);

            // Check if animation finished
            if (_doorStep >= MAX_STEPS)
            {
                _doorTimer.Stop();
                doorLeft.Location = new Point(_opening ? DoorOpenLeft : DoorClosedLeft, 0);
                doorRight.Location = new Point(_opening ? DoorOpenRight : DoorClosedRight, 0);
                _onDoorAnimationComplete?.Invoke();
            }
        }

        // Timer tick handler for movement animation
        private void MovementTimer_Tick(object? sender, EventArgs e)
        {
            int currentY = cabin.Location.Y;

            // Check if reached target
            if ((_movementStep > 0 && currentY >= _targetFloorY) ||
                (_movementStep < 0 && currentY <= _targetFloorY))
            {
                _movementTimer.Stop();
                cabin.Location = new Point(cabin.Location.X, _targetFloorY);
                _onMovementComplete?.Invoke();
            }
            else
            {
                // Continue animation
                cabin.Location = new Point(cabin.Location.X, currentY + _movementStep);
            }
        }

        // Reset doors to closed position
        private void ResetDoors()
        {
            doorLeft.Location = new Point(DoorClosedLeft, 0);
            doorRight.Location = new Point(DoorClosedRight, 0);
            DoorsOpen = false;
        }

        // Update UI status label with current floor and state
        private void UpdateStatusLabel()
        {
            statusLabel.Text = $"Floor: {FloorDisplay}\nStatus: {_currentState.GetStateName()}";
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
        private void BtnGround_Click(object sender, EventArgs e) => MoveToFloor(0);
        private void BtnFirst_Click(object sender, EventArgs e) => MoveToFloor(1);
    }
}