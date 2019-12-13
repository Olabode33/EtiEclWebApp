using EclEngine.BaseEclEngine.PdInput;
using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.Framework.Calculations
{
    public class IrFactorWorkings
    {
        private const string EIR_TYPE = "EIR";
        private const string CIR_TYPE = "CIR";
        public void Run()
        {
            DataTable dataTable = ComputeCummulativeDiscountFactor();
            string stop = "Ma te";
        }

        public DataTable ComputeCummulativeDiscountFactor()
        {
            DataTable cummulativeDiscountFactor = new DataTable();
            cummulativeDiscountFactor.Columns.Add(IrFactorColumns.EirGroup, typeof(string));
            cummulativeDiscountFactor.Columns.Add(IrFactorColumns.GroupRank, typeof(int));
            cummulativeDiscountFactor.Columns.Add(IrFactorColumns.ProjectionMonth, typeof(int));
            cummulativeDiscountFactor.Columns.Add(IrFactorColumns.ProjectionValue, typeof(double));

            DataTable marginalDiscountFactor = ComputeMarginalDiscountFactor();

            foreach (DataRow row in marginalDiscountFactor.Rows)
            {

                DataRow dataRow = cummulativeDiscountFactor.NewRow();
                dataRow[IrFactorColumns.EirGroup] = row.Field<string>(IrFactorColumns.EirGroup);
                dataRow[IrFactorColumns.GroupRank] = row.Field<int>(IrFactorColumns.GroupRank);
                dataRow[IrFactorColumns.ProjectionMonth] = row.Field<int>(IrFactorColumns.ProjectionMonth);
                dataRow[IrFactorColumns.ProjectionValue] = ComputeCummulativeProjectionValue(marginalDiscountFactor, row.Field<string>(IrFactorColumns.EirGroup), row.Field<int>(IrFactorColumns.ProjectionMonth));

                cummulativeDiscountFactor.Rows.Add(dataRow);
            }

            return cummulativeDiscountFactor;
        }
        
        protected double ComputeCummulativeProjectionValue(DataTable marginalDiscountFactor, string eirGroup, int month)
        {
            var range = marginalDiscountFactor.AsEnumerable()
                            .Where(x => x.Field<string>(IrFactorColumns.EirGroup) == eirGroup
                                                && x.Field<int>(IrFactorColumns.ProjectionMonth) <= month)
                            .Select(x => {
                                return x.Field<double>(IrFactorColumns.ProjectionValue);
                            }).ToArray();
            var aggr = range.Aggregate(1.0, (acc, x) => acc * x);

            return aggr;
        }
        public DataTable ComputeMarginalDiscountFactor()
        {
            DataTable marginalDiscountFactor = new DataTable();
            marginalDiscountFactor.Columns.Add(IrFactorColumns.EirGroup, typeof(string));
            marginalDiscountFactor.Columns.Add(IrFactorColumns.GroupRank, typeof(int));
            marginalDiscountFactor.Columns.Add(IrFactorColumns.ProjectionMonth, typeof(int));
            marginalDiscountFactor.Columns.Add(IrFactorColumns.ProjectionValue, typeof(double));

            DataTable eirProjection = GetEirProjectionData();

            int rank = 1;
            foreach (DataRow row in eirProjection.Rows)
            {
                string eirGroup = row.Field<string>(EadInputsColumns.EirProjectionGroups);

                DataRow month0Record = marginalDiscountFactor.NewRow();
                month0Record[IrFactorColumns.EirGroup] = eirGroup;
                month0Record[IrFactorColumns.GroupRank] = rank;
                month0Record[IrFactorColumns.ProjectionMonth] = 0;
                month0Record[IrFactorColumns.ProjectionValue] = 1.0;
                marginalDiscountFactor.Rows.Add(month0Record);

                for (int month = 1; month < TempEclData.MaxIrFactorProjectionMonths; month++)
                {
                    double prevMonthValue = marginalDiscountFactor.AsEnumerable()
                                                                      .FirstOrDefault(x => x.Field<string>(IrFactorColumns.EirGroup) == eirGroup
                                                                                        && x.Field<int>(IrFactorColumns.ProjectionMonth) == month - 1)
                                                                      .Field<double>(IrFactorColumns.ProjectionValue);

                    DataRow newIrFactorRecord = marginalDiscountFactor.NewRow();
                    newIrFactorRecord[IrFactorColumns.EirGroup] = eirGroup;
                    newIrFactorRecord[IrFactorColumns.GroupRank] = rank;
                    newIrFactorRecord[IrFactorColumns.ProjectionMonth] = month;
                    newIrFactorRecord[IrFactorColumns.ProjectionValue] = ComputeProjectionValue(row, month, prevMonthValue, EIR_TYPE);
                    marginalDiscountFactor.Rows.Add(newIrFactorRecord);
                }

                rank += 1;
            }

            return marginalDiscountFactor;
        }
        public DataTable ComputeMarginalAccumulationFactor()
        {
            DataTable marginalAccumulativeFactor = new DataTable();
            marginalAccumulativeFactor.Columns.Add(IrFactorColumns.CirGroup, typeof(string));
            marginalAccumulativeFactor.Columns.Add(IrFactorColumns.GroupRank, typeof(int));
            marginalAccumulativeFactor.Columns.Add(IrFactorColumns.ProjectionMonth, typeof(int));
            marginalAccumulativeFactor.Columns.Add(IrFactorColumns.ProjectionValue, typeof(double));

            DataTable cirProjection = GetCirProjectionData();
            int rank = 1;
            foreach(DataRow row in cirProjection.Rows)
            {
                string cirGroup = row.Field<string>(EadInputsColumns.CirProjectionGroups);

                DataRow month0Record = marginalAccumulativeFactor.NewRow();
                month0Record[IrFactorColumns.CirGroup] = cirGroup;
                month0Record[IrFactorColumns.GroupRank] = rank;
                month0Record[IrFactorColumns.ProjectionMonth] = 0;
                month0Record[IrFactorColumns.ProjectionValue] = 1.0;
                marginalAccumulativeFactor.Rows.Add(month0Record);

                for (int month = 1; month < TempEclData.MaxIrFactorProjectionMonths; month++)
                {
                    double prevMonthValue = marginalAccumulativeFactor.AsEnumerable()
                                                                      .FirstOrDefault(x => x.Field<string>(IrFactorColumns.CirGroup) == cirGroup
                                                                                        && x.Field<int>(IrFactorColumns.ProjectionMonth) == month - 1)
                                                                      .Field<double>(IrFactorColumns.ProjectionValue);

                    DataRow newIrFactorRecord = marginalAccumulativeFactor.NewRow();
                    newIrFactorRecord[IrFactorColumns.CirGroup] = cirGroup;
                    newIrFactorRecord[IrFactorColumns.GroupRank] = rank;
                    newIrFactorRecord[IrFactorColumns.ProjectionMonth] = month;
                    newIrFactorRecord[IrFactorColumns.ProjectionValue] = ComputeProjectionValue(row, month, prevMonthValue);
                    marginalAccumulativeFactor.Rows.Add(newIrFactorRecord);
                }
                rank += 1;
            }

            return marginalAccumulativeFactor;
        }

        protected double ComputeProjectionValue(DataRow projectionValue, int month, double prevValue, string type = CIR_TYPE)
        {
            if (month > TempEclData.TempExcelVariable_LIM_MONTH)
            {
                return prevValue;
            }
            else
            {
                var test1 = 1.0 + projectionValue.Field<double>(month.ToString());
                double test2 = (type == EIR_TYPE ? (double)-1.0 : (double)1.0 / (double)12.0);
                return Math.Pow(1.0 + projectionValue.Field<double>(month.ToString()), ( type == EIR_TYPE ? -1.0 : 1.0)/12.0);
            }
        }

        protected DataTable GetEirProjectionData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempEirProjectionsData());
        }
        protected DataTable GetCirProjectionData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempCirProjectionData());
        }
    }
}
