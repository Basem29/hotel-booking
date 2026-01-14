using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public class RoomType
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string Name { get; set; } 

        [Required]
        public required string Description { get; set; } 

        [Required]
        public decimal Price { get; set; } 

        public int Capacity { get; set; } 

        public ICollection<Room> Rooms { get; set; } = new List<Room>(); 
        public ICollection<Media> Medias { get; set; } = new List<Media>(); 
    }
}