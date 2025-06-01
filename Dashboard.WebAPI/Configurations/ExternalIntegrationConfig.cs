using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.WebAPI.Configurations
{
    public class ExternalIntegrationConfig
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = "GET";
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string JsonPath { get; set; } = "";
        public string DtoType { get; set; } = string.Empty;

    }
}
