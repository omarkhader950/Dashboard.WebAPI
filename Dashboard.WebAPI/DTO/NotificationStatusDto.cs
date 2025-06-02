using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.WebAPI.DTO
{
    public class NotificationStatusDto
    {
        public string Name { get; set; } = string.Empty;

        public int TotalPendingApproval { get; set; }
        public int TotalPendingCorrection { get; set; }
        public int TotalPendingSubmission { get; set; }
    }
}
