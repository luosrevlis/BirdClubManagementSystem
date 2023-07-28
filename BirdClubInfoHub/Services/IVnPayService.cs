using BirdClubInfoHub.Models.Entities;

namespace BirdClubInfoHub.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context, string returnUrl);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
