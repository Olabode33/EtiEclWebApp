using EclEngine.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TestDemo.EclLibrary.Utils;
using TestDemo.Investment;
using TestDemo.InvestmentAssumption;
using TestDemo.InvestmentInputs;

namespace TestDemo.EclLibrary.Investment.Utils
{
    public class InvestmentDbUtil
    {
        public List<InvestmentAssetBook> GetInvestmentEclAssetBookData(Guid eclId)
        {
            string query = @"SELECT * FROM[ETI_IFRS9_DB].[dbo].[InvestmentAssetBooks]
                             Where[InvestmentEclUploadId] in (Select id from ETI_IFRS9_DB.dbo.InvestmentEclUploads where InvestmentEclId = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentAssetBook> assetBooks = DataTableUtil.ConvertDataTableToList<InvestmentAssetBook>(dt);

            return assetBooks;
        }

        public DateTime GetInvestmentEclReportDate(Guid eclId)
        {
            string query = @"SELECT * FROM [ETI_IFRS9_DB].[dbo].InvestmentEcls where id = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentEcl> eclRecord = DataTableUtil.ConvertDataTableToList<InvestmentEcl>(dt);

            return eclRecord[0].ReportingDate;
        }
        public List<InvestmentEclEadInputAssumption> GetInvestmentEclEadAssumption(Guid eclId)
        {
            string query = @"SELECT * FROM [ETI_IFRS9_DB].[dbo].InvestmentEclEadInputAssumptions where InvestmentEclId = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentEclEadInputAssumption> eadAssumption = DataTableUtil.ConvertDataTableToList<InvestmentEclEadInputAssumption>(dt);

            return eadAssumption;
        }
        public List<InvestmentEclLgdInputAssumption> GetInvestmentEclLgdAssumption(Guid eclId)
        {
            string query = @"SELECT * FROM [ETI_IFRS9_DB].[dbo].InvestmentEclLgdInputAssumptions where InvestmentEclId = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentEclLgdInputAssumption> lgdAssumption = DataTableUtil.ConvertDataTableToList<InvestmentEclLgdInputAssumption>(dt);

            return lgdAssumption;
        }
        public List<InvestmentEclPdInputAssumption> GetInvestmentEclPdAssumption(Guid eclId)
        {
            string query = @"SELECT * FROM [ETI_IFRS9_DB].[dbo].InvestmentEclPdInputAssumptions where PdGroup = 5 and InvestmentEclId = = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentEclPdInputAssumption> pdAssumptions = DataTableUtil.ConvertDataTableToList<InvestmentEclPdInputAssumption>(dt);

            return pdAssumptions;
        }

        public List<InvestmentEclPdInputAssumption> GetInvestmentEclMacroEcoScenario(Guid eclId)
        {
            string query = @"SELECT * FROM [ETI_IFRS9_DB].[dbo].InvestmentEclPdInputAssumptions where PdGroup = 6 and InvestmentEclId = = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentEclPdInputAssumption> pdAssumptions = DataTableUtil.ConvertDataTableToList<InvestmentEclPdInputAssumption>(dt);

            return pdAssumptions;
        }


        public List<InvestmentPdInputMacroEconomicAssumption> GetInvestmentEclMacroEcoAssumption(Guid eclId)
        {
            string query = @"SELECT * FROM [ETI_IFRS9_DB].[dbo].InvestmentPdInputMacroEconomicAssumptions where InvestmentEclId = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentPdInputMacroEconomicAssumption> macroAssumptions = DataTableUtil.ConvertDataTableToList<InvestmentPdInputMacroEconomicAssumption>(dt);

            return macroAssumptions;
        }


        public List<InvestmentEclPdFitchDefaultRate> GetInvestmentEclFitchCummulativeRa(Guid eclId)
        {
            string query = @"SELECT * FROM [ETI_IFRS9_DB].[dbo].InvestmentEclPdFitchDefaultRates where InvestmentEclId = '" + eclId + "')";
            DataTable dt = DbUtil.RunQuery(query);
            List<InvestmentEclPdFitchDefaultRate> fitchDefaultRates = DataTableUtil.ConvertDataTableToList<InvestmentEclPdFitchDefaultRate>(dt);

            return fitchDefaultRates;
        }
    }
}
