using System.ComponentModel.DataAnnotations;

namespace Bookify.DTOs.Rooms
{
    public class RoomCreateDTO
    {
        [Required]
        public int RoomNumber { get; set; } 

        [Required]
        public int RoomTypeId { get; set; }
    }
}