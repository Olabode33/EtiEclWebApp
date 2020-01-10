using System;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclEadInputAssumptionDto : CreateOrEditEclEadAssumptionBase
    {
        public Guid RetailEclId { get; set; }
		 
    }
}