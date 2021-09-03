using System;
using AutoMapper;

using SHS.Models.ViewModels;
using SHS.Models.Dtos;
using SHS.Models.Entities;


namespace SHS.Services.MapperProfile
{
    /// <summary>
    /// Use to convert two objects if they can reflect.
    /// </summary>
    public class ShsProfile : Profile
    {
        /// <summary>
        /// Constructor of ShsProfile, defined the rule of mapping objects.
        /// </summary>
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