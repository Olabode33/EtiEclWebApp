using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.EclLibrary.Workers.Trackers;
using TestDemo.Investment;
using TestDemo.InvestmentComputation;
using TestDemo.OBE;
using TestDemo.ObeResults;
using TestDemo.Retail;
using TestDemo.RetailResults;
using TestDemo.Wholesale;
using TestDemo.WholesaleResults;

namespace TestDemo.EclLibrary.Jobs
{
    public class UpdateFacilityStageTrackerJob : BackgroundJob<UpdateFacilityStageTrackerJobArgs>, ITransientDependency
    {
        private readonly IRepository<TrackFacilityStage> _facilityTrackerRepository;
        private readonly IRepository<RetailEclFramworkReportDetail, Guid> _retailResultRepository;
        private readonly IRepository<ObeEclFramworkReportDetail, Guid> _obeResultRepository;
        private readonly IRepository<InvestmentEclFinalResult, Guid> _investmentResultRepository;
        private readonly IRepository<InvestmentEclFinalPostOverrideResult, Guid> _investmentOverrideResultRepository;
        private readonly IRepository<WholesaleEclFramworkReportDetail, Guid> _wholesaleResultRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<InvestmentEcl, Guid> _investmentEclRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;

        public UpdateFacilityStageTrackerJob(
            IRepository<TrackFacilityStage> facilityTrackerRepository,
            IRepository<RetailEclFramworkReportDetail, Guid> retailResultRepository, 
            IRepository<ObeEclFramworkReportDetail, Guid> obeResultRepository, 
            IRepository<InvestmentEclFinalResult, Guid> investmentResultRepository,
            IRepository<InvestmentEclFinalPostOverrideResult, Guid> investmentOverrideResultRepository,
            IRepository<WholesaleEclFramworkReportDetail, Guid> wholesaleResultRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<RetailEcl, Guid> retailEclRepository,
            IRepository<WholesaleEcl, Guid> wholesaleRepository,
            IRepository<InvestmentEcl, Guid> investmentEclRepository,
            IRepository<OrganizationUnit, long> ouRepository)
        {
            _facilityTrackerRepository = facilityTrackerRepository;
            _retailResultRepository = retailResultRepository;
            _obeResultRepository = obeResultRepository;
            _investmentResultRepository = investmentResultRepository;
            _investmentOverrideResultRepository = investmentOverrideResultRepository;
            _wholesaleResultRepository = wholesaleResultRepository;
            _wholesaleEclRepository = wholesaleRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _investmentEclRepository = investmentEclRepository;
            _ouRepository = ouRepository;
        }

        [UnitOfWork]
        public override void Execute(UpdateFacilityStageTrackerJobArgs args)
        {
            List<UpdatedFacilityStageTrackerDto> results = new List<UpdatedFacilityStageTrackerDto>();
            switch (args.EclType)
            {
                case EclShared.FrameworkEnum.Wholesale:
                    results = GetWholesaleResult(args);
                    break;
                case EclShared.FrameworkEnum.Retail:
                    results = GetRetailResult(args);
                    break;
                case EclShared.FrameworkEnum.OBE:
                    results = GetObeResult(args);
                    break;
                case EclShared.FrameworkEnum.Investments:
                    results = GetInvestmentResult(args);
                    break;
                default:
                    break;
            }

            if (results.Count > 0)
            {
                UpdateFacilityTracker(args, results);
            }
        }

        public void UpdateFacilityTracker(UpdateFacilityStageTrackerJobArgs args, List<UpdatedFacilityStageTrackerDto> results)
        {
            foreach (var item in results)
            {
                var facilty = _facilityTrackerRepository.FirstOrDefault(e => e.Facility == item.Facility && e.OrganizationUnitId == args.OrganizationUnitId && e.Framework == args.EclType);
                if (facilty == null)
                {
                    _facilityTrackerRepository.Insert(new TrackFacilityStage
                    {
                        Facility = item.Facility,
                        Stage = item.Stage,
                        Framework = args.EclType,
                        LastReportingDate = item.ReportDate,
                        OrganizationUnitId = args.OrganizationUnitId,
                    });
                }
                else
                {
                    if (facilty.Stage != item.Stage)
                    {
                        facilty.Stage = item.Stage;
                        facilty.LastReportingDate = item.ReportDate;
                        _facilityTrackerRepository.Update(facilty);
                    }
                }
            }
        }

        private List<UpdatedFacilityStageTrackerDto> GetWholesaleResult(UpdateFacilityStageTrackerJobArgs args)
        {
            var ecl = _wholesaleEclRepository.FirstOrDefault(args.EclId);
            var result = _wholesaleResultRepository.GetAll()
                                             .Where(e => e.WholesaleEclId == args.EclId)
                                             .Select(e => new UpdatedFacilityStageTrackerDto
                                             {
                                                 Facility = e.ContractNo,
                                                 Stage = e.Stage == e.Overrides_Stage ? e.Stage : e.Overrides_Stage,
                                                 ReportDate = ecl.ReportingDate
                                             }).ToList();

            return result;
        }

        private List<UpdatedFacilityStageTrackerDto> GetRetailResult(UpdateFacilityStageTrackerJobArgs args)
        {
            var ecl = _retailEclRepository.FirstOrDefault(args.EclId);
            var result = _retailResultRepository.GetAll()
                                             .Where(e => e.RetailEclId == args.EclId)
                                             .Select(e => new UpdatedFacilityStageTrackerDto
                                             {
                                                 Facility = e.ContractNo,
                                                 Stage = e.Stage == e.Overrides_Stage ? e.Stage : e.Overrides_Stage,
                                                 ReportDate = ecl.ReportingDate
                                             }).ToList();

            return result;
        }

        private List<UpdatedFacilityStageTrackerDto> GetObeResult(UpdateFacilityStageTrackerJobArgs args)
        {
            var ecl = _obeEclRepository.FirstOrDefault(args.EclId);
            var result = _obeResultRepository.GetAll()
                                             .Where(e => e.ObeEclId == args.EclId)
                                             .Select(e => new UpdatedFacilityStageTrackerDto
                                             {
                                                 Facility = e.ContractNo,
                                                 Stage = e.Stage == e.Overrides_Stage ? e.Stage : e.Overrides_Stage,
                                                 ReportDate = ecl.ReportingDate
                                             }).ToList();

            return result;
        }

        private List<UpdatedFacilityStageTrackerDto> GetInvestmentResult(UpdateFacilityStageTrackerJobArgs args)
        {
            var ecl = _investmentEclRepository.FirstOrDefault(args.EclId);
            var filter = _investmentResultRepository.GetAll().Where(e => e.EclId == args.EclId);

            var result = from pre in filter
                         join post in _investmentOverrideResultRepository.GetAll() on pre.RecordId equals post.RecordId into post1
                         from post2 in post1.DefaultIfEmpty()
                         select new UpdatedFacilityStageTrackerDto
                         {
                             Facility = pre.AssetDescription,
                             Stage = post2 == null ? pre.Stage : post2.Stage,
                             ReportDate = ecl.ReportingDate
                         };

            return result.ToList();
        }

    }


}
