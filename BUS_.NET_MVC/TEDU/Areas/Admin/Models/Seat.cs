namespace API.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int? TicketId { get; set; }
        public string Status { get; set; }
        public string SeatName { get; set; }
    }
}
