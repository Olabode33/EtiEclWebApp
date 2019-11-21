using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD_Inputs_Automation
{
    class Tables
    {
        public static DataTable RevisedTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(ColumnNames.contract_no, typeof(string));
            dt.Columns.Add(ColumnNames.segment, typeof(string));
            dt.Columns.Add(ColumnNames.currency, typeof(string));
            dt.Columns.Add("PRODUCT_TYPE", typeof(string));
            dt.Columns.Add("CREDIT_LIMIT_LCY", typeof(string));
            dt.Columns.Add("ORIGINAL_BALANCE_LCY", typeof(string));
            dt.Columns.Add("OUTSTANDING_BALANCE_LCY", typeof(string));
            dt.Columns.Add("CONTRACT_START_DATE", typeof(string));
            dt.Columns.Add("CONTRACT_END_DATE", typeof(string));
            dt.Columns.Add("RESTRUCTURE_INDICATOR", typeof(string));
            dt.Columns.Add("RESTRUCTURE_START_DATE", typeof(string));
            dt.Columns.Add("RESTRUCTURE_END_DATE", typeof(string));
            dt.Columns.Add("IPT_O_PERIOD", typeof(string));
            dt.Columns.Add("PRINCIPAL_PAYMENT_STRUCTURE", typeof(string));
            dt.Columns.Add("INTEREST_PAYMENT_STRUCTURE", typeof(string));
            dt.Columns.Add("INTEREST_RATE_TYPE", typeof(string));
            dt.Columns.Add("BASE_RATE", typeof(string));
            dt.Columns.Add("ORIGINATION_CONTRACTUAL_INTEREST_RATE", typeof(string));
            dt.Columns.Add("INTRODUCTORY_PERIOD", typeof(string));
            dt.Columns.Add("POST_IP_CONTRACTUAL_INTEREST_RATE", typeof(string));
            dt.Columns.Add("CURRENT_CONTRACTUAL_INTEREST_RATE", typeof(string));
            dt.Columns.Add("EIR", typeof(string));
            return dt;
        }

        public static DataTable LifeTimeEADs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(ColumnNames.start_date, typeof(string));
            dt.Columns.Add(ColumnNames.end_date, typeof(string));
            dt.Columns.Add(ColumnNames.remaining_ip, typeof(string));
            dt.Columns.Add(ColumnNames.revised_base, typeof(string));
            dt.Columns.Add(ColumnNames.cir_premium, typeof(string));
            dt.Columns.Add(ColumnNames.eir_premium, typeof(string));
            dt.Columns.Add(ColumnNames.cir_base_premium, typeof(string));
            dt.Columns.Add(ColumnNames.eir_base_premium, typeof(string));
            dt.Columns.Add(ColumnNames.mths_in_force, typeof(string));
            dt.Columns.Add(ColumnNames.rem_interest_moritorium, typeof(string));
            dt.Columns.Add(ColumnNames.mths_to_expiry, typeof(string));
            dt.Columns.Add(ColumnNames.interest_divisor, typeof(string));
            dt.Columns.Add(ColumnNames.first_interest_month, typeof(string));
            return dt;
        }

        public static DataTable DynamicTable(double noOfCloumns)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(ColumnNames.key, typeof(string));
            double columns = 0;
            while (columns <= noOfCloumns)
            {//add new columns
                dt.Columns.Add(columns.ToString(), typeof(string));
                columns++;
            }
            return dt;
        }
    }
}
