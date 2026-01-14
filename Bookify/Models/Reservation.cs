using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateTime CheckIn { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        public required DateTime CheckOut { get; set; } 

        [Required]
        public decimal TotalPrice { get; set; } 

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending; 

        [Required]
        public int RoomId { get; set; } 

        [ForeignKey("RoomId")]
        public Room? Room { get; set; } 

        [Required]
        public required string UserId { get; set; } 

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; } 
    }
}