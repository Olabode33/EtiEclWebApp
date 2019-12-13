using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class IndexForecastWorkings
    {
        private string _scenario;

        public IndexForecastWorkings(string screnario)
        {
            _scenario = screnario;
        }

        public void Run()
        {
            DataTable table = ComputeIndexForecast();
            string stop = "stop";
        }

        public DataTable ComputeIndexForecast()
        {
            DataTable indexForecast = new DataTable();
            indexForecast.Columns.Add(IndexForecastColumns.Date, typeof(DateTime));
            indexForecast.Columns.Add(IndexForecastColumns.Actual, typeof(double));
            indexForecast.Columns.Add(IndexForecastColumns.Standardised, typeof(double));

            DataTable principalData = ComputeScenarioPrincipalComponents();

            foreach (DataRow row in principalData.Rows)
            {
                double actual = row.Field<double>(IndexForecastColumns.Principal1) * TempEclData.IndexWeight1
                                    + row.Field<double>(IndexForecastColumns.Principal2) * TempEclData.IndexWeight2;
                double indexStandardDeviation = ComputeHistoricIndexStandardDeviation();
                double indexMean = ComputeHistoricIndexMean();

                DataRow dr = indexForecast.NewRow();
                dr[IndexForecastColumns.Date] = row.Field<DateTime>(IndexForecastColumns.Date);
                dr[IndexForecastColumns.Actual] = actual;
                dr[IndexForecastColumns.Standardised] = indexStandardDeviation == 0 ? 0 : (actual - indexMean) / indexStandardDeviation;

                indexForecast.Rows.Add(dr);
            }

            return indexForecast;
        }

        protected DataTable ComputeScenarioPrincipalComponents()
        {
            DataTable principalData = new DataTable();
            principalData.Columns.Add(IndexForecastColumns.Date, typeof(DateTime));
            principalData.Columns.Add(IndexForecastColumns.Principal1, typeof(double));
            principalData.Columns.Add(IndexForecastColumns.Principal2, typeof(double));

            DataTable statisticalInputs = GetStatisticalInputData();
            DataTable standardisedData = ComputeScenarioStandardisedData();
            DataRow macroeconomicPrincipal1 = statisticalInputs.AsEnumerable()
                                                .FirstOrDefault(row => row.Field<string>(StatisticalInputsColumns.Mode) == StatisticalInputsRowKeys.PrincipalScore1);
            DataRow macroeconomicPrincipal2 = statisticalInputs.AsEnumerable()
                                                .FirstOrDefault(row => row.Field<string>(StatisticalInputsColumns.Mode) == StatisticalInputsRowKeys.PrincipalScore2);

            foreach (DataRow row in standardisedData.Rows)
            {
                double[] standardised = new double[3];
                double[] principal1 = new double[3];
                double[] principal2 = new double[3];

                standardised[0] = row.Field<double>(IndexForecastColumns.PrimeLendingRate);
                standardised[1] = row.Field<double>(IndexForecastColumns.OilExports);
                standardised[2] = row.Field<double>(IndexForecastColumns.DifferencedGdpGrowthRate);

                principal1[0] = macroeconomicPrincipal1.Field<double>(StatisticalInputsColumns.PrimeLendingRate);
                principal1[1] = macroeconomicPrincipal1.Field<double>(StatisticalInputsColumns.OilExports);
                principal1[2] = macroeconomicPrincipal1.Field<double>(StatisticalInputsColumns.RealGdpGrowthRate);

                principal2[0] = macroeconomicPrincipal2.Field<double>(StatisticalInputsColumns.PrimeLendingRate);
                principal2[1] = macroeconomicPrincipal2.Field<double>(StatisticalInputsColumns.OilExports);
                principal2[2] = macroeconomicPrincipal2.Field<double>(StatisticalInputsColumns.RealGdpGrowthRate);


                DataRow dr = principalData.NewRow();
                dr[IndexForecastColumns.Date] = row.Field<DateTime>(IndexForecastColumns.Date);
                dr[IndexForecastColumns.Principal1] = ExcelFormulaUtil.SumProduct(standardised, principal1);
                dr[IndexForecastColumns.Principal2] = ExcelFormulaUtil.SumProduct(standardised, principal2);

                principalData.Rows.Add(dr);
            }

            return principalData;
        }

        protected DataTable ComputeScenarioStandardisedData()
        {
            DataTable standardisedData = new DataTable();
            standardisedData.Columns.Add(IndexForecastColumns.Date, typeof(DateTime));
            standardisedData.Columns.Add(IndexForecastColumns.PrimeLendingRate, typeof(double));
            standardisedData.Columns.Add(IndexForecastColumns.OilExports, typeof(double));
            standardisedData.Columns.Add(IndexForecastColumns.DifferencedGdpGrowthRate, typeof(double));


            DataTable statisticalInputs = GetStatisticalInputData();
            DataTable originalData = GetScenarioProjectionOriginalData();
            DataRow macroeconomicMean = statisticalInputs.AsEnumerable()
                                            .FirstOrDefault(row => row.Field<string>(StatisticalInputsColumns.Mode) == StatisticalInputsRowKeys.Mean);
            DataRow macroeconomicStandardDeviation = statisticalInputs.AsEnumerable()
                                                        .FirstOrDefault(row => row.Field<string>(StatisticalInputsColumns.Mode) == StatisticalInputsRowKeys.StandardDeviation);


            foreach(DataRow row in originalData.Rows)
            {
                DataRow dr = standardisedData.NewRow();
                dr[IndexForecastColumns.Date] = row.Field<DateTime>(IndexForecastColumns.Date);
                dr[IndexForecastColumns.PrimeLendingRate] = (row.Field<double>(IndexForecastColumns.PrimeLendingRate) -
                                                                            macroeconomicMean.Field<double>(StatisticalInputsColumns.PrimeLendingRate)) /
                                                                            macroeconomicStandardDeviation.Field<double>(StatisticalInputsColumns.PrimeLendingRate);
                dr[IndexForecastColumns.OilExports] = (row.Field<double>(IndexForecastColumns.OilExports) -
                                                                        macroeconomicMean.Field<double>(StatisticalInputsColumns.OilExports)) /
                                                                        macroeconomicStandardDeviation.Field<double>(StatisticalInputsColumns.OilExports);
                dr[IndexForecastColumns.DifferencedGdpGrowthRate] = (row.Field<double>(IndexForecastColumns.DifferencedGdpGrowthRate) -
                                                                                macroeconomicMean.Field<double>(StatisticalInputsColumns.RealGdpGrowthRate)) /
                                                                                macroeconomicStandardDeviation.Field<double>(StatisticalInputsColumns.RealGdpGrowthRate);

                standardisedData.Rows.Add(dr);
            }

            return standardisedData;
        }

        protected DataTable GetScenarioProjectionOriginalData()
        {
            DataTable originalData = new DataTable();
            originalData.Columns.Add(IndexForecastColumns.Date, typeof(DateTime));
            originalData.Columns.Add(IndexForecastColumns.PrimeLendingRate, typeof(double));
            originalData.Columns.Add(IndexForecastColumns.OilExports, typeof(double));
            originalData.Columns.Add(IndexForecastColumns.DifferencedGdpGrowthRate, typeof(double));

            DataTable projections = GetScenarioProjectionData();

            //originalData = projections.AsEnumerable()
            //                    .Where(row => row.Field<DateTime>(MacroeconomicProjectionColumns.Date) > TempEclData.ReportDate)
            //                    .CopyToDataTable();

            for(int i = 0; i < projections.Rows.Count; i++)
            {
                if(projections.Rows[i].Field<DateTime>(MacroeconomicProjectionColumns.Date) > TempEclData.ReportDate && i > 3)
                {
                    DataRow dr = originalData.NewRow();
                    dr[IndexForecastColumns.Date] = projections.Rows[i].Field<DateTime>(MacroeconomicProjectionColumns.Date);
                    dr[IndexForecastColumns.PrimeLendingRate] = projections.Rows[i - 2].Field<double>(MacroeconomicProjectionColumns.PrimeLendingRate);
                    dr[IndexForecastColumns.OilExports] = projections.Rows[i - 3].Field<double>(MacroeconomicProjectionColumns.OilExports);
                    dr[IndexForecastColumns.DifferencedGdpGrowthRate] = Convert.ToDouble(projections.Rows[i - 1].Field<string>(MacroeconomicProjectionColumns.DifferencedGdpGrowthRate));

                    originalData.Rows.Add(dr);
                }
            }


            return originalData;
        }

        protected double ComputeHistoricIndexStandardDeviation()
        {
            return ExcelFormulaUtil.CalculateStdDev(GetHistoricIndexData().AsEnumerable()
                                                       .Select(x => x.Field<double>(HistoricIndexColumns.Actual)));
        }
        protected double ComputeHistoricIndexMean()
        {
            return GetHistoricIndexData().AsEnumerable()
                        .Average(x => x.Field<double>(HistoricIndexColumns.Actual));
        }

        private DataTable GetScenarioProjectionData()
        {
            DataTable projections = new DataTable();

            switch (_scenario)
            {
                case TempEclData.ScenarioBest:
                    projections = GetBestMacroeconomicProjectionData();
                    break;
                case TempEclData.ScenarioOptimistic:
                    projections = GetOptimistcMacroeconomicProjectionData();
                    break;
                case TempEclData.ScenarioDownturn:
                    projections = GetDownturnMacroeconomicProjectionData();
                    break;
            }

            return projections;
        }

        protected DataTable GetStatisticalInputData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetStatisticalInputsData());
        }
        protected DataTable GetBestMacroeconomicProjectionData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetMacroEcoBestData());
        }
        protected DataTable GetOptimistcMacroeconomicProjectionData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetMacroEcoOptimisticData());
        }
        protected DataTable GetDownturnMacroeconomicProjectionData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetMacroEcoDownturnData());
        }
        protected DataTable GetHistoricIndexData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GethistoricIndexData());
        }
    }
}
