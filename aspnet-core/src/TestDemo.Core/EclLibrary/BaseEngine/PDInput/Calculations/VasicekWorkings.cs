using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class VasicekWorkings
    {
        private string _scenario;

        public VasicekWorkings(string screnario)
        {
            _scenario = screnario;
        }

        public void Run()
        {
            DataTable dataTable = ComputeVasicekScenario();
            string stop = "Stop";
        }

        public DataTable ComputeVasicekScenario()
        {
            DataTable vasicek = new DataTable();
            vasicek.Columns.Add(VasicekEtiNplIndexColumns.Date, typeof(DateTime));
            vasicek.Columns.Add(VasicekEtiNplIndexColumns.Month, typeof(int));
            vasicek.Columns.Add(VasicekEtiNplIndexColumns.ScenarioPd, typeof(double));
            vasicek.Columns.Add(VasicekEtiNplIndexColumns.ScenarioIndex, typeof(double));
            vasicek.Columns.Add(VasicekEtiNplIndexColumns.ScenarioFactor, typeof(double));

            double pdTtc = ComputePdTtc();
            double averageFittedIndex = ComputeVasicekAverageFitted();
            DataTable indexForecast = GetScenarioIndexForecastResult();
            int month = 1;

            foreach (DataRow row in indexForecast.Rows)
            {
                double scenarioPd = ComputeVasicekIndex(row.Field<double>(IndexForecastColumns.Standardised), pdTtc, TempEclData.Rho);

                DataRow dr = vasicek.NewRow();
                dr[VasicekEtiNplIndexColumns.Date] = row.Field<DateTime>(IndexForecastColumns.Date);
                dr[VasicekEtiNplIndexColumns.Month] = month;
                dr[VasicekEtiNplIndexColumns.ScenarioIndex] = row.Field<double>(IndexForecastColumns.Standardised);
                dr[VasicekEtiNplIndexColumns.ScenarioPd] = scenarioPd;
                dr[VasicekEtiNplIndexColumns.ScenarioFactor] = averageFittedIndex == 0 ? 1 : scenarioPd / averageFittedIndex;

                vasicek.Rows.Add(dr);
                month++;
            }

            return vasicek;
        }
        protected DataTable GetScenarioIndexForecastResult()
        {
            IndexForecastWorkings indexForecastWorkings = new IndexForecastWorkings(_scenario);
            return indexForecastWorkings.ComputeIndexForecast();
        }
        protected double ComputeVasicekAverageFitted()
        {
            DataTable fitted = ComputeEtiNplIndex();
            return fitted.AsEnumerable()
                    .Where(row => row.Field<DateTime>(VasicekEtiNplIndexColumns.Date) >= new DateTime(TempEclData.ReportDate.Year - 3, TempEclData.ReportDate.Month, TempEclData.ReportDate.Day))
                    .Average(row => row.Field<double>(VasicekEtiNplIndexColumns.Fitted));
        }
        public DataTable ComputeEtiNplIndex()
        {
            DataTable etiNpl = GetEtiNplData();
            DataTable historicIndex = GetHistoricIndexData();
            double pdTtc = ComputePdTtc();

            DataTable vasicekEtiNplIndex = new DataTable();
            vasicekEtiNplIndex.Columns.Add(VasicekEtiNplIndexColumns.Date, typeof(DateTime));
            vasicekEtiNplIndex.Columns.Add(VasicekEtiNplIndexColumns.EtiNpl, typeof(double));
            vasicekEtiNplIndex.Columns.Add(VasicekEtiNplIndexColumns.Index, typeof(double));
            vasicekEtiNplIndex.Columns.Add(VasicekEtiNplIndexColumns.Fitted, typeof(double));
            vasicekEtiNplIndex.Columns.Add(VasicekEtiNplIndexColumns.Residuals, typeof(double));
                                        
            foreach(DataRow etiNplRecord in etiNpl.Rows)
            {
                double index = historicIndex.AsEnumerable()
                                            .FirstOrDefault(row => row.Field<DateTime>(HistoricIndexColumns.Date) == etiNplRecord.Field<DateTime>(EtiNplColumns.Date))
                                            .Field<double>(HistoricIndexColumns.Standardised);

                DataRow newRecord = vasicekEtiNplIndex.NewRow();
                newRecord[VasicekEtiNplIndexColumns.Date] = etiNplRecord.Field<DateTime>(EtiNplColumns.Date);
                newRecord[VasicekEtiNplIndexColumns.EtiNpl] = etiNplRecord.Field<double>(EtiNplColumns.Series);
                newRecord[VasicekEtiNplIndexColumns.Index] = index;
                newRecord[VasicekEtiNplIndexColumns.Fitted] = ComputeVasicekIndex(index, pdTtc, TempEclData.Rho);
                newRecord[VasicekEtiNplIndexColumns.Residuals] = etiNplRecord.Field<double>(EtiNplColumns.Series) - ComputeVasicekIndex(index, pdTtc, TempEclData.Rho);

                vasicekEtiNplIndex.Rows.Add(newRecord);
            }

            return vasicekEtiNplIndex;
        }
        protected double ComputeVasicekIndex(double index, double pd_ttc, double rho)
        {
            //var t1 = ExcelFormulaUtil.NormSInv(pd_ttc);
            //var t2 = Math.Sqrt(rho);
            //var t3 = Math.Sqrt(1 - rho);
            //var t4 = (ExcelFormulaUtil.NormSInv(pd_ttc) + Math.Sqrt(rho) * index);
            //var t5 = (ExcelFormulaUtil.NormSInv(pd_ttc) + Math.Sqrt(rho) * index) / Math.Sqrt(1 - rho);
            //var tF = ExcelFormulaUtil.NormSDist((ExcelFormulaUtil.NormSInv(pd_ttc) + Math.Sqrt(rho) * index) / Math.Sqrt(1 - rho));
            return ExcelFormulaUtil.NormSDist((ExcelFormulaUtil.NormSInv(pd_ttc) + Math.Sqrt(rho) * index) / Math.Sqrt(1 - rho));
        }
        protected double ComputePdTtc()
        {
            DataTable etiNpl = GetEtiNplData();
            return etiNpl.AsEnumerable()
                    .Average(row => row.Field<double>(EtiNplColumns.Series));
        }
        protected DataTable GetHistoricIndexData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GethistoricIndexData());
        }
        protected DataTable GetEtiNplData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetEtiNplData());
        }
    }
}
