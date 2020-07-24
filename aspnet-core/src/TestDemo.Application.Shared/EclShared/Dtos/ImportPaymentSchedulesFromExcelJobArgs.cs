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

    [Serializable]
    public class CopyAffiliateAssumptionJobArgs
    {
        public long FromAffiliateId { get; set; }
        public long ToAffiliateId { get; set; }
        public UserIdentifier User { get; set; }
    }

    [Serializable]
    public class ApplyAffiliateAssumptionJobArgs
    {
        public long FromAffiliateId { get; set; }
        public long ToAffiliateId { get; set; }
        public UserIdentifier User { get; set; }
        public AssumptionTypeEnum Type { get; set; }
        public FrameworkEnum Framework { get; set; }
    }

    [Serializable]
    public class CreateSubEclJobArgs
    {
        public FrameworkEnum Framework { get; set; }
        public Guid BatchId { get; set; }
        public long UserId { get; set; }
    }


    [Serializable]
    public class EraserJobArgs
    {
        public Guid GuidId { get; set; }
        public TrackTypeEnum EraseType { get; set; }
        public int IntId { get; set; }
    }

}
