using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Inputs
{
    public class EclUploadDto : EntityDto<Guid>
    {
        public UploadDocTypeEnum DocType { get; set; }
        public GeneralStatusEnum Status { get; set; }
        public Guid EclId { get; set; }
        public string UploadComment { get; set; }
        public bool FileUploaded { get; set; }
        public int AllJobs { get; set; }
        public int CompletedJobs { get; set; }
    }
}
