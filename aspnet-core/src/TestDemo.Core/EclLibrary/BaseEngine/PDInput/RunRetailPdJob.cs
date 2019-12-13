using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using EclEngine.BaseEclEngine.PdInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.PDInput
{
    public class RunRetailPdJob : BackgroundJob<RetailPdJobArgs>, ITransientDependency
    {
        PdInputResult _pdInputResult;

        public RunRetailPdJob()
        {
            _pdInputResult = new PdInputResult();
        }

        [UnitOfWork]
        public override void Execute(RetailPdJobArgs args)
        {
            _pdInputResult.RunSicrInputs();
            _pdInputResult.RunLifetimePdBest();
            _pdInputResult.RunLifetimePdOptimistic();
            _pdInputResult.RunLifetimePdDownturn();
            _pdInputResult.RunRedefaultLifetimePdBest();
            _pdInputResult.RunRedefaultLifetimePdOptimistic();
            _pdInputResult.RunRedefaultLifetimePdDownturn();
        }
    }
}
