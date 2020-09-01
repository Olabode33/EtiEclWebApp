using Abp.Application.Services.Dto;
using System;

namespace TestDemo.HoldCoAssetBook.Dtos
{
    public class GetAllAssetBooksInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}