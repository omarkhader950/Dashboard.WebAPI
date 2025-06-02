namespace Dashboard.WebAPI.DTO
{
    public class NormalizedResultDto
    {

        public string ServiceName { get; set; }
        public int PendingApproval { get; set; }
        public int PendingCorrection { get; set; }
        public int PendingSubmission { get; set; }
    }
}
