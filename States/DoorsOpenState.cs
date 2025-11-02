using ElevatorApp.UI;

namespace ElevatorApp.States
{
    public class DoorsOpenState : IElevatorState
    {
        public void GoToFloor(Elevator elevator, int floor)
        {
            // Close doors first
            elevator.SetState(new DoorsClosingState());
            elevator.CloseDoors(() =>
            {
                // Return to idle and request movement
                elevator.SetState(new IdleState());
                elevator.MoveToFloor(floor);
            });
        }

        public void OpenDoors(Elevator elevator)
        {
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Doors are already open");
        }

        public void CloseDoors(Elevator elevator)
        {
            elevator.SetState(new DoorsClosingState());
            elevator.CloseDoors(() =>
            {
                elevator.SetState(new IdleState());
            });
        }

        public string GetStateName() => "Doors Open";
    }
}