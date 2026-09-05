using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Mappers
{
    public class ReviewProfile : Profile
    {
        CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<CreateReviewDto, Review>();
    }
}
