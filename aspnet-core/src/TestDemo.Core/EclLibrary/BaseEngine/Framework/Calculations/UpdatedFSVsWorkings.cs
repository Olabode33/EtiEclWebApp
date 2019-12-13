using EclEngine.BaseEclEngine.PdInput;
using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.Framework.Calculations
{
    public class UpdatedFSVsWorkings
    {
        public void Run()
        {
            DataTable dataTable = ComputeUpdatedFSV();
            string stop = "Ma te";
        }
        public DataTable ComputeUpdatedFSV()
        {
            DataTable updatedFSV = new DataTable();
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.ContractNo, typeof(string));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.Cash, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.CommercialProperty, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.Debenture, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.Inventory, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.PlantAndEquipment, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.Receivables, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.ResidentialProperty, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.Shares, typeof(double));
            updatedFSV.Columns.Add(TypeCollateralTypeColumns.Vehicle, typeof(double));

            DataTable fsv = GetCollateralTypeFSVResult();
            DataTable omv = GetCollateralTypeOMVResult();

            foreach (DataRow row in fsv.Rows)
            {
                DataRow omvRow = omv.AsEnumerable().FirstOrDefault(x => x.Field<string>(TypeCollateralTypeColumns.ContractNo) == row.Field<string>(TypeCollateralTypeColumns.ContractNo));

                DataRow newRow = updatedFSV.NewRow();
                newRow[TypeCollateralTypeColumns.ContractNo] = row.Field<string>(TypeCollateralTypeColumns.ContractNo);
                newRow[TypeCollateralTypeColumns.Cash] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.Cash), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.Cash),
                                                            TempEclData.CollateralHaircutApplied_Cash);
                newRow[TypeCollateralTypeColumns.CommercialProperty] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.CommercialProperty), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.CommercialProperty),
                                                            TempEclData.CollateralHaircutApplied_CommercialProperty);
                newRow[TypeCollateralTypeColumns.Debenture] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.Debenture), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.Debenture),
                                                            TempEclData.CollateralHaircutApplied_Debenture);
                newRow[TypeCollateralTypeColumns.Inventory] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.Inventory), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.Inventory),
                                                            TempEclData.CollateralHaircutApplied_Invertory);
                newRow[TypeCollateralTypeColumns.PlantAndEquipment] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.PlantAndEquipment), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.PlantAndEquipment),
                                                            TempEclData.CollateralHaircutApplied_PlantEquipment);
                newRow[TypeCollateralTypeColumns.Receivables] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.Receivables), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.Receivables),
                                                            TempEclData.CollateralHaircutApplied_Receivables);
                newRow[TypeCollateralTypeColumns.ResidentialProperty] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.ResidentialProperty), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.ResidentialProperty),
                                                            TempEclData.CollateralHaircutApplied_ResidentialProperty);
                newRow[TypeCollateralTypeColumns.Shares] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.Shares), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.Shares),
                                                            TempEclData.CollateralHaircutApplied_Shares);
                newRow[TypeCollateralTypeColumns.Vehicle] = ComputeCollateralValue(
                                                            row.Field<double>(TypeCollateralTypeColumns.Vehicle), 
                                                            omvRow.Field<double>(TypeCollateralTypeColumns.Vehicle),
                                                            TempEclData.CollateralHaircutApplied_Vehicle);

                updatedFSV.Rows.Add(newRow);
            }

            return updatedFSV;
        }

        protected double ComputeCollateralValue(double fsv, double omv, double haircut)
        {
            if (fsv > 0 && fsv < omv)
                return fsv;
            else
                return omv * (1 - haircut);
        }

        protected DataTable GetCollateralTypeFSVResult()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdCollateralTypeFSVData());
        }
        protected DataTable GetCollateralTypeOMVResult()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdCollateralTypeOMVData());
        }
    }
}
