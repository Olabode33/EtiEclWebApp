using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using TestDemo.Dto;

namespace TestDemo.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
