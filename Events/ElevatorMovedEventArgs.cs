namespace ElevatorApp.Events
{
    public class ElevatorMovedEventArgs(string elevatorName, int floor) : EventArgs
    {
        public string ElevatorName { get; } = elevatorName;
        public int Floor { get; } = floor;
    }
}