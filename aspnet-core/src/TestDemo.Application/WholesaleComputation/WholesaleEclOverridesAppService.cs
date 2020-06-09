using TestDemo.WholesaleInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.EclShared;
using TestDemo.Dto.Overrides;
using TestDemo.InvestmentComputation.Dtos;
using Abp.UI;
using TestDemo.EclConfig;
using Abp.Configuration;

namespace TestDemo.WholesaleComputation
{
    [AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides)]
    public class WholesaleEclOverridesAppService : TestDemoAppServiceBase, IWholesaleEclOverridesAppService
    {
        private readonly IRepository<WholesaleEclOverride, Guid> _wholesaleEclOverrideRepository;
        private readonly IRepository<WholesaleEclDataLoanBook, Guid> _lookup_wholesaleEclDataLoanBookRepository;
        private readonly IRepository<WholesaleEclOverrideApproval, Guid> _lookup_wholesaleEclOverrideApprovalRepository;
        private readonly IRepository<WholesaleEclFrameworkFinal, Guid> _lookup_wholesaleEclFrameworkRepository;


        public WholesaleEclOverridesAppService(
            IRepository<WholesaleEclOverride, Guid> wholesaleEclOverrideRepository, 
            IRepository<WholesaleEclDataLoanBook, Guid> lookup_wholesaleEclDataLoanBookRepository,
            IRepository<WholesaleEclFrameworkFinal, Guid> lookup_wholesaleEclFrameworkRepository,
            IRepository<WholesaleEclOverrideApproval, Guid> lookup_wholesaleEclOverrideApprovalRepository)
        {
            _wholesaleEclOverrideRepository = wholesaleEclOverrideRepository;
            _lookup_wholesaleEclDataLoanBookRepository = lookup_wholesaleEclDataLoanBookRepository;
            _lookup_wholesaleEclOverrideApprovalRepository = lookup_wholesaleEclOverrideApprovalRepository;
            _lookup_wholesaleEclFrameworkRepository = lookup_wholesaleEclFrameworkRepository;
        }

        public async Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input)
        {

            var filteredWholesaleEclOverrides = _wholesaleEclOverrideRepository.GetAll()
                        .Where(x => x.WholesaleEclId == input.EclId)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ContractId.Contains(input.Filter));

            var filteredFrameworkResult = _lookup_wholesaleEclFrameworkRepository.GetAll()
                        .Where(x => x.WholesaleEclId == input.EclId);

            var pagedAndFilteredWholesaleEclOverrides = filteredWholesaleEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var wholesaleEclOverrides = from o in pagedAndFilteredWholesaleEclOverrides
                                     join o1 in filteredFrameworkResult on o.ContractId equals o1.ContractId into j1
                                     from s1 in j1.DefaultIfEmpty()

                                     select new GetEclOverrideForViewDto()
                                     {
                                         EclOverride = new EclOverrideDto
                                         {
                                             StageOverride = o.StageOverride,
                                             ImpairmentOverride = o.ImpairmentOverride,
                                             OverrideComment = o.OverrideComment,
                                             Status = o.Status,
                                             Id = o.Id,
                                             EclId = (Guid)o.WholesaleEclId,
                                             RecordId = s1 == null ? o.Id : s1.Id
                                         },
                                         ContractId = o.ContractId,
                                         //AccountNumber = s1 == null ? "" : s1.AssetDescription,
                                         //CustomerName = s1 == null ? "" : s1.AssetDescription
                                     };

            var totalCount = await filteredWholesaleEclOverrides.CountAsync();

            return new PagedResultDto<GetEclOverrideForViewDto>(
                totalCount,
                await wholesaleEclOverrides.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides_Edit)]
        public async Task<GetWholesaleEclOverrideForEditOutput> GetWholesaleEclOverrideForEdit(EntityDto<Guid> input)
        {
            var wholesaleEclOverride = await _wholesaleEclOverrideRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWholesaleEclOverrideForEditOutput { WholesaleEclOverride = ObjectMapper.Map<CreateOrEditWholesaleEclOverrideDto>(wholesaleEclOverride) };

            if (output.WholesaleEclOverride.WholesaleEclDataLoanBookId != null)
            {
                var _lookupWholesaleEclDataLoanBook = await _lookup_wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclOverride.WholesaleEclDataLoanBookId);
                output.WholesaleEclDataLoanBookCustomerName = _lookupWholesaleEclDataLoanBook.CustomerName.ToString();
            }

            return output;
        }

        public async Task<List<NameValueDto>> SearchResult(EclShared.Dtos.GetRecordForOverrideInputDto input)
        {
            var filteredRecords = _lookup_wholesaleEclFrameworkRepository.GetAll().Where(x => x.WholesaleEclId == input.EclId);

            var query = from o in filteredRecords
                        join o1 in _lookup_wholesaleEclDataLoanBookRepository.GetAll() on o.ContractId equals o1.ContractId into j1
                        from s1 in j1.DefaultIfEmpty()

                        select new GetWholesaleEclOverrideForViewDto()
                        {
                            WholesaleEclOverride = new WholesaleEclOverrideDto
                            {
                                ContractId = o.ContractId,
                                Id = o.Id
                            },
                            WholesaleEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
                        };

            return await query.Where(x => x.WholesaleEclDataLoanBookCustomerName.ToLower().Contains(input.searchTerm.ToLower()))
                                            .Select(x => new NameValueDto
                                            {
                                                Value = x.WholesaleEclOverride.Id.ToString(),
                                                Name = x.WholesaleEclDataLoanBookCustomerName
                                            }).ToListAsync();
        }

        public async Task<GetPreResultForOverrideOutput> GetEclRecordDetails(EntityDto<Guid> input)
        {
            var selectedRecord = await _lookup_wholesaleEclFrameworkRepository.FirstOrDefaultAsync(input.Id);
            var overrideRecord = await _wholesaleEclOverrideRepository.FirstOrDefaultAsync(x => x.WholesaleEclId == selectedRecord.WholesaleEclId && x.ContractId == selectedRecord.ContractId);
            var contract = await _lookup_wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync(x => x.WholesaleEclUploadId == selectedRecord.WholesaleEclId && x.ContractId == selectedRecord.ContractId);

            var dto = new InvestmentComputation.Dtos.CreateOrEditEclOverrideDto() { ContractId = selectedRecord.ContractId, EclSicrId = selectedRecord.Id, EclId = selectedRecord.WholesaleEclId };

            if (overrideRecord != null)
            {
                dto.EclId = (Guid)overrideRecord.WholesaleEclId;
                dto.Id = overrideRecord.Id;
                dto.OverrideComment = overrideRecord.OverrideComment;
                dto.Stage = overrideRecord.StageOverride;
                dto.Status = overrideRecord.Status;
            }

            return new GetPreResultForOverrideOutput
            {
                ContractId = selectedRecord.ContractId,
                AccountNumber = contract.AccountNo,
                AccountName = contract.CustomerName,
                Stage = selectedRecord.Stage,
                Impairment = selectedRecord.FinalEclValue,
                EclOverrides = dto
            };
        }


        public async Task CreateOrEdit(CreateOrEditEclOverrideDto input)
        {
            var validation = await ValidateForOverride(input);
            if (validation.Status)
            {
                if (input.Id == null)
                {
                    await Create(input);
                }
                else
                {
                    await Update(input);
                }
            }
            else
            {
                throw new UserFriendlyException(validation.Message);
            }
        }

        protected virtual async Task Create(CreateOrEditEclOverrideDto input)
        {
            await _wholesaleEclOverrideRepository.InsertAsync(new WholesaleEclOverride
            {
                Id = new Guid(),
                WholesaleEclId = input.EclId,
                ContractId = input.ContractId,
                OverrideComment = input.OverrideComment,
                StageOverride = input.Stage,
                ImpairmentOverride = input.ImpairmentOverride,
                Status = GeneralStatusEnum.Submitted
            });
        }

        protected virtual async Task Update(CreateOrEditEclOverrideDto input)
        {
            var eclOverride = await _wholesaleEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);

            eclOverride.WholesaleEclId = input.EclId;
            eclOverride.ContractId = input.ContractId;
            eclOverride.OverrideComment = input.OverrideComment;
            eclOverride.StageOverride = input.Stage;
            eclOverride.ImpairmentOverride = input.ImpairmentOverride;
            eclOverride.Status = GeneralStatusEnum.Submitted;

            await _wholesaleEclOverrideRepository.UpdateAsync(eclOverride);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _wholesaleEclOverrideRepository.DeleteAsync(input.Id);
        }

        public virtual async Task ApproveReject(EclShared.Dtos.ReviewEclOverrideInputDto input)
        {
            var ecl = await _wholesaleEclOverrideRepository.FirstOrDefaultAsync((Guid)input.OverrideRecordId);

            await _lookup_wholesaleEclOverrideApprovalRepository.InsertAsync(new WholesaleEclOverrideApproval
            {
                EclOverrideId = input.OverrideRecordId,
                WholesaleEclId = input.EclId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewDate = DateTime.Now,
                Status = input.Status
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _lookup_wholesaleEclOverrideApprovalRepository.GetAllListAsync(x => x.WholesaleEclId == input.OverrideRecordId && x.Status == GeneralStatusEnum.Approved);
                if (eclApprovals.Count(x => x.Status == GeneralStatusEnum.Approved) >= requiredApprovals)
                {
                    ecl.Status = GeneralStatusEnum.Approved;
                }
                else
                {
                    ecl.Status = GeneralStatusEnum.AwaitngAdditionApproval;
                }
            }
            else
            {
                ecl.Status = GeneralStatusEnum.Draft;
            }

            ObjectMapper.Map(ecl, ecl);
        }

        protected async Task<EclShared.Dtos.ValidationMessageDto> ValidateForOverride(CreateOrEditEclOverrideDto input)
        {
            var output = new EclShared.Dtos.ValidationMessageDto();

            var reviewedOverride = await _wholesaleEclOverrideRepository.FirstOrDefaultAsync(x => x.ContractId == input.ContractId && x.Status != GeneralStatusEnum.Submitted);

            if (reviewedOverride != null)
            {
                output.Status = false;
                output.Message = L("ApplyOverrideErrorRecordReviewed");
            }
            else
            {
                output.Status = true;
                output.Message = "";
            }

            return output;
        }

    }
}