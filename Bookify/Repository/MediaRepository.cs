using Bookify.Models;
using Bookify.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bookify.Repository
{
    public class MediaRepository : Repository<Media, int>, IMediaRepository
    {
        public MediaRepository(AppDbContext context) : base(context) { }

       public async Task<IEnumerable<Media>> GetMediaByRoomTypeAsync(int roomTypeId)
       {
           return await dbSet.Where(m => m.RoomTypeId == roomTypeId).ToListAsync();
       }
}
}