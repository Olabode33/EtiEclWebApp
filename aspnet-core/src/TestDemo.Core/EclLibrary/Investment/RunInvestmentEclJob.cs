using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.InvestmentComputation;

namespace TestDemo.EclLibrary.Investment
{
    public class RunInvestmentEclJob : BackgroundJob<RunEclJobArgs>, ITransientDependency
    {
        private readonly IEclCustomRepository _investmentEclRepository;

        public RunInvestmentEclJob(IEclCustomRepository investmentEclRepository)
        {
            _investmentEclRepository = investmentEclRepository;
        }

        //[UnitOfWork]
        public override void Execute(RunEclJobArgs args)
        {
            _investmentEclRepository.RunInvestmentPreOverrideEclStoredProcedure(args.EclId);
        }
    }
}
