using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Authorization.Permissions.Dto;

namespace TestDemo.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
