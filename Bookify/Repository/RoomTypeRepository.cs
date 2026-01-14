using Bookify.Models;
using Bookify.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class RoomTypeRepository : Repository<RoomType, int>, IRoomTypeRepository
{
    public RoomTypeRepository(AppDbContext context) : base(context) { }

   public async Task<RoomType?> GetRoomTypeWithMediaAsync(int id)
   {
       return await dbSet.Include(rt => rt.Medias)
                        .FirstOrDefaultAsync(rt => rt.Id == id);
   }
}