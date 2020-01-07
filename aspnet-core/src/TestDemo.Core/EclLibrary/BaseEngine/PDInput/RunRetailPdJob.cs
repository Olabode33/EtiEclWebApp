using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using EclEngine.BaseEclEngine.PdInput;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;
using TestDemo.Retail;

namespace TestDemo.EclLibrary.BaseEngine.PDInput
{
    public class RunRetailPdJob : BackgroundJob<RetailPdJobArgs>, ITransientDependency
    {
        PdInputResult _pdInputResult;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;

        public RunRetailPdJob(
            IRepository<RetailEcl, Guid> retailEclRepository
            )
        {
            _pdInputResult = new PdInputResult();
            _retailEclRepository = retailEclRepository;
        }

        [UnitOfWork]
        public override void Execute(RetailPdJobArgs args)
        {
            UpdateEclStatus(args.RetailEclId, EclStatusEnum.Running);
            _pdInputResult.RunSicrInputs();
            _pdInputResult.RunLifetimePdBest();
            _pdInputResult.RunLifetimePdOptimistic();
            _pdInputResult.RunLifetimePdDownturn();
            _pdInputResult.RunRedefaultLifetimePdBest();
            _pdInputResult.RunRedefaultLifetimePdOptimistic();
            _pdInputResult.RunRedefaultLifetimePdDownturn();
            UpdateEclStatus(args.RetailEclId, EclStatusEnum.Completed);
        }

        private void UpdateEclStatus(Guid eclId, EclStatusEnum status)
        {
            var ecl = _retailEclRepository.FirstOrDefault(eclId);
            ecl.Status = status;
            _retailEclRepository.Update(ecl);
        }
    }
}
