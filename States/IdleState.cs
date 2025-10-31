namespace ElevatorApp.States
{
    public class IdleState : IElevatorState
    {
        public async void GoToFloor(Elevator elevator, int floor)
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
                await elevator.CloseDoorsAsync();
                await Task.Delay(500);
            }

            // Move
            elevator.SetState(new MovingState());
            await elevator.MoveCabin(floor);
            await Task.Delay(500);

            // Open doors
            elevator.SetState(new DoorsOpeningState());
            await elevator.OpenDoorsAsync();

            elevator.SetState(new DoorsOpenState());
            await Task.Delay(2000);

            // Close doors
            elevator.SetState(new DoorsClosingState());
            await elevator.CloseDoorsAsync();

            // Return to idle
            elevator.SetState(new IdleState());
            elevator.NotifyMovementComplete();
        }

        public async void OpenDoors(Elevator elevator)
        {
            elevator.SetState(new DoorsOpeningState());
            await elevator.OpenDoorsAsync();

            elevator.SetState(new DoorsOpenState());
            await Task.Delay(2000);

            elevator.SetState(new DoorsClosingState());
            await elevator.CloseDoorsAsync();

            elevator.SetState(new IdleState());
        }

        public async void CloseDoors(Elevator elevator)
        {
            if (elevator.DoorsOpen)
            {
                elevator.SetState(new DoorsClosingState());
                await elevator.CloseDoorsAsync();
            }
        }

        public string GetStateName() => "Idle";
    }
}