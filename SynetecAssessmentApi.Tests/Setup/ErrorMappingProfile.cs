using AutoMapper;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Tests.Setup
{
    public class ErrorMappingProfile : Profile
    {
        public ErrorMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
        }
    }
}
