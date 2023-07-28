using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.VnPayApi;

namespace BirdClubInfoHub.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context, string returnUrl)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]!);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", _configuration["VnPay:Version"]!);
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", _configuration["VnPay:TmnCode"]!);
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["VnPay:CurrCode"]!);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["VnPay:Locale"]!);
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.Email} {model.OrderDescription} {model.Amount}"); //model stuff
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", returnUrl);
            pay.AddRequestData("vnp_TxnRef", timeNow.Ticks.ToString());

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["VnPay:BaseUrl"]!, _configuration["VnPay:HashSecret"]!);

            return paymentUrl;
        }

        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["VnPay:HashSecret"]!);

            return response;
        }
    }
}
