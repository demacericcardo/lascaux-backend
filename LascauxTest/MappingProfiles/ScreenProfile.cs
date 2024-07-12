using AutoMapper;
using BL.Dtos;
using DAL.Models;

namespace LascauxTest.MappingProfiles
{
    public class ScreenProfile: Profile
    {
        public ScreenProfile()
        {
            CreateMap<ScreenInputDto, Screen>();
            CreateMap<Screen, ScreenOutputDto>();
            CreateMap<Screen, ScreenOutputNoRefDto>();
        }
    }
}
