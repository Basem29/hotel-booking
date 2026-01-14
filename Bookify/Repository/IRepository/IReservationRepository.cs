using Bookify.Models;

namespace Bookify.Repository.IRepository
{
    public interface IReservationRepository: IRepository<Reservation, int>
    {
        Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync( DateTime checkIn, DateTime checkOut);


    }
}
