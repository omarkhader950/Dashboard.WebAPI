using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.WebAPI.DTO
{
    public class NotificationStatusDto
    {
        public int PendingApproval { get; set; }
        public int PendingCorrection { get; set; }
        public int PendingSubmission { get; set; }
    }
}
