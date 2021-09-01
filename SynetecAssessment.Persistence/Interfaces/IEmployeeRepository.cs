using SynetecAssessmentApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAll();
        public Task<Employee> GetEmployeeById(int id);
        public int GetEmployeesTotalSalary();
    }
}
