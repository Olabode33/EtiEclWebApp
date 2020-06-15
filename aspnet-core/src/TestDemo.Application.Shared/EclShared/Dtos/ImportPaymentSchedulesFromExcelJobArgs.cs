using Abp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    [Serializable]
    public class ImportEclDataFromExcelJobArgs
    {
        public Guid BinaryObjectId { get; set; }
        public FrameworkEnum Framework { get; set; }
        public Guid UploadSummaryId { get; set; }
        public UserIdentifier User { get; set; }
    }

    [Serializable]
    public class ImportCalibrationDataFromExcelJobArgs
    {
        public Guid BinaryObjectId { get; set; }
        public Guid CalibrationId { get; set; }
        public UserIdentifier User { get; set; }
    }

    [Serializable]
    public class ImportMacroAnalysisDataFromExcelJobArgs
    {
        public Guid BinaryObjectId { get; set; }
        public int MacroId { get; set; }
        public UserIdentifier User { get; set; }
    }

    [Serializable]
    public class ImportAssumptionDataFromExcelJobArgs
    {
        public Guid BinaryObjectId { get; set; }
        public long AffiliateId { get; set; }
        public FrameworkEnum Framework { get; set; }
        public UserIdentifier User { get; set; }
    }
}
