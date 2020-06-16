using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclConfig
{
    public static class EclSettings
    {
        public const string OverrideCutOffDate = "Ecl.OverrideCutOffDate";
        public const string RequiredNoOfApprovals = "Ecl.RequiredNoOfApprovals";
        public const string PowerBiReportUrl = "Ecl.PowerBiReportUrl";
        public static class InputSourceUpload
        {
            public static class LoanBookSnapshot
            {
                public const string Wholesale = "Ecl.InputSourceUpload.LoanBookSnapshot.Wholesale";
                public const string Retail = "Ecl.InputSourceUpload.LoanBookSnapshot.Retail";
                public const string Obe = "Ecl.InputSourceUpload.LoanBookSnapshot.Obe";
            }

            public static class PaymentSchedule
            {
                public const string Wholesale = "Ecl.InputSourceUpload.PaymentSchedule.Wholesale";
                public const string Retail = "Ecl.InputSourceUpload.PaymentSchedule.Retail";
                public const string Obe = "Ecl.InputSourceUpload.PaymentSchedule.Obe";
            }

            public static class AssetBook
            {
                public const string Investment = "Ecl.InputSourceUpload.AssetBook.Investment";
            }
        }
    }
}
