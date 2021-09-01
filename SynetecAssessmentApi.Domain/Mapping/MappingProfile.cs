using AutoMapper;
using SynetecAssessmentApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SynetecAssessmentApi.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForPath(dest => dest.Department.Title, opt => opt.MapFrom(src => src.Department.Title))
                .ForPath(dest => dest.Department.Description, opt => opt.MapFrom(src => src.Department.Description))
                .ReverseMap();
        }
    }
}
