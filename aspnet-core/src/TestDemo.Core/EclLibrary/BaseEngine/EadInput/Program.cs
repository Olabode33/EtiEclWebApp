using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD_Inputs_Automation
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////
            ///read the raw data 
            ///////
            ///////
            ///
            string rawDatasample = @"C:\Users\tarokodare001\Documents\WORK\ECOBANK\Model Excels\Nigeria\Raw Data\Raw data - Copy.xlsx";
            //@"C: \Users\tarokodare001\Documents\WORK\ECOBANK\My Sample Excel\EAD\Ead Inputs.xlsx";

            DataTable rawData = Helper.ReadExcel(rawDatasample);

            EAD_Inputs_Calculation ead_input = new EAD_Inputs_Calculation();
            ead_input.EAD_Inputs_START(rawData);
            
        }
    }
}
