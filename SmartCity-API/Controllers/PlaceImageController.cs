using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartCity_API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing images associated with places.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceImageController : ControllerBase
    {

        private readonly IPlaceImageRepository _placeImageRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceImageController"/> class.
        /// </summary>
        /// <param name="placeImageRepository">
        /// Repository used to access and manage place images.
        /// </param>
        /// <param name="mapper">
        /// AutoMapper instance used to map entities to DTOs.
        /// </param>
        public PlaceImageController(IPlaceImageRepository placeImageRepository, IMapper mapper)
        {
            _placeImageRepository = placeImageRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all images associated with a specific place.
        /// </summary>
        /// <param name="placeId">The unique identifier of the place.</param>
        /// <returns>
        /// A collection of <see cref="PlaceImageDto"/> objects associated with the place.
        /// </returns>
        /// <response code="200">
        /// The images were successfully retrieved.
        /// </response>
        [HttpGet("place/{placeId}")]
        public async Task<ActionResult<IEnumerable<PlaceImageDto>>> GetByPlaceId(int placeId)
        {
            var images = await _placeImageRepository.GetByPlaceIdAsync(placeId);
            return Ok(_mapper.Map<IEnumerable<PlaceImageDto>>(images));
        }
        /// Creates a new image for a place.
        /// </summary>
        /// <param name="createDto">
        /// Data containing the information required to create the place image.
        /// </param>
        /// <returns>
        /// The newly created <see cref="PlaceImageDto"/>.
        /// </returns>
        /// <response code="201">
        /// The image was successfully created.
        /// </response>
        /// <response code="401">
        /// The user is not authenticated.
        /// </response>
        /// <response code="403">
        /// The authenticated user does not have the Admin role.
        /// </response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PlaceImageDto>> Create([FromBody] CreatePlaceImageDto createDto)
        {
            var image = _mapper.Map<PlaceImage>(createDto);
            var created = await _placeImageRepository.AddAsync(image);

            var dto = _mapper.Map<PlaceImageDto>(created);
            return CreatedAtAction(nameof(GetByPlaceId), new { placeId = created.PlaceId }, dto);
        }
        /// <summary>
        /// Deletes an existing place image.
        /// </summary>
        /// <param name="id">The unique identifier of the image to delete.</param>
        /// <returns>
        /// No content if the image was successfully deleted.
        /// </returns>
        /// <response code="204">
        /// The image was successfully deleted.
        /// </response>
        /// <response code="401">
        /// The user is not authenticated.
        /// </response>
        /// <response code="403">
        /// The authenticated user does not have the Admin role.
        /// </response>
        /// <response code="404">
        /// The image with the specified ID was not found.
        /// </response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _placeImageRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Image with id {id} was not found.");

            await _placeImageRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
