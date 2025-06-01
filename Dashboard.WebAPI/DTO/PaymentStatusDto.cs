namespace Dashboard.WebAPI.DTO
{
    public class PaymentStatusDto
    {
        public string Provider { get; set; } = string.Empty;   // maps to "provider"
        public string Currency { get; set; } = string.Empty;   // maps to "currency"
        public int PendingApproval { get; set; }               // maps to "pendingApproval"
        public int PendingRefunds { get; set; }                // maps to "pendingRefunds"
        public int PendingPayouts { get; set; }                // maps to "pendingPayouts"
        public double TotalAmount { get; set; }
    }



}
