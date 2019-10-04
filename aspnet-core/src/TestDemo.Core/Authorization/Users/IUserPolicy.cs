using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace TestDemo.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
