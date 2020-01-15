using Excel.FinancialFunctions;
using MathNet.Numerics;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace EclEngine.Utils
{
    public static class ExcelFormulaUtil
    {
        //private static WorksheetFunction _excelWorksheetFunctions = new Application().WorksheetFunction;

        public static double YearFrac(DateTime startDate, DateTime endDate, DayCountBasis dayCountBasis = DayCountBasis.UsPsa30_360)
        {
            if (startDate == endDate)
                return 0;
            if (startDate < endDate)
                return Financial.YearFrac(startDate, endDate, dayCountBasis);
            else
                return Financial.YearFrac(endDate, startDate, dayCountBasis);
        }

        public static double PMT(double rate, double nper, double pv, double fv)
        {
            return Financial.Pmt(rate, nper, pv, fv, PaymentDue.EndOfPeriod);
        }

        public static DateTime EOMonth(DateTime? date, int months = 0)
        {
            DateTime eoMonth =  new DateTime(date.Value.Year, date.Value.Month, DateTime.DaysInMonth(date.Value.Year, date.Value.Month));
            return eoMonth.AddMonths(months);
        }

        public static double NormSDist(double p, bool cummulative = true)
        {
            return ExcelFunctions.NormSDist(p);
            //return _excelWorksheetFunctions.Norm_S_Dist(p, cummulative);
        }

        public static double NormSInv(double p)
        {
            return ExcelFunctions.NormSInv(p);
            //return _excelWorksheetFunctions.Norm_S_Inv(p);
        }

        public static double SumProduct(object arg1)
        {
            return 0;
        }

        public static double SumProduct(double[] arg1, double[] arg2)
        {
            double result = 0;
            for (int i = 0; i < arg1.Length; i++)
                result += arg1[i] * arg2[i];
            return result;
        }

        public static double CalculateStdDev(IEnumerable<double> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                double avg = values.Average();
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return ret;
        }
    }
}
