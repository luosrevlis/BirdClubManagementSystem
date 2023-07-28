namespace BirdClubInfoHub.Models.Entities
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; } = "250006";
        public double Amount { get; set; }
        public string OrderDescription { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
