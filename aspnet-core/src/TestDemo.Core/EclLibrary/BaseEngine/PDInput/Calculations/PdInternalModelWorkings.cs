using EclEngine.Utils;
using System;
using System.Data;
using System.Linq;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class PdInternalModelWorkings
    {
        protected const int _maxLogRateYear = 15;
        protected const int _maxRatingYear = 20;
        protected const int _maxRatingRank = 9;

        public void Run()
        {
            DataTable dataTable = ComputeMonthlyCummulativeSurvival();

            string stop = "stop";
        }

        protected DataTable ComputeMonthlyCummulativeSurvival()
        {
            DataTable monthlyLogOddsRatio = ComputeMonthlyLogOddsRatio();

            DataTable monthlyCummulativeSurvivalResult = new DataTable();
            monthlyCummulativeSurvivalResult.Columns.Add(MonthlyLogOddsRatioColumns.Month, typeof(int));
            monthlyCummulativeSurvivalResult.Columns.Add(MonthlyLogOddsRatioColumns.Rank, typeof(int));
            monthlyCummulativeSurvivalResult.Columns.Add(MonthlyLogOddsRatioColumns.Rating, typeof(string));
            monthlyCummulativeSurvivalResult.Columns.Add(MonthlyLogOddsRatioColumns.CreditRating, typeof(double));

            ///Month 1 Computation
            var tempDt = monthlyLogOddsRatio.AsEnumerable()
                                    .Where(row => row.Field<int>(MonthlyLogOddsRatioColumns.Month) == 1)
                                    .CopyToDataTable();
            foreach (DataRow dr in tempDt.Rows)
            {
                DataRow dataRow = monthlyCummulativeSurvivalResult.NewRow();
                dataRow[MonthlyLogOddsRatioColumns.Month] = dr[MonthlyLogOddsRatioColumns.Month];
                dataRow[MonthlyLogOddsRatioColumns.Rank] = dr[MonthlyLogOddsRatioColumns.Rank];
                dataRow[MonthlyLogOddsRatioColumns.Rating] = dr[MonthlyLogOddsRatioColumns.Rating];
                dataRow[MonthlyLogOddsRatioColumns.CreditRating] = 1.0 - dr.Field<double>(MonthlyLogOddsRatioColumns.CreditRating);
                monthlyCummulativeSurvivalResult.Rows.Add(dataRow);
            }

            ///Month 2 to max computation
            for (int month = 2; month <= (_maxRatingYear * 12); month++)
            {
                DataTable prevMonthCreditRating = monthlyCummulativeSurvivalResult.AsEnumerable()
                                                    .Where(row => row.Field<int>(MonthlyLogOddsRatioColumns.Month) == month - 1)
                                                    .CopyToDataTable();

                DataTable currMonthCreditRating = monthlyLogOddsRatio.AsEnumerable()
                                                    .Where(row => row.Field<int>(MonthlyLogOddsRatioColumns.Month) == month)
                                                    .Select(row =>
                                                    {
                                                        double prev = prevMonthCreditRating.AsEnumerable()
                                                                        .FirstOrDefault(x => x.Field<int>(MonthlyLogOddsRatioColumns.Rank) == row.Field<int>(MonthlyLogOddsRatioColumns.Rank))
                                                                        .Field<double>(MonthlyLogOddsRatioColumns.CreditRating);
                                                        row[MonthlyLogOddsRatioColumns.CreditRating] = prev * (1 - row.Field<double>(MonthlyLogOddsRatioColumns.CreditRating));

                                                        return row;
                                                    })
                                                    .CopyToDataTable();

                monthlyCummulativeSurvivalResult.Merge(currMonthCreditRating);
            }


            return monthlyCummulativeSurvivalResult;
        }
        public DataTable ComputeMonthlyLogOddsRatio()
        {
            DataTable marginalDefaultRate = ComputeMarginalDefaultRate();
            DataTable monthlyLogOddsRatioResult = new DataTable();
            monthlyLogOddsRatioResult.Columns.Add(MonthlyLogOddsRatioColumns.Month, typeof(int));
            monthlyLogOddsRatioResult.Columns.Add(MonthlyLogOddsRatioColumns.Rank, typeof(int));
            monthlyLogOddsRatioResult.Columns.Add(MonthlyLogOddsRatioColumns.Rating, typeof(string));
            monthlyLogOddsRatioResult.Columns.Add(MonthlyLogOddsRatioColumns.CreditRating, typeof(double));

            int monthCount = 1;

            for (int year = 1; year <= _maxRatingYear; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    for(int rank = 1; rank <= _maxRatingRank; rank++)
                    {
                        DataRow rate = marginalDefaultRate.AsEnumerable()
                                    .FirstOrDefault(row =>
                                        (row.Field<int>(LogOddRatioColumns.Year) == year) &&
                                        (row.Field<int>(LogOddRatioColumns.Rank) == rank));

                        DataRow dataRow = monthlyLogOddsRatioResult.NewRow();
                        dataRow[MonthlyLogOddsRatioColumns.Month] = monthCount;
                        dataRow[MonthlyLogOddsRatioColumns.Rank] = rank;
                        dataRow[MonthlyLogOddsRatioColumns.Rating] = rate.Field<string>(LogOddRatioColumns.Rating); 
                        dataRow[MonthlyLogOddsRatioColumns.CreditRating] = 1.0 - Math.Pow((1.0 - rate.Field<double>(LogOddRatioColumns.LogOddsRatio)), (1.0 / 12.0)); ;
                        monthlyLogOddsRatioResult.Rows.Add(dataRow);
                    }
                    monthCount += 1;
                }
            }



            return monthlyLogOddsRatioResult;
        }
        protected DataTable ComputeMarginalDefaultRate()
        {
            DataTable cummulativeDefaultRate = ComputeCummulativeDefaultRate();

            ///Get cummulative values for year 1
            DataTable marginalDefaultRateResult = new DataTable();
            marginalDefaultRateResult = cummulativeDefaultRate.Clone();
            var tempDt = cummulativeDefaultRate.AsEnumerable()
                                    .Where(row => row.Field<int>(LogOddRatioColumns.Year) == 1)
                                    .CopyToDataTable();
            foreach(DataRow dr in tempDt.Rows)
            {
                DataRow dataRow = marginalDefaultRateResult.NewRow();
                dataRow[LogOddRatioColumns.Rank] = dr[LogOddRatioColumns.Rank];
                dataRow[LogOddRatioColumns.Rating] = dr[LogOddRatioColumns.Rating];
                dataRow[LogOddRatioColumns.Year] = dr[LogOddRatioColumns.Year];
                dataRow[LogOddRatioColumns.LogOddsRatio] = dr[LogOddRatioColumns.LogOddsRatio];
                marginalDefaultRateResult.Rows.Add(dataRow);
            }

            for (int year = 2; year <= _maxRatingYear; year++)
            {
                for (int rank = 1; rank <= _maxRatingRank; rank++)
                {
                    double prevYearRate = cummulativeDefaultRate.AsEnumerable()
                                            .FirstOrDefault(row =>
                                                (row.Field<int>(LogOddRatioColumns.Rank) == rank) &&
                                                (row.Field<int>(LogOddRatioColumns.Year) == year - 1))
                                            .Field<double>(LogOddRatioColumns.LogOddsRatio);

                    DataRow currYearRate = cummulativeDefaultRate.AsEnumerable()
                                            .FirstOrDefault(row =>
                                                (row.Field<int>(LogOddRatioColumns.Rank) == rank) &&
                                                (row.Field<int>(LogOddRatioColumns.Year) == year));

                    double rate = (currYearRate.Field<double>(LogOddRatioColumns.LogOddsRatio) - prevYearRate) / (1 - prevYearRate);

                    DataRow dataRow = marginalDefaultRateResult.NewRow();
                    dataRow[LogOddRatioColumns.Rank] = currYearRate[LogOddRatioColumns.Rank];
                    dataRow[LogOddRatioColumns.Rating] = currYearRate[LogOddRatioColumns.Rating];
                    dataRow[LogOddRatioColumns.Year] = currYearRate[LogOddRatioColumns.Year];
                    dataRow[LogOddRatioColumns.LogOddsRatio] = rate;
                    marginalDefaultRateResult.Rows.Add(dataRow);
                }
            }

            return marginalDefaultRateResult;
        }
        protected DataTable ComputeCummulativeDefaultRate()
        {
            DataTable logOddsRatio = ComputeLogsOddsRatio();

            DataTable cummulativeDefaultRateResult = logOddsRatio.AsEnumerable()
                                                                .Select(row => {
                                                                    row[LogOddRatioColumns.LogOddsRatio] = 1 / (1 + Math.Exp(row.Field<double>(LogOddRatioColumns.LogOddsRatio)));
                                                                    return row;
                                                                }).CopyToDataTable();

            return cummulativeDefaultRateResult;
        }
        protected DataTable ComputeLogsOddsRatio()
        {
            DataTable pd12MonthAssumption = Get12MonthPdAssumption();
            DataTable pdInputAssumptions = GetPdInputAssumptions();
            DataTable logRates = ComputeLogRates();

            DataTable logOddsRatioResult = new DataTable();
            logOddsRatioResult.Columns.Add(LogOddRatioColumns.Rank, typeof(int));
            logOddsRatioResult.Columns.Add(LogOddRatioColumns.Rating, typeof(string));
            logOddsRatioResult.Columns.Add(LogOddRatioColumns.Year, typeof(int));
            logOddsRatioResult.Columns.Add(LogOddRatioColumns.LogOddsRatio, typeof(double));

            string snpMappingInput = pdInputAssumptions.AsEnumerable().FirstOrDefault(row => row.Field<string>(PdAssumptionsRowKey.AssumptionsColumn) == PdAssumptionsRowKey.SnpMapping).Field<string>(PdAssumptionsRowKey.ValuesColumn);

            for (int rank = 1; rank <= _maxRatingRank; rank++)
            {
                string rating = pd12MonthAssumption.AsEnumerable()
                                   .FirstOrDefault(row => row.Field<double>(Pd12MonthColumns.CreditRating) == rank)
                                   .Field<string>(snpMappingInput == PdAssumptionsRowKey.SnpMappingValueEtiCreditPolicy ? 
                                                        Pd12MonthColumns.SnpMappingEtiCreditPolicy : Pd12MonthColumns.SnpMappingBestFit);


                //Year 1 computation
                double pdValue = pd12MonthAssumption.AsEnumerable().FirstOrDefault(row => row.Field<double>(Pd12MonthColumns.CreditRating) == rank).Field<double>(Pd12MonthColumns.Pd);
                double year1LogOddRatio = Math.Log((1 - pdValue) / pdValue);

                DataRow dataRow = logOddsRatioResult.NewRow();
                dataRow[LogOddRatioColumns.Rank] = rank;
                dataRow[LogOddRatioColumns.Rating] = rating;
                dataRow[LogOddRatioColumns.Year] = 1;
                dataRow[LogOddRatioColumns.LogOddsRatio] = year1LogOddRatio;
                logOddsRatioResult.Rows.Add(dataRow);

                //Year to Max computation
                double year1RatingLogRate = logRates.AsEnumerable()
                                                              .FirstOrDefault(row =>
                                                                                (row.Field<string>(LogOddRatioColumns.Rating) == rating) &&
                                                                                (row.Field<int>(LogOddRatioColumns.Year) == 1))
                                                              .Field<double>(LogRateColumns.LogOddsRatio);

                for (int year = 2; year <= _maxRatingYear; year++)
                {
                    double currentYearRatingLogRate = logRates.AsEnumerable()
                                                              .FirstOrDefault(row => 
                                                                                (row.Field<string>(LogOddRatioColumns.Rating) == rating) &&
                                                                                (row.Field<int>(LogOddRatioColumns.Year) == Math.Min(year, _maxLogRateYear)))
                                                              .Field<double>(LogRateColumns.LogOddsRatio);

                    double currentYearLogOddRatio = year1LogOddRatio + currentYearRatingLogRate - year1RatingLogRate;

                    DataRow currentYeardataRow = logOddsRatioResult.NewRow();
                    currentYeardataRow[LogOddRatioColumns.Rank] = rank;
                    currentYeardataRow[LogOddRatioColumns.Rating] = rating;
                    currentYeardataRow[LogOddRatioColumns.Year] = year;
                    currentYeardataRow[LogOddRatioColumns.LogOddsRatio] = currentYearLogOddRatio;
                    logOddsRatioResult.Rows.Add(currentYeardataRow);
                }
            }


            return logOddsRatioResult;
        }
        protected DataTable ComputeLogRates()
        {
            DataTable snpCummulativeRate = GetSnPCummulativeDefaultRateAssumption();

            DataTable logRateResult = new DataTable();
            logRateResult.Columns.Add(LogOddRatioColumns.Rating, typeof(string));
            logRateResult.Columns.Add(LogOddRatioColumns.Year, typeof(int));
            logRateResult.Columns.Add(LogRateColumns.LogOddsRatio, typeof(double));

            //DataTable snpCummulativeRating = snpCummulativeRate.DefaultView.ToTable(false,  SnPCummlativeDefaultRateColumns.Rating );

            foreach (DataRow row in snpCummulativeRate.Rows)
            {
                string rating = row.Field<string>(SnPCummlativeDefaultRateColumns.Rating);
                for (int year = 1; year <= _maxLogRateYear; year++)
                {
                    double defaultRate = row.Field<double>(year.ToString());
                    double log = Math.Log((1 - defaultRate) / defaultRate);

                    DataRow dataRow = logRateResult.NewRow();
                    dataRow[LogOddRatioColumns.Rating] = rating;
                    dataRow[LogOddRatioColumns.Year] = year;
                    dataRow[LogRateColumns.LogOddsRatio] = log;

                    logRateResult.Rows.Add(dataRow);
                }
            }


            return logRateResult;
        }
        protected DataTable GetSnPCummulativeDefaultRateAssumption()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetSnPCummulativeDefaultData());
        }
        protected DataTable Get12MonthPdAssumption()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.Get12MonthsPDData());
        }
        protected DataTable GetPdInputAssumptions()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetPdInputAssumptionsData());
        }
    }
}
