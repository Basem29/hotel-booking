using Bookify.DTOs.Reservations;
using Bookify.Models;

namespace Bookify.Services.Reservations
{
    public interface IReservationService
    {
        Task<ReservationReadDTO> CreateAsync(string userId, ReservationCreateDTO dto);
        Task<List<ReservationReadDTO>> GetUserReservationsAsync(string userId);
        Task<List<ReservationReadDTO>> GetAllAsync(); 
        Task<bool> UpdateStatusAsync(int id, ReservationStatus status);
    }
}