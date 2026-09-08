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
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteDto>>> GetMyFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var favorites = await _favoriteRepository.GetByUserIdAsync(userId);
            return Ok(_mapper.Map<IEnumerable<FavoriteDto>>(favorites));
        }
        [HttpPost("{placeId}")]
        public async Task<ActionResult<FavoriteDto>> Add(int placeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var alreadyExists = await _favoriteRepository.ExistsAsync(userId, placeId);
            if (alreadyExists)
                return BadRequest("This place is already in your favorites.");

            var favorite = new Favorite
            {
                UserId = userId,
                PlaceId = placeId
            };

            var created = await _favoriteRepository.AddAsync(favorite);
            return Ok(_mapper.Map<FavoriteDto>(created));
        }

        [HttpDelete("{placeId}")]
        public async Task<IActionResult> Remove(int placeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var exists = await _favoriteRepository.ExistsAsync(userId, placeId);
            if (!exists)
                return NotFound("This place is not in your favorites.");

            await _favoriteRepository.DeleteAsync(userId, placeId);
            return NoContent();
        }


    }
}
