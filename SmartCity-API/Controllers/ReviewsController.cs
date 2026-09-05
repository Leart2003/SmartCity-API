using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost]
        [Authorize] // çdo user i kyçur (Tourist ose Admin) mund të bëjë review
        public async Task<ActionResult<ReviewDto>> Create([FromBody] CreateReviewDto createDto)
        {
            if (createDto.Rating < 1 || createDto.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var review = _mapper.Map<Review>(createDto);
            review.UserId = userId;

            var created = await _reviewRepository.AddAsync(review);
            var dto = _mapper.Map<ReviewDto>(created);

            return CreatedAtAction(nameof(GetByPlaceId), new { placeId = created.PlaceId }, dto);
        }
    }
}
