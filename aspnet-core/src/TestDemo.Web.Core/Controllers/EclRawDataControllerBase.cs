using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
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
using TestDemo.BatchEcls.BatchEclInput;
using TestDemo.EclShared;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Importing;
using TestDemo.ObeInputs;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.WholesaleInputs;

namespace TestDemo.Web.Controllers
{
    public abstract class EclRawDataControllerBase: TestDemoControllerBase
    {
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;
        protected readonly IRepository<RetailEclUpload, Guid> _retailUploadSummaryRepository;
        protected readonly IRepository<WholesaleEclUpload, Guid> _wholesaleUploadSummaryRepository;
        protected readonly IRepository<ObeEclUpload, Guid> _obeUploadSummaryRepository;
        protected readonly IRepository<BatchEclUpload, Guid> _batchUploadSummaryRepository;

        protected EclRawDataControllerBase(
            IBinaryObjectManager binaryObjectManager,
            IRepository<RetailEclUpload, Guid> retailUploadSummaryRepository,
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadSummaryRepository,
            IRepository<ObeEclUpload, Guid> obeUploadSummaryRepository,
            IRepository<BatchEclUpload, Guid> batchUploadSummaryRepository,
            IBackgroundJobManager backgroundJobManager)
        {
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
            _retailUploadSummaryRepository = retailUploadSummaryRepository;
            _wholesaleUploadSummaryRepository = wholesaleUploadSummaryRepository;
            _obeUploadSummaryRepository = obeUploadSummaryRepository;
            _batchUploadSummaryRepository = batchUploadSummaryRepository;
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
                await CurrentUnitOfWork.SaveChangesAsync();

                await UpdateSummaryTableToFileUploaded(Guid.Parse(uploadSummaryId.ToString()), (FrameworkEnum)Convert.ToInt32(framework));

                await BackgroundJobManager.EnqueueAsync<ImportPaymentScheduleFromExcelJob , ImportEclDataFromExcelJobArgs>(new ImportEclDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    Framework = (FrameworkEnum)Convert.ToInt32(framework),
                    UploadSummaryId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                }, priority: BackgroundJobPriority.High);


                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }


        [HttpPost]
        public async Task<JsonResult> ImportLoanbookFromExcel()
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
                await CurrentUnitOfWork.SaveChangesAsync();

                await UpdateSummaryTableToFileUploaded(Guid.Parse(uploadSummaryId.ToString()), (FrameworkEnum)Convert.ToInt32(framework));

                await BackgroundJobManager.EnqueueAsync<ImportLoanbookFromExcelJob, ImportEclDataFromExcelJobArgs>(new ImportEclDataFromExcelJobArgs
                {
                    BinaryObjectId = fileObject.Id,
                    Framework = (FrameworkEnum)Convert.ToInt32(framework),
                    UploadSummaryId = Guid.Parse(uploadSummaryId.ToString()),
                    User = AbpSession.ToUserIdentifier()
                }, priority: BackgroundJobPriority.High);


                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }


        [HttpPost]
        public async Task<JsonResult> ImportAssetFromExcel()
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
                await CurrentUnitOfWork.SaveChangesAsync();

                await BackgroundJobManager.EnqueueAsync<ImportAssetBookFromExcelJob, ImportEclDataFromExcelJobArgs>(new ImportEclDataFromExcelJobArgs
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

        private async Task UpdateSummaryTableToFileUploaded(Guid updateSummaryId, FrameworkEnum framework)
        {
            switch (framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = await _retailUploadSummaryRepository.FirstOrDefaultAsync((Guid)updateSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.FileUploaded = true;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = await _wholesaleUploadSummaryRepository.FirstOrDefaultAsync((Guid)updateSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.FileUploaded = true;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = await _obeUploadSummaryRepository.FirstOrDefaultAsync((Guid)updateSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.FileUploaded = true;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = await _batchUploadSummaryRepository.FirstOrDefaultAsync((Guid)updateSummaryId);
                    if (bSummary != null)
                    {
                        bSummary.FileUploaded = true;
                        _batchUploadSummaryRepository.Update(bSummary);
                    }
                    break;
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}
