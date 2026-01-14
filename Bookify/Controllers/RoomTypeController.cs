using Bookify.Models;
using Bookify.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Bookify.DTOs.RoomTypes;
using AutoMapper;

namespace Bookify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoomTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomTypeController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
        }

        // GET: api/RoomType
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomTypes = await _unitOfWork.RoomTypes.GetAllAsync();

            // map using AutoMapper
            var result = _mapper.Map<IEnumerable<RoomTypeReadDTO>>(roomTypes); 

            return Ok(result);
        }

        // GET: api/RoomType/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var roomType = await _unitOfWork.RoomTypes.GetByIdAsync(id);
            if (roomType == null)
                return NotFound();

            // map to DTO using AutoMapper
            var dto = _mapper.Map<RoomTypeReadDTO>(roomType); 

            return Ok(dto);
        }

        // POST: api/RoomType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomTypeCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // map DTO → Entity using AutoMapper
            var roomType = _mapper.Map<RoomType>(dto); 

            await _unitOfWork.RoomTypes.AddAsync(roomType);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Room type created successfully", id = roomType.Id });
        }

        // PUT: api/RoomType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoomTypeCreateDTO dto) //use create-dto shape for update
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _unitOfWork.RoomTypes.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // map DTO onto existing entity (preserve Id)
            _mapper.Map(dto, existing); 

            await _unitOfWork.RoomTypes.UpdateAsync(existing);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Room type updated successfully" });
        }

        // DELETE: api/RoomType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.RoomTypes.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            await _unitOfWork.RoomTypes.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Room type deleted successfully" });
        }
    }
}