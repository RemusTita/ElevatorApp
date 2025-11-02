using ElevatorApp.UI;

namespace ElevatorApp.States
{
    public class MovingState : IElevatorState
    {
        public void GoToFloor(Elevator elevator, int floor)
        {
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Cannot go to floor {floor} - currently moving");
        }

        public void OpenDoors(Elevator elevator)
        {
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Cannot open doors - currently moving");
        }

        public void CloseDoors(Elevator elevator)
        {
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Doors are already closed - currently moving");
        }

        public string GetStateName() => "Moving";
    }
}