namespace Dashboard.WebAPI.DTO
{
    public class PaymentStatusDto
    {
        public string Provider { get; set; } = string.Empty;   
        public string Currency { get; set; } = string.Empty;   
        public int PendingApproval { get; set; }               
        public int PendingRefunds { get; set; }                
        public int PendingPayouts { get; set; }                
        public double TotalAmount { get; set; }
    }



}
