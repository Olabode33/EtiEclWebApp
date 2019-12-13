using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class CreditIndex
    {
        protected const int _maxCreditIndexMonth = 60;
        protected VasicekWorkings _vasicekWorkings;
        protected IndexForecastWorkings _indexForecastBest;
        protected IndexForecastWorkings _indexForecastOptimistics;
        protected IndexForecastWorkings _indexForecastDownturn;

        public CreditIndex()
        {
            _vasicekWorkings = new VasicekWorkings(TempEclData.ScenarioBest);
            _indexForecastBest = new IndexForecastWorkings(TempEclData.ScenarioBest);
            _indexForecastOptimistics = new IndexForecastWorkings(TempEclData.ScenarioOptimistic);
            _indexForecastDownturn = new IndexForecastWorkings(TempEclData.ScenarioDownturn);
        }

        public void Run()
        {
            DataTable dataTable = ComputeCreditIndex();
            string stop = "Stop";
        }

        public DataTable ComputeCreditIndex()
        {
            DataTable creditIndex = new DataTable();
            creditIndex.Columns.Add(CreditIndexColumns.ProjectionMonth, typeof(string));
            creditIndex.Columns.Add(CreditIndexColumns.BestEstimate, typeof(double));
            creditIndex.Columns.Add(CreditIndexColumns.Optimistic, typeof(double));
            creditIndex.Columns.Add(CreditIndexColumns.Downturn, typeof(double));

            DataTable indexForecastBest = GetScenarioIndexForecasting(_indexForecastBest);
            DataTable indexForecastOptimistic = GetScenarioIndexForecasting(_indexForecastOptimistics);
            DataTable indexForecastDownturn = GetScenarioIndexForecasting(_indexForecastDownturn);

            for (int month = 0; month <= _maxCreditIndexMonth; month++)
            {
                int monthOffset = Convert.ToInt32((month - 1) / 3) * 3 + 3;
                DateTime eoMonth = ExcelFormulaUtil.EOMonth(TempEclData.ReportDate, monthOffset);
                double vasicekIndexUsed = GetScenarioVasicekIndex();

                DataRow dr = creditIndex.NewRow();
                dr[CreditIndexColumns.ProjectionMonth] = month;
                dr[CreditIndexColumns.BestEstimate] = month < 1 ? vasicekIndexUsed : indexForecastBest.AsEnumerable().FirstOrDefault(x => x.Field<DateTime>(IndexForecastColumns.Date) == eoMonth).Field<double>(IndexForecastColumns.Standardised);
                dr[CreditIndexColumns.Optimistic] = month < 1 ? vasicekIndexUsed : indexForecastOptimistic.AsEnumerable().FirstOrDefault(x => x.Field<DateTime>(IndexForecastColumns.Date) == eoMonth).Field<double>(IndexForecastColumns.Standardised);
                dr[CreditIndexColumns.Downturn] = month < 3 ? vasicekIndexUsed : indexForecastDownturn.AsEnumerable().FirstOrDefault(x => x.Field<DateTime>(IndexForecastColumns.Date) == eoMonth).Field<double>(IndexForecastColumns.Standardised);

                creditIndex.Rows.Add(dr);
            }

            return creditIndex;
        }
        
        protected DataTable GetScenarioIndexForecasting(IndexForecastWorkings  indexForecastWorkings)
        {
            return indexForecastWorkings.ComputeIndexForecast();
        }
        protected double GetScenarioVasicekIndex()
        {
            return _vasicekWorkings.ComputeEtiNplIndex().AsEnumerable()
                    .FirstOrDefault(row => row.Field<DateTime>(VasicekEtiNplIndexColumns.Date) == TempEclData.ReportDate)
                    .Field<double>(VasicekEtiNplIndexColumns.Index);
        }
    }
}
