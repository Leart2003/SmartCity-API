using AutoMapper;
using Domain.Dtos;
using Domain.Interface;
using Infrastructure.Repository;
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

     
    }
}
