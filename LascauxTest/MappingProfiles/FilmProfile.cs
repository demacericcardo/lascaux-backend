using AutoMapper;
using BL.Dtos;
using DAL.Models;

namespace LascauxTest.MappingProfiles
{
    public class FilmProfile: Profile
    {
        public FilmProfile()
        {
            CreateMap<FilmInputDto, Film>();
            CreateMap<Film, FilmOutputDto>();
        }
    }
}
