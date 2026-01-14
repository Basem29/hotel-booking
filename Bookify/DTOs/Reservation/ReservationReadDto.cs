namespace Bookify.DTOs.Reservation
{
    public class ReservationReadDto
    {
    
            public int Id { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public decimal TotalPrice { get; set; }
            public string Status { get; set; }
            public int RoomId { get; set; }
            public string UserId { get; set; }

        

    }
}
