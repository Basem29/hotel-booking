using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookify.DTOs.Reservations;
using Bookify.Models;

namespace Bookify.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReservationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservationReadDTO?> CreateAsync(string userId, ReservationCreateDTO dto)
        {
            if (string.IsNullOrEmpty(userId)) return null; 
            var room = await _context.Rooms.Include(r => r.RoomType).FirstOrDefaultAsync(r => r.Id == dto.RoomId);
            if (room == null) return null;

            var days = (dto.CheckOut - dto.CheckIn).Days;
            if (days <= 0) return null;

            var total = room.RoomType?.Price ?? 0m * days;

            var reservation = _mapper.Map<Reservation>(dto);
            reservation.UserId = userId;
            reservation.TotalPrice = total;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // reload with includes for mapping
            var saved = await _context.Reservations
                .Include(r => r.Room).ThenInclude(rt => rt.RoomType)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservation.Id);

            return saved == null ? null : _mapper.Map<ReservationReadDTO>(saved);
        }

        public async Task<List<ReservationReadDTO>> GetUserReservationsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new List<ReservationReadDTO>(); 

            var reservations = await _context.Reservations
                .Include(r => r.Room).ThenInclude(rt => rt.RoomType)
                .Include(r => r.User)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<ReservationReadDTO>>(reservations);
        }

        public async Task<List<ReservationReadDTO>> GetAllAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room).ThenInclude(rt => rt.RoomType)
                .Include(r => r.User)
                .ToListAsync();

            return _mapper.Map<List<ReservationReadDTO>>(reservations);
        }

        public async Task<bool> UpdateStatusAsync(int id, ReservationStatus status)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            reservation.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}