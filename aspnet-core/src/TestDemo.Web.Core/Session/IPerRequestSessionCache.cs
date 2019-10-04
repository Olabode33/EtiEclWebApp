using System.Threading.Tasks;
using TestDemo.Sessions.Dto;

namespace TestDemo.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
