using Bookify.Models;
using Bookify.Repository;
using Bookify.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;


    public IRoomTypeRepository RoomTypes { get; }

    public IRoomRepository Rooms { get; }

    public IReservationRepository Reservations { get; }

    public IMediaRepository Medias { get; }

    public IPaymentRepository Payments { get; }

    public IApplicationUserRepository Users { get; }
    public UnitOfWork(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        RoomTypes = new RoomTypeRepository(_context);
        Rooms = new RoomRepository(_context);
        Reservations = new ReservationRepository(_context);
        Medias = new MediaRepository(_context);
        Payments = new PaymentRepository(_context);
        Users = new ApplicationUserRepository(_userManager);
    }
    public async Task SaveAsync()
    {
       await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
