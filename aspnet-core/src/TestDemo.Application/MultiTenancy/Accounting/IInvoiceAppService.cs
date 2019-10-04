using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using TestDemo.MultiTenancy.Accounting.Dto;

namespace TestDemo.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
