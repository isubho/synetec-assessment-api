
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.Dtos;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Interfaces
{
    public interface IBonusPoolService
    {
        public Response<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, Employee employee);
    }
}
