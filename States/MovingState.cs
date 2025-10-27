namespace ElevatorApp.States
{
    public class MovingState : IElevatorState
    {
        public void GoToFloor(Elevator elevator, int floor)
        {
            // Cannot accept new floor while moving
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Cannot go to floor {floor} - currently moving");
        }

        public void OpenDoors(Elevator elevator)
        {
            // Cannot open doors while moving
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Cannot open doors - currently moving");
        }

        public void CloseDoors(Elevator elevator)
        {
            // Already closed while moving
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Doors are already closed - currently moving");
        }

        public string GetStateName() => "Moving";
    }
}