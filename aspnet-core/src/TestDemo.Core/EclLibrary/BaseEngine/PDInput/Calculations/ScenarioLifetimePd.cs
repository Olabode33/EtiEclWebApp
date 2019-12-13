using System.Data;
using System.Linq;
using EclEngine.Utils;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class ScenarioLifetimePd
    {
        private string _scenario;
        protected ScenarioMarginalPd _scenarioMarginalPd;

        public ScenarioLifetimePd(string scenario)
        {
            _scenario = scenario;
            _scenarioMarginalPd = new ScenarioMarginalPd(_scenario);
        }

        public void Run()
        {
            DataTable dataTable = ComputeLifetimePd();
            string stop = "Stop";
        }

        public DataTable ComputeLifetimePd()
        {
            DataTable lifetimePd = new DataTable();
            lifetimePd.Columns.Add(MarginalLifetimeRedefaultPdColumns.PdGroup, typeof(string));
            lifetimePd.Columns.Add(MarginalLifetimeRedefaultPdColumns.ProjectionMonth, typeof(int));
            lifetimePd.Columns.Add(MarginalLifetimeRedefaultPdColumns.Value, typeof(double));

            DataTable marginalPd = GetScenarioMarginalPd();

            foreach (DataRow row in marginalPd.Rows)
            {
                double month1 = GetMonth1MarginalPdForPdGroup(marginalPd, row.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup), row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth));
                double pd = row.Field<double>(MarginalLifetimeRedefaultPdColumns.Value);

                DataRow dr = lifetimePd.NewRow();
                dr[MarginalLifetimeRedefaultPdColumns.PdGroup] = row.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup);
                dr[MarginalLifetimeRedefaultPdColumns.ProjectionMonth] = row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth);
                dr[MarginalLifetimeRedefaultPdColumns.Value] = row.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) == 1 ?
                                                                    row.Field<double>(MarginalLifetimeRedefaultPdColumns.Value) :
                                                                    month1 * row.Field<double>(MarginalLifetimeRedefaultPdColumns.Value);

                lifetimePd.Rows.Add(dr);
            }

            return lifetimePd;
        }

        protected double GetMonth1MarginalPdForPdGroup(DataTable marginalPd, string pdGroup, int month)
        {
            var range = marginalPd.AsEnumerable()
                            .Where(x => x.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup) == pdGroup
                                                && x.Field<int>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) < (month == 1 ? 2 : month))
                            .Select(x => x.Field<double>(MarginalLifetimeRedefaultPdColumns.Value)).ToArray();
            var aggr = range.Aggregate(1.0, (acc, x) => acc * (1.0 - x));
            return aggr;
        }


        protected DataTable GetScenarioMarginalPd()
        {
            return _scenarioMarginalPd.ComputeMaginalPd();
        }
    }
}
