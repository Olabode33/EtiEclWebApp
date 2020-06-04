using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.WholesaleInputs
{
	[Table("WholesaleEclUploads")]
    [Audited]
    public class WholesaleEclUpload : EclUploadBase
    {
		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}