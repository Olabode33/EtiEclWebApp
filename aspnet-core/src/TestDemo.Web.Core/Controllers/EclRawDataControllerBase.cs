using Abp.BackgroundJobs;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.EclShared;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Importing;
using TestDemo.Storage;

namespace TestDemo.Web.Controllers
{
    public abstract class EclRawDataControllerBase: TestDemoControllerBase
    {
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;

        protected EclRawDataControllerBase(
            IBinaryObjectManager binaryObjectManager, 
            IBackgroundJobManager backgroundJobManager)
        {
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
        }

        [HttpPost]
        public async Task<JsonResult> ImportPaymentScheduleFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["uploadSummaryId"];
                var framework = Request.Form["framework"].First();

                if (file == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                if (file.Length > 1048576 * 100) //100 MB
                {
                    throw new UserFriendlyException(L("File_SizeLimit_Error"));
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var tenantId = AbpSession.TenantId;
                var fileObject = new BinaryObject(tenantId, fileBytes);

                await BinaryObjectManager.SaveAsync(fileObject);

                await BackgroundJobManager.EnqueueAsync<ImportPaymentScheduleFromExcelJob, ImportPaymentSchedulesFromExcelJobArgs>(new ImportPaymentSchedulesFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    Framework = (FrameworkEnum)Convert.ToInt32(framework),
                    UploadSummaryId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                });

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
    }
}
