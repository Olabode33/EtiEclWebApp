﻿using TestDemo.Wholesale;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.EclShared.Dtos;

namespace TestDemo.WholesaleAssumption
{
    public class WholesalePdAssumptionNonInternalModelsAppService : TestDemoAppServiceBase, IWholesalePdAssumptionNonInternalModelsAppService
    {
		 private readonly IRepository<WholesaleEclPdAssumptionNonInternalModel, Guid> _wholesalePdAssumptionNonInternalModelRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdAssumptionNonInternalModelsAppService(IRepository<WholesaleEclPdAssumptionNonInternalModel, Guid> wholesalePdAssumptionNonInternalModelRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdAssumptionNonInternalModelRepository = wholesalePdAssumptionNonInternalModelRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

        public async Task<List<PdInputAssumptionNonInternalModelDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _wholesalePdAssumptionNonInternalModelRepository.GetAll()
                                                              .Where(x => x.WholesaleEclId == input.Id)
                                                              .Select(x => new PdInputAssumptionNonInternalModelDto()
                                                              {
                                                                  Key = x.Key,
                                                                  PdGroup = x.PdGroup,
                                                                  Month = x.Month,
                                                                  MarginalDefaultRate = x.MarginalDefaultRate,
                                                                  CummulativeSurvival = x.CummulativeSurvival,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        public async Task<PagedResultDto<GetWholesalePdAssumptionNonInternalModelForViewDto>> GetAll(GetAllWholesalePdAssumptionNonInternalModelsInput input)
         {
			
			var filteredWholesalePdAssumptionNonInternalModels = _wholesalePdAssumptionNonInternalModelRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredWholesalePdAssumptionNonInternalModels = filteredWholesalePdAssumptionNonInternalModels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdAssumptionNonInternalModels = from o in pagedAndFilteredWholesalePdAssumptionNonInternalModels
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdAssumptionNonInternalModelForViewDto() {
							WholesalePdAssumptionNonInternalModel = new WholesalePdAssumptionNonInternalModelDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdAssumptionNonInternalModels.CountAsync();

            return new PagedResultDto<GetWholesalePdAssumptionNonInternalModelForViewDto>(
                totalCount,
                await wholesalePdAssumptionNonInternalModels.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesalePdAssumptionNonInternalModelForEditOutput> GetWholesalePdAssumptionNonInternalModelForEdit(EntityDto<Guid> input)
         {
            var wholesalePdAssumptionNonInternalModel = await _wholesalePdAssumptionNonInternalModelRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdAssumptionNonInternalModelForEditOutput {WholesalePdAssumptionNonInternalModel = ObjectMapper.Map<CreateOrEditWholesalePdAssumptionNonInternalModelDto>(wholesalePdAssumptionNonInternalModel)};

		    if (output.WholesalePdAssumptionNonInternalModel.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdAssumptionNonInternalModel.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdAssumptionNonInternalModelDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesalePdAssumptionNonInternalModelDto input)
         {
            var wholesalePdAssumptionNonInternalModel = ObjectMapper.Map<WholesaleEclPdAssumptionNonInternalModel>(input);

			

            await _wholesalePdAssumptionNonInternalModelRepository.InsertAsync(wholesalePdAssumptionNonInternalModel);
         }

		 protected virtual async Task Update(CreateOrEditWholesalePdAssumptionNonInternalModelDto input)
         {
            var wholesalePdAssumptionNonInternalModel = await _wholesalePdAssumptionNonInternalModelRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdAssumptionNonInternalModel);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdAssumptionNonInternalModelRepository.DeleteAsync(input.Id);
         } 
    }
}