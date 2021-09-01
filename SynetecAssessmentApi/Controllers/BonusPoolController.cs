using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.Dtos;
using SynetecAssessmentApi.Interfaces;
using SynetecAssessmentApi.Services;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : ControllerBase
    {
        private readonly IBonusPoolService bonusPoolService;
        private readonly IEmployeeService employeeService;
        public BonusPoolController(IBonusPoolService bonusPoolService, IEmployeeService employeeService)
        {
            this.bonusPoolService = bonusPoolService;
            this.employeeService = employeeService;
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            if (request.SelectedEmployeeId == 0)
            {
                return BadRequest("Employee id missing");
            }

            Response<Employee> employee = await employeeService.GetEmployeeById(request.SelectedEmployeeId);

            if (employee.Value == null)
            {
                return BadRequest($"Employee with id={request.SelectedEmployeeId} doesn't exist");
            }

            var result = bonusPoolService.CalculateAsync(request.TotalBonusPoolAmount, employee.Value);

            if (!result.IsSuccees)
            {
                return BadRequest("Calculate Bonus:" + result.ErrorMessage);
            }
            return Ok(result.Value);
        }
    }
}
