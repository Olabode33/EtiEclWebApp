

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.HoldCoInterCompanyResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Calibration.Exporting;

namespace TestDemo.HoldCoInterCompanyResults
{
	//[AbpAuthorize(AppPermissions.Pages_HoldCoInterCompanyResults)]
    public class HoldCoInterCompanyResultsAppService : TestDemoAppServiceBase, IHoldCoInterCompanyResultsAppService
    {
		private readonly IRepository<HoldCoInterCompanyResult, Guid> _holdCoInterCompanyResultRepository;
        private readonly IInputPdCrDrExporter _inputDataExporter;


        public HoldCoInterCompanyResultsAppService(IRepository<HoldCoInterCompanyResult, Guid> holdCoInterCompanyResultRepository, IInputPdCrDrExporter inputDataExporter) 
		  {
			_holdCoInterCompanyResultRepository = holdCoInterCompanyResultRepository;
            _inputDataExporter = inputDataExporter;


          }

        public async Task<List<CreateOrEditHoldCoInterCompanyResultDto>> GetResults(Guid id)
        {
            return ObjectMapper.Map<List<CreateOrEditHoldCoInterCompanyResultDto>>(await _holdCoInterCompanyResultRepository.GetAll().Where(a => a.RegistrationId == id).ToListAsync());

        }

            public async Task<PagedResultDto<GetHoldCoInterCompanyResultForViewDto>> GetAll(GetAllHoldCoInterCompanyResultsInput input)
         {
			
			var filteredHoldCoInterCompanyResults = _holdCoInterCompanyResultRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.AssetType.Contains(input.Filter) || e.AssetDescription.Contains(input.Filter));

			var pagedAndFilteredHoldCoInterCompanyResults = filteredHoldCoInterCompanyResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var holdCoInterCompanyResults = from o in pagedAndFilteredHoldCoInterCompanyResults
                         select new GetHoldCoInterCompanyResultForViewDto() {
							HoldCoInterCompanyResult = new HoldCoInterCompanyResultDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredHoldCoInterCompanyResults.CountAsync();

            return new PagedResultDto<GetHoldCoInterCompanyResultForViewDto>(
                totalCount,
                await holdCoInterCompanyResults.ToListAsync()
            );
         }
		 
		 public async Task<GetHoldCoInterCompanyResultForViewDto> GetHoldCoInterCompanyResultForView(Guid id)
         {
            var holdCoInterCompanyResult = await _holdCoInterCompanyResultRepository.GetAsync(id);

            var output = new GetHoldCoInterCompanyResultForViewDto { HoldCoInterCompanyResult = ObjectMapper.Map<HoldCoInterCompanyResultDto>(holdCoInterCompanyResult) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInterCompanyResults_Edit)]
		 public async Task<GetHoldCoInterCompanyResultForEditOutput> GetHoldCoInterCompanyResultForEdit(EntityDto<Guid> input)
         {
            var holdCoInterCompanyResult = await _holdCoInterCompanyResultRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetHoldCoInterCompanyResultForEditOutput {HoldCoInterCompanyResult = ObjectMapper.Map<CreateOrEditHoldCoInterCompanyResultDto>(holdCoInterCompanyResult)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditHoldCoInterCompanyResultDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInterCompanyResults_Create)]
		 protected virtual async Task Create(CreateOrEditHoldCoInterCompanyResultDto input)
         {
            var holdCoInterCompanyResult = ObjectMapper.Map<HoldCoInterCompanyResult>(input);

			

            await _holdCoInterCompanyResultRepository.InsertAsync(holdCoInterCompanyResult);
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInterCompanyResults_Edit)]
		 protected virtual async Task Update(CreateOrEditHoldCoInterCompanyResultDto input)
         {
            var holdCoInterCompanyResult = await _holdCoInterCompanyResultRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, holdCoInterCompanyResult);
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInterCompanyResults_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _holdCoInterCompanyResultRepository.DeleteAsync(input.Id);
         }

        public async Task<FileDto> ExportToExcel(EntityDto<Guid> input)
        {

            var items = await _holdCoInterCompanyResultRepository.GetAll().Where(x => x.RegistrationId == input.Id)
                                                         .Select(x => ObjectMapper.Map<InputHoldCoInterCompanyResultDto>(x))
                                                         .ToListAsync();

            return _inputDataExporter.ExportToFile(items);
        }
    }
}