using System;
using System.Data;
using System.Linq;
using EclEngine.Utils;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class PdMapping
    {
        public void Run()
        {
            DataTable dataTable = ComputePdMappingTable();
            string stop = "stop";
        }

        public DataTable ComputePdMappingTable()
        {
            var temp = GetPdInputAssumptions();
            string[] testAccounts = { "103ABLD150330005", "15036347", "222017177" };

            int expOdPerformacePastRepoting = Convert.ToInt32(
                                                    GetPdInputAssumptions().AsEnumerable()
                                                    .FirstOrDefault(row => row.Field<string>(PdAssumptionsRowKey.AssumptionsColumn) == PdAssumptionsRowKey.Expired)
                                                    .Field<string>(PdAssumptionsRowKey.ValuesColumn));
            int odPerformancePastExpiry = Convert.ToInt32(
                                                GetPdInputAssumptions().AsEnumerable()
                                                    .FirstOrDefault(row => row.Field<string>(PdAssumptionsRowKey.AssumptionsColumn) == PdAssumptionsRowKey.NonExpired)
                                                    .Field<string>(PdAssumptionsRowKey.ValuesColumn));
            DataTable loanbook = GetLoanbookData().AsEnumerable()
                                    //.Where(x => x.Field<string>(LoanBookColumns.ContractID).Substring(0, 3) != "EXP")
                                    ///.Where(x => x.Field<string>(LoanBookColumns.ContractEndDate) != null && testAccounts.Contains(x.Field<string>(LoanBookColumns.ContractID)))
                                    //.Take(500)
                                    .CopyToDataTable();

            DataTable pdMappingTable = new DataTable();
            pdMappingTable.Columns.Add(LoanBookColumns.ContractID, typeof(string));
            pdMappingTable.Columns.Add(LoanBookColumns.AccountNo, typeof(string));
            pdMappingTable.Columns.Add(LoanBookColumns.ProductType, typeof(string));
            pdMappingTable.Columns.Add(LoanBookColumns.RatingModel, typeof(string));
            pdMappingTable.Columns.Add(LoanBookColumns.Segment, typeof(string));
            pdMappingTable.Columns.Add(PdMappingColumns.RatingUsed, typeof(long));
            pdMappingTable.Columns.Add(PdMappingColumns.ClassificationScore, typeof(int)).AllowDBNull = true;
            pdMappingTable.Columns.Add(PdMappingColumns.MaxClassificationScore, typeof(int));
            pdMappingTable.Columns.Add(PdMappingColumns.MaxDpd, typeof(long));
            //pdMappingTable.Columns.Add(PdMappingColumns.RestructureEndDate, typeof(string));
            pdMappingTable.Columns.Add(PdMappingColumns.TimeToMaturityMonths, typeof(int));
            pdMappingTable.Columns.Add(PdMappingColumns.PdGroup, typeof(string));


            foreach (DataRow loanbookRecord in loanbook.Rows)
            {
                DataRow mappingRow = pdMappingTable.NewRow();
                mappingRow[LoanBookColumns.ContractID] = loanbookRecord.Field<string>(LoanBookColumns.ContractID);
                mappingRow[LoanBookColumns.AccountNo] = loanbookRecord.Field<string>(LoanBookColumns.AccountNo);
                mappingRow[LoanBookColumns.ProductType] = loanbookRecord.Field<string>(LoanBookColumns.ProductType);
                mappingRow[LoanBookColumns.RatingModel] = loanbookRecord.Field<string>(LoanBookColumns.RatingModel);
                mappingRow[LoanBookColumns.Segment] = loanbookRecord.Field<string>(LoanBookColumns.Segment);
                mappingRow[PdMappingColumns.RatingUsed] = ComputeRatingUsedPerRecord(loanbookRecord);
                mappingRow[PdMappingColumns.ClassificationScore] = ComputeClassificationScorePerRecord(loanbookRecord);
                mappingRow[PdMappingColumns.MaxDpd] = ComputeMaxDpdPerRecord(loanbookRecord, loanbook);
                mappingRow[PdMappingColumns.TimeToMaturityMonths] = ComputeTimeToMaturityMonthsPerRecord(loanbookRecord, expOdPerformacePastRepoting, odPerformancePastExpiry);
                mappingRow[PdMappingColumns.PdGroup] = ComputePdGroupingPerRecord(mappingRow);

                pdMappingTable.Rows.Add(mappingRow);
            }


            pdMappingTable = pdMappingTable.AsEnumerable()
                                .Select(row =>
                                {
                                    row[PdMappingColumns.MaxClassificationScore] = ComputeMaxClassificationScorePerRecord(row, pdMappingTable);
                                    return row;
                                }).CopyToDataTable();

            return pdMappingTable;
        }
        protected string ComputePdGroupingPerRecord(DataRow pdMappingWorkingRecord)
        {
            string pdGrouping = "";
            string[] productTypes = { "od", "card", "cards" };
            if(pdMappingWorkingRecord.Field<string>(LoanBookColumns.ContractID).Substring(0, 3) == "EXP" ||
                    (productTypes.Contains(pdMappingWorkingRecord.Field<string>(LoanBookColumns.ProductType).ToLower()) && pdMappingWorkingRecord.Field<int>(PdMappingColumns.TimeToMaturityMonths) == 0))
            {
                pdGrouping = "EXP";
            }
            else
            {
                if (pdMappingWorkingRecord.Field<string>(LoanBookColumns.RatingModel).ToLower() == "yes")
                {
                    pdGrouping = pdMappingWorkingRecord.Field<long>(PdMappingColumns.RatingUsed).ToString();
                }
                else
                {
                    pdGrouping = pdMappingWorkingRecord.Field<string>(LoanBookColumns.Segment).ToLower() == "commercial" ? "COMM" : "CONS";
                    pdGrouping += pdMappingWorkingRecord.Field<long>(PdMappingColumns.MaxDpd) < 30 ? "_STAGE_1" : "_STAGE_2";
                }
            }

            return pdGrouping;
        }
        protected int ComputeTimeToMaturityMonthsPerRecord(DataRow loanbookRecord, int expOdPerformacePastRepoting, int odPerformancePastExpiry)
        {
            if (loanbookRecord.Field<string>(LoanBookColumns.ContractID).Substring(0,3) == "EXP")
            {
                return 0;
            }
            else
            {
                int xValue = 0;
                int yValue = 0;
                
                DateTime? endDate;
                string restructureED = loanbookRecord[LoanBookColumns.RestructureEndDate].ToString();
                if (loanbookRecord.Field<bool>(LoanBookColumns.RestructureIndicator) && (!string.IsNullOrWhiteSpace(restructureED)))
                {
                    //string restructureED = loanbookRecord[LoanBookColumns.RestructureEndDate].ToString();
                    endDate =  DateTime.Parse(loanbookRecord[LoanBookColumns.RestructureEndDate].ToString());
                }
                else
                {
                    endDate = DateTime.Parse(loanbookRecord[LoanBookColumns.ContractEndDate].ToString());
                }
                var eomonth = ExcelFormulaUtil.EOMonth(endDate);
                var yearFrac = ExcelFormulaUtil.YearFrac(TempEclData.ReportDate, eomonth);
                var round = Convert.ToInt32(Math.Round(yearFrac * 12, 0));

                xValue = endDate > TempEclData.ReportDate ? round : 0;

                var maxx = Math.Max(expOdPerformacePastRepoting - round, 0);
                var prod = endDate < TempEclData.ReportDate ? maxx : odPerformancePastExpiry;
                yValue = loanbookRecord.Field<string>(LoanBookColumns.ProductType) == "OD" || loanbookRecord.Field<string>(LoanBookColumns.ProductType) == "OD" ? prod : 0;

                //Financial.YearFrac()
                return xValue + yValue;
            }
        }
        protected DateTime? ComputeRestructureEndDatePerRecord(DataRow loanbookRecord)
        {
            var restructureEndDate = loanbookRecord.Field<DateTime>(LoanBookColumns.RestructureEndDate);
            if (restructureEndDate == null)
            {
                return null;
            } 
            else
            {
                return restructureEndDate;
            }
        }
        protected int ComputeMaxClassificationScorePerRecord(DataRow pdMappingWorkingRecord, DataTable pdMappingWorkings)
        {
            return pdMappingWorkings.AsEnumerable()
                        .Where(row => row.Field<string>(LoanBookColumns.AccountNo) == pdMappingWorkingRecord.Field<string>(LoanBookColumns.AccountNo))
                        .Max(row => row.Field<int>(PdMappingColumns.ClassificationScore));
        }
        protected long? ComputeMaxDpdPerRecord(DataRow loanbookRecord, DataTable loanbook)
        {

            var temp = loanbook.AsEnumerable()
                                .Where(row => row.Field<string>(LoanBookColumns.AccountNo) == loanbookRecord.Field<string>(LoanBookColumns.AccountNo))
                                .Max(row => row.Field<long?>(LoanBookColumns.DaysPastDue));
            return temp == null ? 0 : temp;
        }
        protected int? ComputeClassificationScorePerRecord(DataRow loanbookRecord)
        {
            string classification = loanbookRecord.Field<string>(LoanBookColumns.Classification).ToUpper();
            switch (classification)
            {
                case "P":
                    return 1;
                case "W":
                    return 2;
                case "S":
                    return 3;
                case "D":
                    return 4;
                case "L":
                    return 5;
                default:
                    return null;
            }
        }
        protected long ComputeRatingUsedPerRecord(DataRow loanbookRecord)
        {
            long current_rating = loanbookRecord.Field<Int64>(LoanBookColumns.CurrentRating);
            return current_rating > 10 ? current_rating / 10 : current_rating;
        }
        protected DataTable GetLoanbookData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetLoanbookData());
        }
        protected DataTable GetPdInputAssumptions()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetPdInputAssumptionsData());
        }
    }
}
