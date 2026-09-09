using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Mappers
{
    public class PlaceImageProfile : Profile
    {
        public PlaceImageProfile()
        {
            CreateMap<PlaceImage, PlaceImageDto>();
            CreateMap<CreatePlaceImageDto, PlaceImage>();
        }

    }
}
