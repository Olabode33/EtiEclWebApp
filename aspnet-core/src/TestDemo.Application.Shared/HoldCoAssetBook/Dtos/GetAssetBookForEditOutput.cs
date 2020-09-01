using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.HoldCoAssetBook.Dtos
{
    public class GetAssetBookForEditOutput
    {
		public CreateOrEditAssetBookDto AssetBook { get; set; }


    }
}