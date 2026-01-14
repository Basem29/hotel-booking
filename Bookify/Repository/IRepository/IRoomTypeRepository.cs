using Bookify.Models;

namespace Bookify.Repository.IRepository
{
    public interface IRoomTypeRepository: IRepository<RoomType, int>
    {
        Task<RoomType?> GetRoomTypeWithMediaAsync(int id);
    }
}
