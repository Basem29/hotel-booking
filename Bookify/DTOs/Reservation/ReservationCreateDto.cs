using System.ComponentModel.DataAnnotations;

namespace Bookify.DTOs.Reservation
{
    public class ReservationCreateDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
