using Microsoft.AspNetCore.Mvc;
using Bookify.Repository.IRepository;
using Bookify.DTOs.Medias;
using Bookify.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.IO;
using System.Threading.Tasks;

namespace Bookify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //added
    public class MediaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MediaController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // POST api/Media
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] MediaCreateDTO dto) 
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Invalid file.");

            using var ms = new MemoryStream();
            await dto.File.CopyToAsync(ms);

            var media = _mapper.Map<Media>(dto); 
            media.Data = ms.ToArray(); 
            media.FileName = dto.File.FileName; 
            media.ContentType = dto.File.ContentType; 

            await _unitOfWork.Medias.AddAsync(media);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Media uploaded successfully", mediaId = media.Id });
        }

        // GET api/Media/{id}/file  (download)
        [HttpGet("{id}/file")]
        [AllowAnonymous] //optional
        public async Task<IActionResult> Download(int id) 
        {
            var media = await _unitOfWork.Medias.GetByIdAsync(id);
            if (media == null)
                return NotFound();

            return File(media.Data, media.ContentType, media.FileName);
        }
    }
}