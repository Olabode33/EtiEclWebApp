using System.Threading.Tasks;
using Abp.Application.Services;
using TestDemo.Sessions.Dto;

namespace TestDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
