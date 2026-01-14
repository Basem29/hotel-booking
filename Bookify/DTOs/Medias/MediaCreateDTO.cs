using Microsoft.AspNetCore.Http;
using Bookify.Models;

namespace Bookify.DTOs.Medias
{
    public class MediaCreateDTO
    {
        public int RoomTypeId { get; set; }
        public MediaType MediaType { get; set; }

        public IFormFile File { get; set; } 
    }
}