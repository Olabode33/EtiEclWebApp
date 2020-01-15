using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.EclLibrary.Investment.Dtos;
using TestDemo.EclLibrary.Investment.Utils;

namespace TestDemo.EclLibrary.Investment.Calculations
{
    public class EadInput
    {
        List<InvsecLifetimeEad> invsecLifetimeEad;

        public EadInput()
        {
            this.invsecLifetimeEad = new List<InvsecLifetimeEad>();
        }

        public void ComputeLifetimeEad(Guid eclId)
        {
            InvestmentDbUtil dbUtil = new InvestmentDbUtil();
            var reportingDate = dbUtil.GetInvestmentEclReportDate(eclId);
            var ead = dbUtil.GetInvestmentEclEadAssumption(eclId);
            var assumedLifetime = ead.FirstOrDefault(x => x.Key == "InvSec.AssumedCreditLife");
            var assetbooks = dbUtil.GetInvestmentEclAssetBookData(eclId);
            foreach (var item in assetbooks)
            {
                InvsecLifetimeEad invsec = new InvsecLifetimeEad(item);
                invsecLifetimeEad.Add(invsec);
            }
        }
    }
}
