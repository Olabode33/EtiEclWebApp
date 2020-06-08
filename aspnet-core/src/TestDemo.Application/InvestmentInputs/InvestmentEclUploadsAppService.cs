using TestDemo.Investment;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.InvestmentInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Authorization.Users;
using Abp.UI;
using TestDemo.Dto.Inputs;

namespace TestDemo.InvestmentInputs
{
	[AbpAuthorize(AppPermissions.Pages_InvestmentEclUploads)]
    public class InvestmentEclUploadsAppService : TestDemoAppServiceBase, IInvestmentEclUploadsAppService
    {
		 private readonly IRepository<InvestmentEclUpload, Guid> _investmentEclUploadRepository;
		 private readonly IRepository<InvestmentEcl,Guid> _lookup_investmentEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public InvestmentEclUploadsAppService(
            IRepository<InvestmentEclUpload, Guid> investmentEclUploadRepository , 
            IRepository<InvestmentEcl, Guid> lookup_investmentEclRepository,
            IRepository<User, long> lookup_userRepository) 
		  {
			_investmentEclUploadRepository = investmentEclUploadRepository;
			_lookup_investmentEclRepository = lookup_investmentEclRepository;
            _lookup_userRepository = lookup_userRepository;
		  }

		 public async Task<PagedResultDto<GetInvestmentEclUploadForViewDto>> GetAll(GetAllInvestmentEclUploadsInput input)
         {
			
			var filteredInvestmentEclUploads = _investmentEclUploadRepository.GetAll()
						.Include( e => e.InvestmentEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.UploadComment.Contains(input.Filter));

			var pagedAndFilteredInvestmentEclUploads = filteredInvestmentEclUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentEclUploads = from o in pagedAndFilteredInvestmentEclUploads
                         join o1 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetInvestmentEclUploadForViewDto() {
							EclUpload = new InvestmentEclUploadDto
							{
                                DocType = o.DocType,
                                Status = o.Status,
                                Id = o.Id
							},
                         	InvestmentEclReportingDate = s1 == null ? "" : s1.ReportingDate.ToString()
						};

            var totalCount = await filteredInvestmentEclUploads.CountAsync();

            return new PagedResultDto<GetInvestmentEclUploadForViewDto>(
                totalCount,
                await investmentEclUploads.ToListAsync()
            );
         }

        public async Task<List<GetEclUploadForViewDto>> GetEclUploads(EntityDto<Guid> input)
        {
            var retailEclUploads = from o in _investmentEclUploadRepository.GetAll().Where(x => x.InvestmentEclId == input.Id)
                                   join o1 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   join u in _lookup_userRepository.GetAll() on o.CreatorUserId equals u.Id into u1
                                   from u2 in u1.DefaultIfEmpty()

                                   select new GetEclUploadForViewDto()
                                   {
                                       EclUpload = new EclUploadDto
                                       {
                                           DocType = o.DocType,
                                           //UploadComment = o.UploadComment,
                                           Status = o.Status,
                                           EclId = o.InvestmentEclId,
                                           Id = o.Id
                                       },
                                       DateUploaded = o.CreationTime,
                                       UploadedBy = u2 == null ? "" : u2.FullName
                                   };

            return await retailEclUploads.ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclUploads_Edit)]
		 public async Task<GetInvestmentEclUploadForEditOutput> GetInvestmentEclUploadForEdit(EntityDto<Guid> input)
         {
            var investmentEclUpload = await _investmentEclUploadRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentEclUploadForEditOutput {InvestmentEclUpload = ObjectMapper.Map<CreateOrEditInvestmentEclUploadDto>(investmentEclUpload)};

		    if (output.InvestmentEclUpload.EclId != null)
            {
                var _lookupInvestmentEcl = await _lookup_investmentEclRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclUpload.EclId);
                output.InvestmentEclReportingDate = _lookupInvestmentEcl.ReportingDate.ToString();
            }
			
            return output;
         }

		 public async Task<Guid> CreateOrEdit(CreateOrEditInvestmentEclUploadDto input)
         {
            if(input.Id == null){
				return await Create(input);
			}
			else{
				await Update(input);
                return input.EclId;
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclUploads_Create)]
		 protected virtual async Task<Guid> Create(CreateOrEditInvestmentEclUploadDto input)
         {
            var eclUploadExist = await _investmentEclUploadRepository.FirstOrDefaultAsync(x => x.DocType == input.DocType && x.InvestmentEclId == input.EclId);

            if (eclUploadExist == null)
            {
                var investmentEclUpload = ObjectMapper.Map<InvestmentEclUpload>(input);

                var id = await _investmentEclUploadRepository.InsertAndGetIdAsync(investmentEclUpload);
                return id;
            }
            else
            {
                throw new UserFriendlyException(L("UploadRecordExists"));
            }
            
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclUploads_Edit)]
		 protected virtual async Task Update(CreateOrEditInvestmentEclUploadDto input)
         {
            var investmentEclUpload = await _investmentEclUploadRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentEclUpload);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclUploads_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentEclUploadRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_InvestmentEclUploads)]
         public async Task<PagedResultDto<InvestmentEclUploadInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_investmentEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ReportingDate.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var investmentEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclUploadInvestmentEclLookupTableDto>();
			foreach(var investmentEcl in investmentEclList){
				lookupTableDtoList.Add(new InvestmentEclUploadInvestmentEclLookupTableDto
				{
					Id = investmentEcl.Id.ToString(),
					//DisplayName = investmentEcl.ReportingDate?.ToString()
				});
			}

            return new PagedResultDto<InvestmentEclUploadInvestmentEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}