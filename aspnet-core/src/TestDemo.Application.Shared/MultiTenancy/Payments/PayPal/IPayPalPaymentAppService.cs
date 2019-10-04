using System.Threading.Tasks;
using Abp.Application.Services;
using TestDemo.MultiTenancy.Payments.Dto;
using TestDemo.MultiTenancy.Payments.PayPal.Dto;

namespace TestDemo.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalPaymentId, string paypalPayerId);

        PayPalConfigurationDto GetConfiguration();

        Task CancelPayment(CancelPaymentDto input);
    }
}
