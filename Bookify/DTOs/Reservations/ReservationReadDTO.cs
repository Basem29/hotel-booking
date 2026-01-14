namespace Bookify.DTOs.Reservations
{
    public class ReservationReadDTO
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = default!;
        public int RoomId { get; set; }
        public string RoomName { get; set; } = default!; 
        public string UserId { get; set; } = default!;
        public string UserFullName { get; set; } = default!;
    }
}