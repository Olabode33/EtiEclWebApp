using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD_Inputs_Automation
{
    class Helper
    {
        public static string connection_string = @"Data Source=TAROKODARE001\MSSQLSERVER_NEW;Initial Catalog=IFRS9_AUTOMATION_DB;Integrated Security=True;";
        public static DataTable ReadExcel(string fileName)
        {
            DataTable dt = new DataTable();
            using (XLWorkbook wb = new XLWorkbook(fileName))
            {

                var ws = wb.Worksheets.Worksheet(1);
                var range = ws.RangeUsed();
                var Columns = ws.Columns();
                var rows = ws.Rows();
                bool firstRow = true;
                foreach (IXLRow row in ws.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;

                        for (int j = 1; j < dt.Columns.Count + 1; j++)
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = row.Cell(j).Value;
                            if (dt.Columns.Count - 1 > i)
                            {
                                i++;
                            }
                        }

                    }

                }
            }
            return dt;
        }

        public static DataTable RevisedTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CONTRACT_NO", typeof(string));
            dt.Columns.Add("SEGMENT", typeof(string));
            dt.Columns.Add("CURRENCY", typeof(string));
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

        public static DataTable ReadData(string query)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"" + connection_string + "";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                SqlDataReader odr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(odr);
                return dt;
            }
            catch (SqlException ex)
            {
                //WriteLog(ex);//writelog

                return null;
            }
            catch (Exception ex)
            {
                //WriteLog(ex);//writelog
                return null;
            }
            finally
            {
                con.Close();
            }
        }


    }
}
