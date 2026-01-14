using Bookify.Models;

namespace Bookify.Repository.IRepository
{
    public interface IRoomRepository:IRepository<Room, int>
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<Room?> GetRoomWithTypeAsync(int id);
    }
}
