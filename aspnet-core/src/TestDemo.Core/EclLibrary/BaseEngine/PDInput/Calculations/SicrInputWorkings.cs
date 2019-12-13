using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class SicrInputWorkings
    {
        protected ScenarioLifetimePd _scenarioLifetimePd;
        protected ScenarioRedefaultLifetimePds _scenarioRedefaultLifetimePd;
        protected PdMapping _pdMapping;

        public SicrInputWorkings()
        {
            _scenarioLifetimePd = new ScenarioLifetimePd(TempEclData.ScenarioBest);
            _scenarioRedefaultLifetimePd = new ScenarioRedefaultLifetimePds(TempEclData.ScenarioBest);
            _pdMapping = new PdMapping();
        }

        public void Run()
        {
            DataTable dataTable = ComputeSicrInput();

           string stop = "stop";
        }

        public DataTable ComputeSicrInput()
        {
            DataTable sicrInput = new DataTable();
            sicrInput.Columns.Add(SicrInputsColumns.ContractId, typeof(string));
            sicrInput.Columns.Add(PdMappingColumns.PdGroup, typeof(string));
            sicrInput.Columns.Add(PdMappingColumns.TimeToMaturityMonths, typeof(int));
            //sicrInput.Columns.Add(SicrInputsColumns.ImpairedDate, typeof(string));
            //sicrInput.Columns.Add(SicrInputsColumns.DefaultDate, typeof(string));
            sicrInput.Columns.Add(PdMappingColumns.MaxDpd, typeof(int));
            sicrInput.Columns.Add(PdMappingColumns.MaxClassificationScore, typeof(int));
            //sicrInput.Columns.Add(SicrInputsColumns.RestructureIndicator, typeof(int));
            //sicrInput.Columns.Add(SicrInputsColumns.RestructureRisk, typeof(int));
            //sicrInput.Columns.Add(SicrInputsColumns.WatchlistIndicator, typeof(int));
            //sicrInput.Columns.Add(SicrInputsColumns.CurrentRating, typeof(int));
            sicrInput.Columns.Add(SicrInputsColumns.Pd12Month, typeof(double));
            sicrInput.Columns.Add(SicrInputsColumns.LifetimePd, typeof(double));
            sicrInput.Columns.Add(SicrInputsColumns.RedefaultLifetimePd, typeof(double));
            sicrInput.Columns.Add(SicrInputsColumns.Stage1Transition, typeof(int));
            sicrInput.Columns.Add(SicrInputsColumns.Stage2Transition, typeof(int));
            sicrInput.Columns.Add(SicrInputsColumns.DaysPastDue, typeof(int));
            //sicrInput.Columns.Add(SicrInputsColumns.OriginationRating, typeof(int));
            //sicrInput.Columns.Add(SicrInputsColumns.Origination12MonthPd, typeof(double));
            //sicrInput.Columns.Add(SicrInputsColumns.OriginationLifetimePd, typeof(double));
            //sicrInput.Columns.Add(SicrInputsColumns.ImpairedDate, typeof(DateTime));
            //sicrInput.Columns.Add(SicrInputsColumns.DefaultDate, typeof(DateTime));


            string[] testAccounts = { "15033346", "15036347", "222017177" };

            DataTable loanbookTable = GetLoanbookData().AsEnumerable()
                                        //.Where(x => x.Field<string>(LoanBookColumns.ContractID).Substring(0, 3) != "EXP")
            //                            .Where(x => x.Field<string>(LoanBookColumns.ContractEndDate) != null && testAccounts.Contains(x.Field<string>(LoanBookColumns.ContractID)))
                                        //.Take(500)
                                        .CopyToDataTable();
            DataTable lifetimePds = _scenarioLifetimePd.ComputeLifetimePd();
            DataTable redefaultLifetimePds = _scenarioRedefaultLifetimePd.ComputeRedefaultLifetimePd();
            DataTable pdMapping = _pdMapping.ComputePdMappingTable();

            foreach (DataRow loanbookRow in loanbookTable.Rows)
            {
                DataRow contractPdMapping = pdMapping.AsEnumerable().FirstOrDefault(x => x.Field<string>(LoanBookColumns.ContractID) == loanbookRow.Field<string>(LoanBookColumns.ContractID));
                if(contractPdMapping == null)
                {
                    continue;
                }
                string contractPdGroup = contractPdMapping.Field<string>(PdMappingColumns.PdGroup);
                int contractTtmMonths = contractPdMapping.Field<int>(PdMappingColumns.TimeToMaturityMonths);
                string impairedDate = loanbookRow.Field<DateTime>(LoanBookColumns.ImpairedDate).ToString() == "1/1/1900 12:00:00 AM" ? null : loanbookRow.Field<DateTime>(LoanBookColumns.ImpairedDate).ToString();
                string defaultDate = loanbookRow.Field<DateTime>(LoanBookColumns.DefaultDate).ToString() == "1/1/1900 12:00:00 AM" ? null : loanbookRow.Field<DateTime>(LoanBookColumns.DefaultDate).ToString();
                int maxClassification = contractPdMapping.Field<int>(PdMappingColumns.MaxClassificationScore);
                long maxDpd = contractPdMapping.Field<long>(PdMappingColumns.MaxDpd);

                DataRow sicrRow = sicrInput.NewRow();
                sicrRow[SicrInputsColumns.ContractId] = loanbookRow.Field<string>(LoanBookColumns.ContractID);
                sicrRow[PdMappingColumns.PdGroup] = contractPdGroup;
                sicrRow[PdMappingColumns.TimeToMaturityMonths] = contractTtmMonths;
                //sicrRow[SicrInputsColumns.ImpairedDate] = impairedDate;
                //sicrRow[SicrInputsColumns.DefaultDate] = defaultDate;
                sicrRow[PdMappingColumns.MaxClassificationScore] = maxClassification;
                sicrRow[PdMappingColumns.MaxDpd] = maxDpd;
                //sicrRow[SicrInputsColumns.RestructureIndicator] = loanbookRow.Field<bool>(LoanBookColumns.RestructureIndicator);
                //sicrRow[SicrInputsColumns.RestructureRisk] = loanbookRow.Field<string>(LoanBookColumns.RestructureRisk);
                //sicrRow[SicrInputsColumns.WatchlistIndicator] = loanbookRow.Field<bool>(LoanBookColumns.WatchlistIndicator);
                //sicrRow[SicrInputsColumns.CurrentRating] = loanbookRow.Field<string>(LoanBookColumns.CurrentRating);
                sicrRow[SicrInputsColumns.Pd12Month] = ComputeLifetimeAndRedefaultPds(lifetimePds, contractPdGroup, 12);
                sicrRow[SicrInputsColumns.LifetimePd] = ComputeLifetimeAndRedefaultPds(lifetimePds, contractPdGroup, contractTtmMonths);
                sicrRow[SicrInputsColumns.RedefaultLifetimePd] = ComputeLifetimeAndRedefaultPds(redefaultLifetimePds, contractPdGroup, contractTtmMonths);
                sicrRow[SicrInputsColumns.Stage1Transition] = Math.Round(ComputeStageDaysPastDue(impairedDate));
                sicrRow[SicrInputsColumns.Stage2Transition] = ComputeStageDaysPastDue(defaultDate);
                sicrRow[SicrInputsColumns.DaysPastDue] = ComputeDaysPastDue(maxClassification, maxDpd);

                sicrInput.Rows.Add(sicrRow);
            }

            return sicrInput;
        }
        protected long ComputeDaysPastDue(int maxClassification, long maxDpd)
        {
            if(maxClassification == 1 || maxClassification == 2)
            {
                return maxDpd < 30 ? 0 : 30;
            }
            else if (maxClassification == 3)
            {
                return 90;
            }
            else if (maxClassification == 4)
            {
                return 180;
            }
            else
            {
                return 360;
            }
        }
        protected double ComputeStageDaysPastDue(string date)
        {
            return date == null ? 0 : ExcelFormulaUtil.YearFrac(DateTime.Parse(date), TempEclData.ReportDate) * 365;
        }
        protected double ComputeLifetimeAndRedefaultPds(DataTable lifetimePd, string contractPdMapping, int noOfMonths)
        {
            if (noOfMonths == 0)
            {
                return 1.0;
            }
            var monthPds = lifetimePd.AsEnumerable()
                                        .Where(row => row.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup) == contractPdMapping
                                                   && row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) <= noOfMonths)
                                        .Select(row => row.Field<double>(MarginalLifetimeRedefaultPdColumns.Value)).ToArray();
            return monthPds.Aggregate(0.0, (acc, x) => acc + x);
        }
        protected DataTable GetLoanbookData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetLoanbookData());
        }
    }
}
