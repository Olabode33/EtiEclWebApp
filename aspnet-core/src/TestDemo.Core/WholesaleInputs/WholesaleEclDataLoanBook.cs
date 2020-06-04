using TestDemo.WholesaleInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.WholesaleInputs
{
	[Table("WholesaleEclDataLoanBooks")]
    public class WholesaleEclDataLoanBook : EclDataLoanBookBase
    {
		public virtual Guid WholesaleEclUploadId { get; set; }
    }
}