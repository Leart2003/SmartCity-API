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
    /// <summary>
    /// Provides endpoints for managing the authenticated user's favorite places.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteController"/> class.
        /// </summary>
        /// <param name="favoriteRepository">
        /// Repository used to access and manage favorite places.
        /// </param>
        /// <param name="mapper">
        /// AutoMapper instance used to map entities to DTOs.
        /// </param>
        public FavoriteController(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all favorite places belonging to the currently authenticated user.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="FavoriteDto"/> objects representing the user's favorites.
        /// </returns>
        /// <response code="200">
        /// The user's favorite places were successfully retrieved.
        /// </response>
        /// <response code="401">
        /// The user is not authenticated or their user ID could not be determined.
        /// </response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteDto>>> GetMyFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var favorites = await _favoriteRepository.GetByUserIdAsync(userId);
            return Ok(_mapper.Map<IEnumerable<FavoriteDto>>(favorites));
        }
        /// <summary>
        /// Adds a place to the currently authenticated user's favorites.
        /// </summary>
        /// <param name="placeId">The unique identifier of the place to add.</param>
        /// <returns>
        /// The newly created <see cref="FavoriteDto"/>.
        /// </returns>
        /// <response code="200">
        /// The place was successfully added to the user's favorites.
        /// </response>
        /// <response code="400">
        /// The place is already in the user's favorites.
        /// </response>
        /// <response code="401">
        /// The user is not authenticated or their user ID could not be determined.
        /// </response>
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
        }    /// <summary>
             /// Removes a place from the currently authenticated user's favorites.
             /// </summary>
             /// <param name="placeId">The unique identifier of the place to remove.</param>
             /// <returns>
             /// No content if the place was successfully removed.
             /// </returns>
             /// <response code="204">
             /// The place was successfully removed from the user's favorites.
             /// </response>
             /// <response code="401">
             /// The user is not authenticated or their user ID could not be determined.
             /// </response>
             /// <response code="404">
             /// The place does not exist in the user's favorites.
             /// </response>

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
