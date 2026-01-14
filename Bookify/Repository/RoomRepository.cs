using Bookify.Models;
using Bookify.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class RoomRepository : Repository<Room, int>, IRoomRepository
{
    public RoomRepository(AppDbContext context) : base(context) { }

   public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
   {
       return await dbSet.Where(r => r.Status == RoomStatus.Available)
                        .Include(r => r.RoomType)
                        .ToListAsync();
   }

   public async Task<Room?> GetRoomWithTypeAsync(int id)
   {
           return await dbSet.Include(r => r.RoomType)
                            .FirstOrDefaultAsync(r => r.Id == id);
   }
}