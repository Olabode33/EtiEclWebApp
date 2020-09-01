

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.HoldCoAssetBook
{
	[AbpAuthorize(AppPermissions.Pages_AssetBooks)]
    public class AssetBooksAppService : TestDemoAppServiceBase, IAssetBooksAppService
    {
		 private readonly IRepository<AssetBook, Guid> _assetBookRepository;
		 

		  public AssetBooksAppService(IRepository<AssetBook, Guid> assetBookRepository ) 
		  {
			_assetBookRepository = assetBookRepository;
			
		  }

		 public async Task<PagedResultDto<GetAssetBookForViewDto>> GetAll(GetAllAssetBooksInput input)
         {
			
			var filteredAssetBooks = _assetBookRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Entity.Contains(input.Filter) || e.AssetDescription.Contains(input.Filter) || e.AssetType.Contains(input.Filter) || e.RatingAgency.Contains(input.Filter) || e.PurchaseDateCreditRating.Contains(input.Filter) || e.CurrentCreditRating.Contains(input.Filter) || e.PrincipalAmortisation.Contains(input.Filter) || e.PrincipalRepaymentTerms.Contains(input.Filter) || e.InterestRepaymentTerms.Contains(input.Filter) || e.PrudentialClassification.Contains(input.Filter) || e.ForebearanceFlag.Contains(input.Filter));

			var pagedAndFilteredAssetBooks = filteredAssetBooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var assetBooks = from o in pagedAndFilteredAssetBooks
                         select new GetAssetBookForViewDto() {
							AssetBook = new AssetBookDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredAssetBooks.CountAsync();

            return new PagedResultDto<GetAssetBookForViewDto>(
                totalCount,
                await assetBooks.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_AssetBooks_Edit)]
		 public async Task<GetAssetBookForEditOutput> GetAssetBookForEdit(EntityDto<Guid> input)
         {
            var assetBook = await _assetBookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetAssetBookForEditOutput {AssetBook = ObjectMapper.Map<CreateOrEditAssetBookDto>(assetBook)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditAssetBookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_AssetBooks_Create)]
		 protected virtual async Task Create(CreateOrEditAssetBookDto input)
         {
            var assetBook = ObjectMapper.Map<AssetBook>(input);

			

            await _assetBookRepository.InsertAsync(assetBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_AssetBooks_Edit)]
		 protected virtual async Task Update(CreateOrEditAssetBookDto input)
         {
            var assetBook = await _assetBookRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, assetBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_AssetBooks_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _assetBookRepository.DeleteAsync(input.Id);
         } 
    }
}