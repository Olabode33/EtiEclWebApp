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
