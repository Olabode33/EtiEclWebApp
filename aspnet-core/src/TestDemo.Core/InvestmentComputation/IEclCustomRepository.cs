using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Investment;

namespace TestDemo.InvestmentComputation
{
    public interface IEclCustomRepository: IRepository<InvestmentEcl, Guid>
    {
        Task RunInvestmentPreOverrideEclStoredProcedure(Guid eclId);
    }
}
