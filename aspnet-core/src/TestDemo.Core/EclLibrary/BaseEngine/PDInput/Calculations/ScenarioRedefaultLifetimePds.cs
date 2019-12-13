using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class ScenarioRedefaultLifetimePds
    {
        private string _scenario;
        protected ScenarioMarginalPd _scenarioMarginalPd;

        public ScenarioRedefaultLifetimePds(string scenario)
        {
            _scenario = scenario;
            _scenarioMarginalPd = new ScenarioMarginalPd(_scenario);
        }
        public void Run()
        {
            DataTable dataTable = ComputeRedefaultLifetimePd();
            string stop = "Stop";
        }

        public DataTable ComputeRedefaultLifetimePd()
        {
            DataTable redefaultLifetimePd = new DataTable();
            redefaultLifetimePd.Columns.Add(MarginalLifetimeRedefaultPdColumns.PdGroup, typeof(string));
            redefaultLifetimePd.Columns.Add(MarginalLifetimeRedefaultPdColumns.ProjectionMonth, typeof(int));
            redefaultLifetimePd.Columns.Add(MarginalLifetimeRedefaultPdColumns.Value, typeof(double));

            DataTable marginalPd = GetScenarioMarginalPd();
            double readjustmentFactor = GetRedefaultAdjustmentFactor();

            double test = GetMonthMarginalPdForPdGroup(marginalPd, "1", 10, readjustmentFactor);
            double test2 = marginalPd.AsEnumerable()
                            .FirstOrDefault(row => row.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup) == "1" && row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) == 10)
                            .Field<double>(MarginalLifetimeRedefaultPdColumns.Value);
            double test3 = test2 * readjustmentFactor;
            double test4 = test3 * test;

            foreach (DataRow row in marginalPd.Rows)
            {
                double prevValue = GetMonthMarginalPdForPdGroup(marginalPd, row.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup), 
                                                                            row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth),
                                                                            readjustmentFactor);
                double marginalPdValue = row.Field<double>(MarginalLifetimeRedefaultPdColumns.Value);

                DataRow dr = redefaultLifetimePd.NewRow();
                dr[MarginalLifetimeRedefaultPdColumns.PdGroup] = row.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup);
                dr[MarginalLifetimeRedefaultPdColumns.ProjectionMonth] = row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth);
                dr[MarginalLifetimeRedefaultPdColumns.Value] = row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) == 1 ?
                                                                    Math.Min(marginalPdValue * readjustmentFactor, 1.0) :
                                                                    prevValue * Math.Min(marginalPdValue * readjustmentFactor, 1.0);

                redefaultLifetimePd.Rows.Add(dr);
            }

            return redefaultLifetimePd;
        }
        //0.98867548257824434 || 0.00154465569130425
        protected double GetMonthMarginalPdForPdGroup(DataTable marginalPd, string pdGroup, int month, double readjustmentFactor)
        {
            var range = marginalPd.AsEnumerable()
                            .Where(x => x.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup) == pdGroup
                                                && x.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) < (month == 1 ? 2 : month))
                            .Select(x => {
                                double value = x.Field<double>(MarginalLifetimeRedefaultPdColumns.Value);

                                return Math.Min(value * readjustmentFactor, 1.0);
                             }).ToArray();
            var aggr = range.Aggregate(1.0, (acc, x) => acc * (1.0 - x));
            return aggr;
        }
        protected double GetRedefaultAdjustmentFactor()
        {
            return Convert.ToDouble(GetPdInputAssumptions().AsEnumerable()
                                        .FirstOrDefault(row => row.Field<string>(PdAssumptionsRowKey.AssumptionsColumn) == PdAssumptionsRowKey.ReDefaultAdjustmentFactor)
                                        .Field<string>(PdAssumptionsRowKey.ValuesColumn));
        }
        protected DataTable GetPdInputAssumptions()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetPdInputAssumptionsData());
        }
        protected DataTable GetScenarioMarginalPd()
        {
            return _scenarioMarginalPd.ComputeMaginalPd();
        }
    }
}
