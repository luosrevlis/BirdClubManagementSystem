namespace BirdClubManagementSystem.Models.Statuses
{
    public class MemRequestStatuses
    {
        public const string Pending = "PEN";
        public const string Accepted = "ACC";
        public const string Rejected = "REJ";
        public const string PaymentReceived = "REC";

        public static string Convert(string code)
        {
            return code switch
            {
                Pending => nameof(Pending),
                Accepted => nameof(Accepted),
                Rejected => nameof(Rejected),
                PaymentReceived => "Payment Received",
                _ => throw new ArgumentException(code),
            };
        }
    }
}
