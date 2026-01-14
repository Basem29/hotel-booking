namespace Bookify.DTOs.Rooms
{
    public class RoomReadDTO
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; } 
        public int RoomTypeId { get; set; }

        public string RoomTypeName { get; set; } = default!; 
    }
}