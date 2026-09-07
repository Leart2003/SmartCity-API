using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Mappers
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, FavoriteDto>()
               .ForMember(dest => dest.PlaceName, opt => opt.MapFrom(src => src.Place.Name))
               .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src =>
                   src.Place.Images.Any(i => i.IsCoverImage)
                       ? src.Place.Images.First(i => i.IsCoverImage).ImageUrl
                       : src.Place.Images.FirstOrDefault() != null ? src.Place.Images.First().ImageUrl : null));
        }
    }
}
