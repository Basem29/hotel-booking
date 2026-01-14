using Bookify.Repository.IRepository;

public interface IUnitOfWork
{
    IRoomTypeRepository RoomTypes { get; }
    IRoomRepository Rooms { get; }
    IReservationRepository Reservations { get; }
    IMediaRepository Medias { get; }
    IPaymentRepository Payments { get; }


    IApplicationUserRepository Users { get; }  
    Task SaveAsync();

    void Dispose();
}
