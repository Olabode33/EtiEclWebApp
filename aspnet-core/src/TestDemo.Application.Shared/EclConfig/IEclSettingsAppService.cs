using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclConfig.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclConfig
{
    public interface IEclSettingsAppService : IApplicationService 
    {
        Task<EclSettingsEditDto> GetAllSettings();
        Task UpdateAllSettings(EclSettingsEditDto input);
    }
}