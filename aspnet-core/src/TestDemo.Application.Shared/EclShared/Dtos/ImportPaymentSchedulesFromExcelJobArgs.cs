using Abp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    [Serializable]
    public class ImportPaymentSchedulesFromExcelJobArgs
    {
        public Guid BinaryObjectId { get; set; }
        public FrameworkEnum Framework { get; set; }
        public Guid UploadSummaryId { get; set; }
        public UserIdentifier User { get; set; }
    }
}
