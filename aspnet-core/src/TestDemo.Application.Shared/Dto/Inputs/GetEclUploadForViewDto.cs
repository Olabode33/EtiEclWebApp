using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto.Inputs
{
    public class GetEclUploadForViewDto
    {
        public EclUploadDto EclUpload { get; set; }
        public string EclReportingDate { get; set; }
        public DateTime? DateUploaded { get; set; }
        public string UploadedBy { get; set; }
    }

    public class GetBatchEclUploadForViewDto
    {
        public BatchEclUploadDto EclUpload { get; set; }
        public string EclReportingDate { get; set; }
        public DateTime? DateUploaded { get; set; }
        public string UploadedBy { get; set; }
    }
}
