using System;

namespace TestDemo.IVModels.Dtos
{
    public class GetHoldCoRegisterForViewDto
    {
		public HoldCoRegisterDto HoldCoRegister { get; set; }

        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}