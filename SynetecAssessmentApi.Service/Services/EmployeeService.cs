using AutoMapper;
using SynetecAssessmentApi.Domain.Dtos;
using SynetecAssessmentApi.Interfaces;
using SynetecAssessmentApi.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SynetecAssessmentApi.Domain;

namespace SynetecAssessmentApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        public async Task<Response<Employee>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return Response<Employee>.Fail($"Employee with id={id} doesn't exist");
                }
                return Response<Employee>.Ok(employee);
            }
            catch (Exception e)
            {
                return Response<Employee>.Fail(e.Message); ;
            }
        }
        public async Task<Response<List<EmployeeDto>>> GetEmployeesAsync()
        {
            try
            {
                var employees = await employeeRepository.GetAll();

                List<EmployeeDto> result = new();
                result.AddRange(employees.Select(employee => mapper.Map<EmployeeDto>(employee)));
                return Response<List<EmployeeDto>>.Ok(result);
            }
            catch (Exception e)
            {
                return Response<List<EmployeeDto>>.Fail(e.Message);
            }
        }

        public int GetEmployeesTotalSalary()
        {
            throw new NotImplementedException();
        }
    }
}
