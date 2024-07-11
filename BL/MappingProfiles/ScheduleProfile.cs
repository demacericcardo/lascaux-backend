using AutoMapper;
using BL.Dtos;
using DAL.Models;

namespace BL.MappingProfiles
{
    public class ScheduleProfile: Profile
    {
        public ScheduleProfile()
        {
            CreateMap<ScheduleInputDto, Schedule>();
        }
    }
}
