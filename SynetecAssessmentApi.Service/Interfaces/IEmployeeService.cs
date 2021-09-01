
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Interfaces
{
    public interface IEmployeeService
    {
        public Task<Response<List<EmployeeDto>>> GetEmployeesAsync();
        public Task<Response<Employee>> GetEmployeeById(int id);
        public int GetEmployeesTotalSalary();
    }
}
