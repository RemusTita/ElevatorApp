namespace ElevatorApp.States
{
    public class DoorsClosingState : IElevatorState
    {
        public void GoToFloor(Elevator elevator, int floor)
        {
            System.Diagnostics.Debug.WriteLine("Cannot go to floor while doors are closing.");
        }

        public void OpenDoors(Elevator elevator)
        {
            System.Diagnostics.Debug.WriteLine("Cannot open doors while they are closing.");
        }

        public void CloseDoors(Elevator elevator)
        {
            System.Diagnostics.Debug.WriteLine("Doors are already closing.");
        }

        public string GetStateName() => "Doors Closing";
    }
}