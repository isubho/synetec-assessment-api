using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService )
        {
            this.employeeService = employeeService;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await employeeService.GetEmployeesAsync();

            if (!result.IsSuccees)
            {
                return BadRequest("Get All Employee Error:" + result.ErrorMessage);
            }
            return Ok(result.Value);
        }
    }
}
