using AutoMapper;
using Domain.Dtos;
using Domain.Interface;
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
    }
}
