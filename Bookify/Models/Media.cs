using Bookify.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Models
{
    public class Media
    {
        public int Id { get; set; }

        public int RoomTypeId { get; set; }

        public RoomType? RoomType { get; set; } 

        [Required]
        public MediaType MediaType { get; set; } 

        [Required]
        public required string FileName { get; set; } 

        [Required]
        public required string ContentType { get; set; } 

        [Required]
        public required byte[] Data { get; set; } 
    }
}