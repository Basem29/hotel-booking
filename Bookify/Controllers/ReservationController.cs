<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Bookify.DTOs.Reservations;
using Bookify.Services.Reservations;
using Bookify.Models;

namespace Bookify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize] // user must be logged in
        public async Task<IActionResult> Create([FromBody] ReservationCreateDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.CreateAsync(userId, dto);
            if (result == null) return BadRequest("Invalid reservation data.");
            return Ok(result);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservations = await _service.GetUserReservationsAsync(userId);
            return Ok(reservations);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ReservationStatus status)
        {
            var success = await _service.UpdateStatusAsync(id, status);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
=======
﻿using Bookify.DTOs.Reservation;
using Bookify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _unitOfWork.Reservations.GetAllAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById([FromRoute] int id)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] DateTime checkin, [FromQuery] DateTime checkout)
        {

            var availableRooms = await _unitOfWork.Reservations.GetAvailableRoomsAsync(checkin, checkout);

            if (!availableRooms.Any())
                return NotFound("No rooms available for the selected dates.");

            return Ok(availableRooms);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isAvailable = await _unitOfWork.Reservations.IsRoomAvailableAsync(dto.RoomId, dto.CheckIn, dto.CheckOut);
            if (!isAvailable)
                return BadRequest("The selected room is not available for the chosen dates.");
            var reservation = new Reservation
            {
                RoomId = dto.RoomId,
                UserId = dto.UserId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                Status = ReservationStatus.Confirmed
            };
            await _unitOfWork.Reservations.AddAsync(reservation);
            await _unitOfWork.SaveAsync();
            return Ok(new { message = "Reservation created successfully", reservation });
        }

    }
}
>>>>>>> 2511bd70c61aefe14b5f6503f0a5bdd8b1ad672c
