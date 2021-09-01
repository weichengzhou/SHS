using System;
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
                    opt => opt.MapFrom(src =>
                        src.Dob.HasValue ?
                        ((DateTime)src.Dob).ToString("yyyy-MM-dd") :
                        null))
                .ReverseMap();
            this.CreateMap<AgentDto, AgentViewModel>()
                .ReverseMap();
            this.CreateMap<ExcelFileDto, ExcelFileViewModel>()
                .ReverseMap();
        }
    }
}