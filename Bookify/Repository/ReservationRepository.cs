using Bookify.Models;
using Bookify.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bookify.Repository
{
    public class ReservationRepository : Repository<Reservation, int>, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context) { }



        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
        {
            return await _db.Rooms
                .Where(room => !_db.Reservations.Any(r =>
                    r.RoomId == room.Id &&
                    r.Status != ReservationStatus.Cancelled &&
                    (
                        (checkIn >= r.CheckIn && checkIn < r.CheckOut) ||
                        (checkOut > r.CheckIn && checkOut <= r.CheckOut) ||
                        (checkIn <= r.CheckIn && checkOut >= r.CheckOut)
                    )
                ))
                .ToListAsync();
        }


        public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId)
       {
           return await dbSet.Where(r => r.UserId == userId)
                             .Include(r => r.Room)
                             .ThenInclude(rt => rt.RoomType)
                             .ToListAsync();
       }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            return !await dbSet.AnyAsync(r =>
                r.RoomId == roomId &&
                r.Status != ReservationStatus.Cancelled &&
                (
                    (checkIn >= r.CheckIn && checkIn < r.CheckOut) ||
                    (checkOut > r.CheckIn && checkOut <= r.CheckOut) ||
                    (checkIn <= r.CheckIn && checkOut >= r.CheckOut)
                ));
        }

        public async Task<bool> IsRoomReservedAsync(int roomId, DateTime checkIn, DateTime checkOut)
       {
           return await dbSet.AnyAsync(r =>
               r.RoomId == roomId &&
               r.CheckIn<checkOut &&
               r.CheckOut> checkIn);
       }
    }
}