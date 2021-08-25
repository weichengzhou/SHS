using AutoMapper;

using SHS.Models.ViewModels;
using SHS.Models.Dtos;
using SHS.Models.Entities;


namespace SHS.Services.MapperProfile
{
    public class ShsProfile : Profile
    {
        public ShsProfile()
        {
            this.CreateMap<Agent, AgentDto>()
                .ForMember(
                    dest => dest.Dob,
                    src => src.MapFrom(s => s.Dob.ToString("yyyy-MM-dd"))
                ).ReverseMap();
            this.CreateMap<AgentDto, AgentViewModel>()
                .ReverseMap();
            this.CreateMap<ImportFileDto, ImportFileViewModel>()
                .ReverseMap();
        }
    }
}