namespace ElevatorApp.States
{
    public class DoorsOpenState : IElevatorState
    {
        public async void GoToFloor(Elevator elevator, int floor)
        {
            // Close doors first
            elevator.SetState(new DoorsClosingState());
            await elevator.CloseDoorsAsync();

            // Return to idle and request movement
            elevator.SetState(new IdleState());
            elevator.GoToFloor(floor);
        }

        public void OpenDoors(Elevator elevator)
        {
            // Already open - ignore
        }

        public async void CloseDoors(Elevator elevator)
        {
            elevator.SetState(new DoorsClosingState());
            await elevator.CloseDoorsAsync();
            elevator.SetState(new IdleState());
        }

        public string GetStateName() => "Doors Open";
    }
}