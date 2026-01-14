using Bookify.Models;

namespace Bookify.Repository.IRepository
{
    public interface IMediaRepository: IRepository<Media, int>
    {
        Task<IEnumerable<Media>> GetMediaByRoomTypeAsync(int roomTypeId);
    }
}
