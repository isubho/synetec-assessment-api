using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _dbContext.Employees
               .Include(e => e.Department)
               .FirstOrDefaultAsync(item => item.Id == id);
        }

        public int GetEmployeesTotalSalary()
        {
            return (int)_dbContext.Employees.Sum(item => item.Salary);
        }
    }
}
