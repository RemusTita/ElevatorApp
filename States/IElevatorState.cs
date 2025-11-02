using ElevatorApp.UI;

namespace ElevatorApp.States
{
    public interface IElevatorState
    {
        void GoToFloor(Elevator elevator, int floor);
        void OpenDoors(Elevator elevator);
        void CloseDoors(Elevator elevator);
        string GetStateName();
    }
}