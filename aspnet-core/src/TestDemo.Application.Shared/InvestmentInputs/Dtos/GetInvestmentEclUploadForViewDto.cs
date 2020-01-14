using System;

namespace TestDemo.InvestmentInputs.Dtos
{
    public class GetInvestmentEclUploadForViewDto
    {
		public InvestmentEclUploadDto EclUpload { get; set; }

		public string InvestmentEclReportingDate { get; set; }
        public DateTime? DateUploaded { get; set; }

        public string UploadedBy { get; set; }


    }
}