using AutoMapper;
using Domain.Entities;
using Domain.Dtos;

namespace SmartCity_API.MappingProfiles
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, PlaceDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.Images.Any(i => i.IsCoverImage) ?
                src.Images.First(i => i.IsCoverImage).ImageUrl : src.Images.FirstOrDefault() != null ? src.Images.First().ImageUrl:null ))
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
                .ForMember(dest => dest.DistanceKm, opt => opt.Ignore());
            CreateMap<CreatePlaceDto, Place>();
        }
     
    }
}