using Bookify.Models;

namespace Bookify.Repository.IRepository
{
    public interface IPaymentRepository: IRepository<Payment, int>
    {
        Task<Payment?> GetPaymentWithReservationAsync(int id);
    }
}
