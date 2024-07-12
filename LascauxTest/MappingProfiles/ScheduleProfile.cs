using AutoMapper;
using BL.Dtos;
using DAL.Models;

namespace LascauxTest.MappingProfiles
{
    public class ScheduleProfile: Profile
    {
        public ScheduleProfile()
        {
            CreateMap<ScheduleInputDto, Schedule>();
            CreateMap<Schedule, ScheduleOutputDto>();
        }
    }
}
