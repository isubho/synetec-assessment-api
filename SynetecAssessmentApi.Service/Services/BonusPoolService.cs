
using AutoMapper;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.Dtos;
using SynetecAssessmentApi.Interfaces;
using SynetecAssessmentApi.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        public BonusPoolService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public Response<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, Employee employee)
        {
            try
            {
                if (bonusPoolAmount <= 0)
                {
                    return Response<BonusPoolCalculatorResultDto>.Fail("Bonus Pool amount can't be negative or zero");
                }
                if(employee.Salary <= 0)
                {
                    return Response<BonusPoolCalculatorResultDto>.Fail("Employee salary can't be negative or zero");
                }
                int totalSalary = employeeRepository.GetEmployeesTotalSalary();
                if (totalSalary <= 0)
                {
                    return Response<BonusPoolCalculatorResultDto>.Fail("Total Employees Salary can't be negative or zero");
                }
                if (employee.Salary > totalSalary)
                {
                    return Response<BonusPoolCalculatorResultDto>.Fail("Employee salary can't be more than total employees salary");
                }
                decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
                decimal bonusAllocation = bonusPercentage * (decimal)bonusPoolAmount;

                var Employee = mapper.Map<EmployeeDto>(employee);
                var bonus = new BonusPoolCalculatorResultDto
                {
                    Employee = Employee,
                    Amount = bonusAllocation
                };
                return Response<BonusPoolCalculatorResultDto>.Ok(bonus);
                              
            }
            catch (Exception e)
            {
                return Response<BonusPoolCalculatorResultDto>.Fail(e.Message);
            }
        }
    }
}
