using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration
{
	[Table("CalibrationRunEadBehaviouralTerms")]
    [Audited]
    public class CalibrationEadBehaviouralTerm : CalibrationRunBase
    {
		
    }
}