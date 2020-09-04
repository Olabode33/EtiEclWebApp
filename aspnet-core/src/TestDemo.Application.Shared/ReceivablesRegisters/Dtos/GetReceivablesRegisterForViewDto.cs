using System;

namespace TestDemo.ReceivablesRegisters.Dtos
{
    public class GetReceivablesRegisterForViewDto
    {
		public ReceivablesRegisterDto ReceivablesRegister { get; set; }

        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}