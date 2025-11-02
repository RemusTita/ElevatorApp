using ElevatorApp.UI;

namespace ElevatorApp.States
{
    public class IdleState : IElevatorState
    {
        public void GoToFloor(Elevator elevator, int floor)
        {
            if (elevator.CurrentFloor == floor)
            {
                OpenDoors(elevator);
                return;
            }

            // Close doors if open
            if (elevator.DoorsOpen)
            {
                elevator.SetState(new DoorsClosingState());
                elevator.CloseDoors(() =>
                {
                    // Wait 500ms then move
                    var timer = new System.Windows.Forms.Timer { Interval = 500 };
                    timer.Tick += (s, e) =>
                    {
                        timer.Stop();
                        timer.Dispose();
                        StartMovement(elevator, floor);
                    };
                    timer.Start();
                });
            }
            else
            {
                StartMovement(elevator, floor);
            }
        }

        private static void StartMovement(Elevator elevator, int floor)
        {
            // Move
            elevator.SetState(new MovingState());
            elevator.MoveCabin(floor, () =>
            {
                // Wait 500ms then open doors
                var timer = new System.Windows.Forms.Timer { Interval = 500 };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    OpenDoorsAfterMovement(elevator);
                };
                timer.Start();
            });
        }

        private static void OpenDoorsAfterMovement(Elevator elevator)
        {
            // Open doors
            elevator.SetState(new DoorsOpeningState());
            elevator.OpenDoors(() =>
            {
                elevator.SetState(new DoorsOpenState());

                // Wait 2000ms with doors open
                var timer = new System.Windows.Forms.Timer { Interval = 2000 };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    CloseDoorsAfterWait(elevator);
                };
                timer.Start();
            });
        }

        private static void CloseDoorsAfterWait(Elevator elevator)
        {
            // Close doors
            elevator.SetState(new DoorsClosingState());
            elevator.CloseDoors(() =>
            {
                // Return to idle
                elevator.SetState(new IdleState());
                elevator.NotifyMovementComplete();
            });
        }

        public void OpenDoors(Elevator elevator)
        {
            elevator.SetState(new DoorsOpeningState());
            elevator.OpenDoors(() =>
            {
                elevator.SetState(new DoorsOpenState());

                // Wait 2000ms with doors open
                var timer = new System.Windows.Forms.Timer { Interval = 2000 };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    timer.Dispose();

                    elevator.SetState(new DoorsClosingState());
                    elevator.CloseDoors(() =>
                    {
                        elevator.SetState(new IdleState());
                    });
                };
                timer.Start();
            });
        }

        public void CloseDoors(Elevator elevator)
        {
            if (elevator.DoorsOpen)
            {
                elevator.SetState(new DoorsClosingState());
                elevator.CloseDoors();
            }
        }

        public string GetStateName() => "Idle";
    }
}