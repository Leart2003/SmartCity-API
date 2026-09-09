using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartCity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceImageController : ControllerBase
    {

        private readonly IPlaceImageRepository _placeImageRepository;
        private readonly IMapper _mapper;

        public PlaceImageController(IPlaceImageRepository placeImageRepository, IMapper mapper)
        {
            _placeImageRepository = placeImageRepository;
            _mapper = mapper;
        }
        [HttpGet("place/{placeId}")]
        public async Task<ActionResult<IEnumerable<PlaceImageDto>>> GetByPlaceId(int placeId)
        {
            var images = await _placeImageRepository.GetByPlaceIdAsync(placeId);
            return Ok(_mapper.Map<IEnumerable<PlaceImageDto>>(images));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PlaceImageDto>> Create([FromBody] CreatePlaceImageDto createDto)
        {
            var image = _mapper.Map<PlaceImage>(createDto);
            var created = await _placeImageRepository.AddAsync(image);

            var dto = _mapper.Map<PlaceImageDto>(created);
            return CreatedAtAction(nameof(GetByPlaceId), new { placeId = created.PlaceId }, dto);
        }
    }
}
