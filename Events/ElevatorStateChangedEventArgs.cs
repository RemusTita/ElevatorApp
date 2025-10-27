namespace ElevatorApp.Events
{
    public class ElevatorStateChangedEventArgs(string elevatorName, string state) : EventArgs
    {
        public string ElevatorName { get; } = elevatorName;
        public string State { get; } = state;
    }
}