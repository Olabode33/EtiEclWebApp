using System;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetRetailEclUploadForViewDto
    {
		public RetailEclUploadDto EclUpload { get; set; }

		public string RetailEclTenantId { get; set;}

        public DateTime? DateUploaded { get; set; }

        public string UploadedBy { get; set; }
    }
}