using AutoMapper;
using Domain.Dtos;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartCity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet("place/{placeId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByPlaceId(int placeId)
        {
            var reviews = await _reviewRepository.GetByPlaceIdAsync(placeId);
            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));
        }

    }
}
