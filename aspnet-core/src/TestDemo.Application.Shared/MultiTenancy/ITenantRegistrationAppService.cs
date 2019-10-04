using System.Threading.Tasks;
using Abp.Application.Services;
using TestDemo.Editions.Dto;
using TestDemo.MultiTenancy.Dto;

namespace TestDemo.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}