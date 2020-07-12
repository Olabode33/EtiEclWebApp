using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using TestDemo.BatchEcls.BatchEclInput;
using TestDemo.ObeInputs;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.WholesaleInputs;

namespace TestDemo.Web.Controllers
{
    public class EclRawDataController : EclRawDataControllerBase
    {
        public EclRawDataController(
            IBinaryObjectManager binaryObjectManager,
            IRepository<RetailEclUpload, Guid> retailUploadSummaryRepository,
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadSummaryRepository,
            IRepository<ObeEclUpload, Guid> obeUploadSummaryRepository,
            IRepository<BatchEclUpload, Guid> batchUploadSummaryRepository,
            IBackgroundJobManager backgroundJobManager) 
            : base(binaryObjectManager, retailUploadSummaryRepository, wholesaleUploadSummaryRepository, obeUploadSummaryRepository, batchUploadSummaryRepository, backgroundJobManager)
        {
        }
    }
}
