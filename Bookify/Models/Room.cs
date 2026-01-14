using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public int RoomNumber { get; set; } 

        [Required]
        public int Floor { get; set; } 

        public RoomStatus Status { get; set; } = RoomStatus.Available; 

        public int RoomTypeId { get; set; }

        [ForeignKey("RoomTypeId")]
        [ValidateNever]
        public RoomType? RoomType { get; set; } 
    }
}