using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleLGDAccountData")]
    [Audited]
    public class WholesaleLgdContractData : EclLgdContractDataBase
	{
		public virtual Guid WholesaleEclId { get; set; }
    }
}