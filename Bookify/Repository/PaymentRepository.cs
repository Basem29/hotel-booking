using Bookify.Models;
using Bookify.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bookify.Repository
{
    public class PaymentRepository : Repository<Payment, int>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context) { }

       public async Task<Payment?> GetPaymentWithReservationAsync(int id)
       {
           return await dbSet.Include(p => p.Reservation)
                             .ThenInclude(r => r.Room)
                             .FirstOrDefaultAsync(p => p.Id == id);
       }
}
}