using AutoMapper;
using Domain.Dtos;
using Domain.Interface;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartCity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IReviewRepository _reviewRepository;

        private readonly IMapper _mapper;
        public PlacesController(IPlaceRepository placeRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _placeRepository = placeRepository;

            _reviewRepository = reviewRepository;

            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceDto>>> GetAll()
        {
            var places = await _placeRepository.GetPlacesAsync();
            var dtos = new List<PlaceDto>();

            foreach (var place in places)
            {
                var dto = _mapper.Map<PlaceDto>(place);
                dto.AverageRating = Math.Round(await _reviewRepository.GetAverageRatingAsync(place.Id), 1);
                dtos.Add(dto);
            }

            return Ok(dtos);

        }
        [HttpGet("{id}")]

        public async Task<ActionResult<PlaceDto>> GetById(int id)
        {
            var place = await _placeRepository.GetPlaceById(id);

            if (place == null)
            {
                return NotFound($"Place with id {id} was not found.");
            }
            var dto = _mapper.Map<PlaceDto>(place);
            dto.AverageRating =  Math.Round(await _reviewRepository.GetAverageRatingAsync(place.Id), 1);


            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePlaceDto updateDto)
        {
            var existing = await _placeRepository.GetPlaceById(id);

            if (existing is null)
            {
                return NotFound("Place not found");
            };
            _mapper.Map(updateDto, existing);

            await _placeRepository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _placeRepository.GetPlaceById(id);

            if (existing is null)
            {
                return NotFound("Place not found");
            }
            ;
            

            await _placeRepository.DeleteAsync(id);
            return NoContent();
        }

        private static double CalculateDistanceForDisplay(double lat1, double lon1, double lat2, double lon2)
        {
            double latDiffKm = (lat2 - lat1) * 111.0;
            double lonDiffKm = (lon2 - lon1) * 111.0 * Math.Cos(lat1 * Math.PI / 180);
            return Math.Sqrt(latDiffKm * latDiffKm + lonDiffKm * lonDiffKm);
        }



    }
}
