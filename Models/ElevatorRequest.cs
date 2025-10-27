namespace ElevatorApp.Models
{
    public class ElevatorRequest(int floor)
    {
        public int Floor { get; set; } = floor;
        public DateTime RequestTime { get; set; } = DateTime.Now;
    }
}
