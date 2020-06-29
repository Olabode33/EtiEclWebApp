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
        Task RunInvestmentPostOverrideEclStoredProcedure(Guid eclId);
        Task RunInvestmentCloseEclStoredProcedure(Guid eclId);
        Task RunInvestmentReopenEclStoredProcedure(Guid eclId);
        Task RunWholesaleCloseEclStoredProcedure(Guid eclId);
        Task RunWholesaleReopenEclStoredProcedure(Guid eclId);
        Task RunRetailCloseEclStoredProcedure(Guid eclId);
        Task RunRetailReopenEclStoredProcedure(Guid eclId);
        Task RunObeCloseEclStoredProcedure(Guid eclId);
        Task RunObeReopenEclStoredProcedure(Guid eclId);
        Task DeleteExistingInputRecords(string tableName, string columnName, string value);
    }
}
