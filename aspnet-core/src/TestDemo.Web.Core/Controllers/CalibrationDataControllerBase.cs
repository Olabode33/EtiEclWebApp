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
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Importing;
using TestDemo.Storage;

namespace TestDemo.Web.Controllers
{
    public abstract class CalibrationDataControllerBase : TestDemoControllerBase
    {
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;

        protected CalibrationDataControllerBase(
            IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
        }

        [HttpPost]
        public async Task<JsonResult> ImportBehaviouralTermFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["calibrationId"];

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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportCalibrationBehaviouralFromExcelJob, ImportCalibrationDataFromExcelJobArgs>(new ImportCalibrationDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    CalibrationId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                });

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpPost]
        public async Task<JsonResult> ImportCcfSummaryFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["calibrationId"];

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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportCalibrationCcfSummaryFromExcelJob, ImportCalibrationDataFromExcelJobArgs>(new ImportCalibrationDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    CalibrationId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                });

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpPost]
        public async Task<JsonResult> ImportLgdHaircutFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["calibrationId"];

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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportCalibrationLgdHaircutFromExcelJob, ImportCalibrationDataFromExcelJobArgs>(new ImportCalibrationDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    CalibrationId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                });

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpPost]
        public async Task<JsonResult> ImportLgdRecoveryRateFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["calibrationId"];

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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportCalibrationLgdRecoveryRateFromExcelJob, ImportCalibrationDataFromExcelJobArgs>(new ImportCalibrationDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    CalibrationId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                });

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpPost]
        public async Task<JsonResult> ImportPdCrDrFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["calibrationId"];

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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportCalibrationPdCrDrFromExcelJob, ImportCalibrationDataFromExcelJobArgs>(new ImportCalibrationDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    CalibrationId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                });

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpPost]
        public async Task<JsonResult> ImportPdCommsConsFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["calibrationId"];

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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportCalibrationPdCommsConsFromExcelJob, ImportCalibrationDataFromExcelJobArgs>(new ImportCalibrationDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    CalibrationId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                });

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }


        [HttpPost]
        public async Task<JsonResult> ImportMacroAnalysisFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();
                var uploadSummaryId = Request.Form["calibrationId"];

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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportMacroAnalysisDataFromExcelJob, ImportMacroAnalysisDataFromExcelJobArgs>(new ImportMacroAnalysisDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    MacroId = Convert.ToInt32(uploadSummaryId),
                    User = AbpSession.ToUserIdentifier(),
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
