using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.PdInput.Calculations
{
    public class ScenarioMarginalPd
    {
        private string _scenario;
        protected PdInternalModelWorkings _pdInternalModelWorkings;
        protected VasicekWorkings _vasicekWorkings;

        public ScenarioMarginalPd(string scenario)
        {
            _scenario = scenario;
            _pdInternalModelWorkings = new PdInternalModelWorkings();
            _vasicekWorkings = new VasicekWorkings(_scenario);
        }

        public void Run()
        {
            DataTable dataTable = ComputeMaginalPd();
            string stop = "Stop";
        }

        public DataTable ComputeMaginalPd()
        {
            DataTable marginalPds = new DataTable();
            marginalPds.Columns.Add(MarginalLifetimeRedefaultPdColumns.PdGroup, typeof(string));
            marginalPds.Columns.Add(MarginalLifetimeRedefaultPdColumns.ProjectionMonth, typeof(int));
            marginalPds.Columns.Add(MarginalLifetimeRedefaultPdColumns.Value, typeof(double));

            DataTable logOddsRatio = GetMonthlyLogOddsRatio();
            DataTable varsicekIndex = GetVasicekScenario();
            DataTable nonInternalModelInput = GetNonInternalModelInputsData();

            for (int month = 1; month <= TempEclData.MaxMarginalLifetimeRedefaultPdMonth; month++)
            {

                int vasicekSearchMonth = Convert.ToInt32((month - 1) / 3) + 1;
                double vasicekIndexMonthValue = varsicekIndex.AsEnumerable()
                                                        .FirstOrDefault(row => row.Field<int>(VasicekEtiNplIndexColumns.Month) == (vasicekSearchMonth < 24 ? vasicekSearchMonth : 24))
                                                        .Field<double>(VasicekEtiNplIndexColumns.ScenarioFactor);

                //Pd group 1 to 9
                for (int pdGroup = 1; pdGroup < 10; pdGroup++)
                {
                    string pdGroupName = pdGroup.ToString();
                    double logOddsRatioMonthRankValue = logOddsRatio.AsEnumerable()
                                                        .FirstOrDefault(row => row.Field<int>(MonthlyLogOddsRatioColumns.Rank) == pdGroup
                                                                                && row.Field<int>(MonthlyLogOddsRatioColumns.Month) == month)
                                                        .Field<double>(MonthlyLogOddsRatioColumns.CreditRating);

                    DataRow dr = marginalPds.NewRow();
                    dr[MarginalLifetimeRedefaultPdColumns.PdGroup] = pdGroupName;
                    dr[MarginalLifetimeRedefaultPdColumns.ProjectionMonth] = month;
                    dr[MarginalLifetimeRedefaultPdColumns.Value] = logOddsRatioMonthRankValue * vasicekIndexMonthValue;

                    marginalPds.Rows.Add(dr);
                }

                //Pd Group Cons Stage 1
                DataRow pdGroup10 = marginalPds.NewRow();
                DataRow consStage1Row = marginalPds.NewRow();
                DataRow consStage2Row = marginalPds.NewRow();
                DataRow commStage1Row = marginalPds.NewRow();
                DataRow commStage2Row = marginalPds.NewRow();
                DataRow pdGroupExp = marginalPds.NewRow();

                pdGroup10 = GetPdGroupForConsAndComm(pdGroup10, nonInternalModelInput, "10", month, vasicekIndexMonthValue);
                consStage1Row = GetPdGroupForConsAndComm(consStage1Row, nonInternalModelInput, NonInternalModelInputColumns.ConsStage1, month, vasicekIndexMonthValue);
                consStage2Row = GetPdGroupForConsAndComm(consStage2Row, nonInternalModelInput, NonInternalModelInputColumns.ConsStage2, month, vasicekIndexMonthValue);
                commStage1Row = GetPdGroupForConsAndComm(commStage1Row, nonInternalModelInput, NonInternalModelInputColumns.CommStage1, month, vasicekIndexMonthValue);
                commStage2Row = GetPdGroupForConsAndComm(commStage2Row, nonInternalModelInput, NonInternalModelInputColumns.CommStage2, month, vasicekIndexMonthValue);
                pdGroupExp = GetPdGroupForConsAndComm(pdGroupExp, nonInternalModelInput, "EXP", month, vasicekIndexMonthValue);

                marginalPds.Rows.Add(pdGroup10);
                marginalPds.Rows.Add(consStage1Row);
                marginalPds.Rows.Add(consStage2Row);
                marginalPds.Rows.Add(commStage1Row);
                marginalPds.Rows.Add(commStage2Row);
                marginalPds.Rows.Add(pdGroupExp);
            }


            return marginalPds;
        }

        private DataRow GetPdGroupForConsAndComm(DataRow dr, DataTable nonInternalModelInput, string columnName, int month, double vasicekIndexMonthValue)
        {
            if (columnName == "10" || columnName == "EXP")
            {
                dr[MarginalLifetimeRedefaultPdColumns.PdGroup] = columnName;
                dr[MarginalLifetimeRedefaultPdColumns.ProjectionMonth] = month;
                dr[MarginalLifetimeRedefaultPdColumns.Value] = month == 1 ? 1 : 0;

                return dr;
            } 
            else
            {
                double consStage1 = nonInternalModelInput.AsEnumerable()
                                                        .FirstOrDefault(row => row.Field<double>(NonInternalModelInputColumns.Month) == month)
                                                        .Field<double>(columnName);

                dr[MarginalLifetimeRedefaultPdColumns.PdGroup] = columnName;
                dr[MarginalLifetimeRedefaultPdColumns.ProjectionMonth] = month;
                dr[MarginalLifetimeRedefaultPdColumns.Value] = consStage1 * vasicekIndexMonthValue;

                return dr;
            }
        }

        protected DataTable GetMonthlyLogOddsRatio()
        {
            return _pdInternalModelWorkings.ComputeMonthlyLogOddsRatio();
        }
        protected DataTable GetVasicekScenario()
        {
            return _vasicekWorkings.ComputeVasicekScenario();
        }
        protected DataTable GetNonInternalModelInputsData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetnonInternalModelInputData());
        }
    }
}
