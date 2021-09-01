using AutoMapper;
using FluentAssertions;
using Moq;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.Dtos;
using SynetecAssessmentApi.Domain.Mapping;
using SynetecAssessmentApi.Persistence.Interfaces;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Tests.Setup;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SynetecAssessmentApi.Tests
{
    public class BonusPoolTests
    {
        private readonly Mock<IEmployeeRepository> employeeRepository = new();
        private readonly Mock<IMapper> objMapper = new();
        private readonly BonusPoolService bonusPoolService;

        public BonusPoolTests()
        {
            bonusPoolService = new BonusPoolService(employeeRepository.Object, objMapper.Object);
        }

        [Theory]
        [InlineData(100, "35.00")]
        [InlineData(int.MaxValue, "751619276.45")]
        [InlineData(1456, "509.60")]
        [InlineData(466, "163.10")]
        [InlineData(20, "7.00")]
        public void TestCorrectBonusResult(int bonusPoolAmount, string expected)
        {
            // Arrange
            Employee employee = new(1, "", "", 525, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(1500);

            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            result.Value.Amount.Equals(350);
            Assert.Equal(result.Value.Amount.ToString("0.00"), expected);
        }

        [Theory]
        [InlineData(0, "Bonus Pool amount can't be negative or zero")]
        [InlineData(-1, "Bonus Pool amount can't be negative or zero")]
        [InlineData(int.MinValue, "Bonus Pool amount can't be negative or zero")]
        public void TestIncorrectBonusResult(int bonusPoolAmount,string expected)
        {
            // Arrange
            Employee employee = new(1, "", "", 500, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(2000);

            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            Assert.Equal(result.ErrorMessage, expected);

        }

        [Theory]
        [InlineData(100, "66.67")]
        [InlineData(1006,"670.67")]
        [InlineData(1500, "1000.00")]
        [InlineData(1, "0.67")]
        public void TestCorrectEmployeeSalaryResult(int employeeSalary, string expected)
        {
            // Arrange
            int bonusPoolAmount = 1000;

            Employee employee = new(1, "", "", employeeSalary, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(1500);

            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            Assert.Equal(result.Value.Amount.ToString("0.00"), expected);
        }

        [Theory]
        [InlineData(0, "Employee salary can't be negative or zero")]
        [InlineData(1501, "Employee salary can't be more than total employees salary")]
        [InlineData(-1, "Employee salary can't be negative or zero")]
        [InlineData(int.MinValue, "Employee salary can't be negative or zero")]
        public void TestIncorrectEmployeeSalaryResult(int employeeSalary, string expected)
        {
            // Arrange
            int bonusPoolAmount = 1000;

            Employee employee = new(1, "", "", employeeSalary, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(1500);

            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            Assert.Equal(result.ErrorMessage, expected);
        }




        [Theory]
        [InlineData(100, "1000.00")]
        [InlineData(1006, "99.40")]
        [InlineData(1500, "66.67")]
        public void TestCorrectTotalEmployeeSalaryResult(int TotalEmployeeSalary, string expected)
        {
            // Arrange
            int bonusPoolAmount = 1000;

            Employee employee = new(1, "", "", 100, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(TotalEmployeeSalary);

            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            Assert.Equal(result.Value.Amount.ToString("0.00"), expected);
        }

        [Theory]
        [InlineData(0, "Total Employees Salary can't be negative or zero")]
        [InlineData(99, "Employee salary can't be more than total employees salary")]
        [InlineData(-1, "Total Employees Salary can't be negative or zero")]
        [InlineData(int.MinValue, "Total Employees Salary can't be negative or zero")]
        public void TestIncorrectTotalEmployeeSalaryResult(int TotalEmployeeSalary, string expected)
        {
            // Arrange
            int bonusPoolAmount = 1000;

            Employee employee = new(1, "", "", 100, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(TotalEmployeeSalary);

            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            Assert.Equal(result.ErrorMessage, expected);
        }

        [Fact]
        public void TestWrongAutoMapperConfigurationTest()
        {
            // Arrange
            int bonusPoolAmount = 1000;

            Employee employee = new(1, "", "", 500, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(2000);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ErrorMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var bonusPoolService = new BonusPoolService(employeeRepository.Object, mapper);
            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            
            Assert.Contains("Error mapping types.", result.ErrorMessage);
        }

        [Fact]
        public void TestCorrectAutoMapperConfigurationTest()
        {
            // Arrange
            int bonusPoolAmount = 1000;

            Employee employee = new(1, "", "", 500, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(2000);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var bonusPoolService = new BonusPoolService(employeeRepository.Object, mapper);
            // Act
            var result = bonusPoolService.CalculateAsync(bonusPoolAmount, employee);

            // Assert
            Assert.True(result.IsSuccees);
            Assert.Equal(250, result.Value.Amount);
            
        }
    }
}
