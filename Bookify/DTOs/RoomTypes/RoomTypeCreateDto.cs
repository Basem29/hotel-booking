namespace Bookify.DTOs.RoomTypes
{
    public class RoomTypeCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}