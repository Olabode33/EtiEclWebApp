using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetRecordForOverrideInputDto
    {
        public Guid EclId { get; set; }
        public string searchTerm { get; set; }
    }
}
