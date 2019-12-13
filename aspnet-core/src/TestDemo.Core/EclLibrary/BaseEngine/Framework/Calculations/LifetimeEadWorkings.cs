using EclEngine.BaseEclEngine.PdInput;
using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.Framework.Calculations
{
    public class LifetimeEadWorkings
    {
        protected IrFactorWorkings _irFactorWorkings = new IrFactorWorkings();
        protected PdInputResult _pdInputResult = new PdInputResult();

        public void Run()
        {
            DataTable dataTable = ComputeLifetimeEad();
            string stop = "Ma te";
        }
        public DataTable ComputeLifetimeEad()
        {
            DataTable lifetimeEad = new DataTable();
            lifetimeEad.Columns.Add(LifetimeEadColumns.ContractId, typeof(string));
            lifetimeEad.Columns.Add(LifetimeEadColumns.CirIndex, typeof(int));
            lifetimeEad.Columns.Add(LifetimeEadColumns.ProductType, typeof(string));
            lifetimeEad.Columns.Add(LifetimeEadColumns.MonthsPastDue, typeof(long));
            lifetimeEad.Columns.Add(LifetimeEadColumns.ProjectionMonth, typeof(int));
            lifetimeEad.Columns.Add(LifetimeEadColumns.ProjectionValue, typeof(double));

            DataTable eadInputs = GetTempEadInputData();
            DataTable sircInputs = GetSircInputResult();
            DataTable contractData = GetTempContractData();
            DataTable marginalAccumulationFactor = GetMarginalAccumulationFactorResult();

            foreach (DataRow row in eadInputs.Rows)
            {
                string contractId = row.Field<string>(EadInputsColumns.ContractId);
                int cirIndex = marginalAccumulationFactor.AsEnumerable()
                                                         .FirstOrDefault(x => x.Field<string>(IrFactorColumns.CirGroup) == row.Field<string>(EadInputsColumns.CirGroup))
                                                         .Field<int>(IrFactorColumns.GroupRank);
                string productType = contractData.AsEnumerable()
                                                         .FirstOrDefault(x => x.Field<string>(TempLgdContractDataColumns.ContractNo) == contractId)
                                                         .Field<string>(TempLgdContractDataColumns.ProductType);
                DataRow sirc = sircInputs.AsEnumerable().FirstOrDefault(x => x.Field<string>(SicrInputsColumns.ContractId) == contractId);
                long? daysPastDue = sirc == null ? 0 : sirc.Field<long?>(SicrInputsColumns.DaysPastDue);

                DataRow month0Record = lifetimeEad.NewRow();
                month0Record[LifetimeEadColumns.ContractId] = contractId;
                month0Record[LifetimeEadColumns.CirIndex] = cirIndex;
                month0Record[LifetimeEadColumns.ProductType] = productType;
                month0Record[LifetimeEadColumns.MonthsPastDue] = daysPastDue == null ? 0 : daysPastDue / 30;
                month0Record[LifetimeEadColumns.ProjectionMonth] = 0;
                month0Record[LifetimeEadColumns.ProjectionValue] = row.Field<double>("0");
                lifetimeEad.Rows.Add(month0Record);

                for (int month = 1; month < TempEclData.TempExcelVariable_LIM_MONTH; month++)
                {

                    if (contractId == "12017325")
                    {
                        string stop = "Stop";
                    }

                    DataRow newRecord = lifetimeEad.NewRow();
                    newRecord[LifetimeEadColumns.ContractId] = contractId;
                    newRecord[LifetimeEadColumns.CirIndex] = cirIndex;
                    newRecord[LifetimeEadColumns.ProductType] = productType;
                    newRecord[LifetimeEadColumns.MonthsPastDue] = daysPastDue / 30;
                    newRecord[LifetimeEadColumns.ProjectionMonth] = month;
                    newRecord[LifetimeEadColumns.ProjectionValue] = ComputeLifetimeValue(row, marginalAccumulationFactor, (long)daysPastDue / 30, month, cirIndex, productType);
                    lifetimeEad.Rows.Add(newRecord);
                }
            }

            return lifetimeEad;
        }
        protected double ComputeLifetimeValue(DataRow eadInputRecord, DataTable accumlationFactor, long monthsPastDue, int months, int cirIndex, string productType)
        {
            if (productType.ToLower() != "loan" && productType.ToLower() != "lease" && productType.ToLower() != "mortgage")
                return eadInputRecord.Field<double>(months.ToString());
            else
            {
                double eadOffset = ComputeEadOffest(eadInputRecord, months, monthsPastDue);
                double multiplierValue = ComputeMultiplierValue(accumlationFactor, monthsPastDue, cirIndex, months);

                return eadOffset * multiplierValue;
            }

        }
        protected double ComputeEadOffest(DataRow eadInputRecord, int month, long monthsPastDue)
        {
            int temp1 = TempEclData.TempExcelVariable_MPD_DEFAULT_CRITERIA - (int)monthsPastDue;
            int temp2 = month - Math.Max(temp1, 0);
            int offestMonth = Math.Max(temp2, 0);


            return eadInputRecord.Field<double>(offestMonth.ToString());
        }
        protected double ComputeMultiplierValue(DataTable accumlationFactor, long monthsPastDue, int cirIndex, int month)
        {
            int temp1 = Math.Min(TempEclData.TempExcelVariable_MPD_DEFAULT_CRITERIA - (int)monthsPastDue, month);
            int temp2 = Math.Abs(Math.Max(temp1, 1));
            int tempRow = cirIndex;
            int tempColumn = month;
            int tempHeight = temp2;
            var offsetvalues = accumlationFactor.AsEnumerable()
                                                .Where(x => x.Field<int>(IrFactorColumns.GroupRank) == cirIndex 
                                                         && (x.Field<int>(IrFactorColumns.ProjectionMonth) > 0 && x.Field<int>(IrFactorColumns.ProjectionMonth) <= temp2))
                                                .Select(x =>
                                                {
                                                    return x.Field<double>(IrFactorColumns.ProjectionValue);
                                                }).ToArray();
            var product = offsetvalues.Aggregate(1.0, (acc, x) => acc * x);
            return monthsPastDue >= TempEclData.TempExcelVariable_MPD_DEFAULT_CRITERIA ? 1 : product;
        }
        protected DataTable GetTempEadInputData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempEadInputsData());
        }
        protected DataTable GetSircInputResult()
        {
            return JsonUtil.DeserializeToDatatable(_pdInputResult.GetSicrInputs());
        }
        protected DataTable GetTempContractData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdContractDataData());
        }
        protected DataTable GetMarginalAccumulationFactorResult()
        {
            return _irFactorWorkings.ComputeMarginalAccumulationFactor();
        }
    }
}
