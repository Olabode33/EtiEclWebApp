using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Calibration;
using TestDemo.CalibrationResult;
using TestDemo.EclShared.Dtos;

namespace TestDemo.EclShared.Calculations
{
    public class UpdatePdSnPMappingBestFitJob: BackgroundJob<ImportAssumptionDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IRepository<PdInputAssumptionSnPCummulativeDefaultRate, Guid> _pdInputSnPCummulativeDefaultRateRepository;
        private readonly IRepository<PdInputAssumption, Guid> _pdInputAssumptionRepository;
        private readonly IRepository<CalibrationResultPd12Months> _pd12MonthsResultRepository;
        private readonly IRepository<CalibrationPdCrDr, Guid> _calibrationRepository;

        public UpdatePdSnPMappingBestFitJob(
            IRepository<PdInputAssumptionSnPCummulativeDefaultRate, Guid> pdInputSnPCummulativeDefaultRateRepository, 
            IRepository<PdInputAssumption, Guid> pdInputAssumptionRepository, 
            IRepository<CalibrationResultPd12Months> pd12MonthsResultRepository, 
            IRepository<CalibrationPdCrDr, Guid> calibrationRepository)
        {
            _pdInputSnPCummulativeDefaultRateRepository = pdInputSnPCummulativeDefaultRateRepository;
            _pdInputAssumptionRepository = pdInputAssumptionRepository;
            _pd12MonthsResultRepository = pd12MonthsResultRepository;
            _calibrationRepository = calibrationRepository;
        }

        [UnitOfWork]
        public override void Execute(ImportAssumptionDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault(e => e.OrganizationUnitId == args.AffiliateId && e.Status == CalibrationStatusEnum.AppliedToEcl);
            if (calibration == null)
            {
                Logger.Error(this.GetType().Name + ": No Applied calibration");
                return;
            }

            var pd12Month = _pd12MonthsResultRepository.GetAllList(e => e.CalibrationId == calibration.Id);
            var snp = _pdInputSnPCummulativeDefaultRateRepository.GetAllList(e => e.OrganizationUnitId == args.AffiliateId 
                                                                                && e.Framework == ((args.Framework == FrameworkEnum.Retail || args.Framework == FrameworkEnum.OBE) ? args.Framework :  FrameworkEnum.Wholesale) 
                                                                                && e.Years == 1);
            if (pd12Month.Count > 0 && snp.Count > 0)
            {
                var upperBond = GetUperBond(snp);
                if (upperBond == null)
                {
                    Logger.Error(this.GetType().Name + ": No upper bond!");
                    return;
                }
                var bestFitList = _pdInputAssumptionRepository.GetAllList(e => e.PdGroup == PdInputAssumptionGroupEnum.CreditBestFit
                                                                                && e.Framework == ((args.Framework == FrameworkEnum.Retail || args.Framework == FrameworkEnum.OBE) ? args.Framework : FrameworkEnum.Wholesale)
                                                                                && e.OrganizationUnitId == args.AffiliateId);
                foreach (var credit in pd12Month)
                {
                    var bestFit = bestFitList.FirstOrDefault(e => e.InputName == credit.Rating.ToString());
                    var bond = upperBond.Where(e => e.Month12Pd <= credit.Months_PDs_12)
                                        .Last();
                    if (bond != null && bestFit != null)
                    {
                        bestFit.Value = bond.Rating;
                        bestFit.LastModifierUserId = args.User.UserId;
                        _pdInputAssumptionRepository.Update(bestFit);
                    }
                }

            }
        }

        private List<UpperBond> GetUperBond(List<PdInputAssumptionSnPCummulativeDefaultRate> snp)
        {
            var upperBound = new List<UpperBond>();
            upperBound.Add(new UpperBond    
            {
                Rating = "AAA",
                Month12Pd = 0
            });

            var snpAAA = snp.FirstOrDefault(e => e.Rating == "AAA");
            var snpAA = snp.FirstOrDefault(e => e.Rating == "AA");
            var snpA = snp.FirstOrDefault(e => e.Rating == "A");
            var snpBBB = snp.FirstOrDefault(e => e.Rating == "BBB");
            var snpBB = snp.FirstOrDefault(e => e.Rating == "BB");
            var snpB = snp.FirstOrDefault(e => e.Rating == "B");
            var snpCCC = snp.FirstOrDefault(e => e.Rating == "CCC");

            if (snpAAA == null || snpAA == null || snpA == null || snpBBB == null || snpBB == null || snpB == null || snpCCC == null)
            {
                Logger.Error(this.GetType().Name + ": Snp Ratting is not complete!");
                return null;
            }

            var t = (snpAA.Value ?? 0) * (snpA.Value ?? 0);
            var s = Math.Sqrt(snpAA.Value ?? 0 * snpA.Value ?? 0);
            var h = snpAA.Value + (Math.Sqrt(snpAA.Value ?? 0 * snpA.Value ?? 0));

            var upperBondAA = snpAAA.Value + (Math.Sqrt((snpAAA.Value ?? 0) * (snpAA.Value ?? 0)));
            var upperBondA = snpAA.Value + (Math.Sqrt((snpAA.Value ?? 0) * (snpA.Value ?? 0)));
            var upperBondBBB = snpA.Value + (Math.Sqrt((snpA.Value ?? 0) * (snpBBB.Value ?? 0)));
            var upperBondBB = snpBBB.Value + (Math.Sqrt((snpBBB.Value ?? 0) * (snpBB.Value ?? 0)));
            var upperBondB = snpBB.Value + (Math.Sqrt((snpBB.Value ?? 0) * (snpB.Value ?? 0)));
            var upperBondCCC = snpB.Value + (Math.Sqrt((snpB.Value ?? 0) * (snpCCC.Value ?? 0)));
            upperBound.Add(new UpperBond
            {
                Rating = "AA",
                Month12Pd = upperBondAA ?? 0
            });
            upperBound.Add(new UpperBond
            {
                Rating = "A",
                Month12Pd = upperBondA ?? 0
            });
            upperBound.Add(new UpperBond
            {
                Rating = "BBB",
                Month12Pd = upperBondBBB ?? 0
            });
            upperBound.Add(new UpperBond
            {
                Rating = "BB",
                Month12Pd = upperBondBB ?? 0
            });
            upperBound.Add(new UpperBond
            {
                Rating = "B",
                Month12Pd = upperBondB ?? 0
            });
            upperBound.Add(new UpperBond
            {
                Rating = "CCC",
                Month12Pd = upperBondCCC ?? 0
            });

            return upperBound;
        }
    }

    public class UpperBond
    {
        public string Rating { get; set; }
        public double Month12Pd { get; set; }
    }
}
