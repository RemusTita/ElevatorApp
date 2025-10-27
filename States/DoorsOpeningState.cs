namespace ElevatorApp.States
{
    public class DoorsOpeningState : IElevatorState
    {
        public void GoToFloor(Elevator elevator, int floor)
        {
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Cannot go to floor {floor} - currently moving");

        }

        public void OpenDoors(Elevator elevator)
        {
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Doors are already opening");
        }

        public void CloseDoors(Elevator elevator)
        {
            System.Diagnostics.Debug.WriteLine($"{elevator.ElevatorName}: Cannot close doors - currently opening");
        }

        public string GetStateName() => "Doors Opening";
    }
}