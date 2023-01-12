using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile // using AutoMapper;
    {
        public AutoMapperProfiles()
        {
            // From - To
            CreateMap<AppUser, MemberDto>()
            // We specify where it will find the Url from, if true then the Photos url
                .ForMember(destination => destination.PhotoUrl,
                            options => options.MapFrom(
                            source => source.Photos.FirstOrDefault
                            (x => x.IsMain).Url))
                .ForMember(destination => destination.Age,
                            options => options.MapFrom(
                            source => source.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
        }
    }
}