namespace Bookify.DTOs.Reservations
{
    public class ReservationCreateDTO
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomId { get; set; }
    }
}