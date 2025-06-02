using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.WebAPI.Configurations
{
    public class ExternalIntegrationConfig
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }

        public string PendingApproval { get; set; }
        public string PendingCorrection { get; set; }
        public string PendingSubmission { get; set; }
    }
}


