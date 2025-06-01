
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dashboard.WebAPI.Configurations;
using Dashboard.WebAPI.DTO;
using Dashboard.WebAPI.ServiceContracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Dashboard.WebAPI.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public IntegrationService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, List<object>>?> GetAllDataGroupedByNameAsync()
        {
            var configs = _configuration.GetSection("ExternalIntegrations").Get<List<ExternalIntegrationConfig>>()!;
            var client = _httpClientFactory.CreateClient();
            var groupedResults = new Dictionary<string, List<object>>();

            foreach (var config in configs)
            {
                var dtoType = config.DtoType switch
                {
                    "NotificationStatusDto" => typeof(NotificationStatusDto),
                    "PaymentStatusDto" => typeof(PaymentStatusDto),
                    "UserStatusDto" => typeof(UserStatusDto),
                    _ => null
                };

                if (dtoType == null) continue;

                var request = new HttpRequestMessage(new HttpMethod(config.HttpMethod), config.Url);
                request.Headers.Add("X-Access-Key", config.AccessKey);
                request.Headers.Add("X-Secret-Key", config.SecretKey);

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) continue;

                var json = await response.Content.ReadAsStringAsync();
                var array = JArray.Parse(json);
                var list = (IList)array.ToObject(typeof(List<>).MakeGenericType(dtoType))!;
                if (list == null || list.Count == 0) continue;

                groupedResults[config.Name] = list.Cast<object>().ToList();
            }

            return groupedResults;
        }


        public async Task<List<object>?> GetDataAsync(string integrationName)
        {
            var configs = _configuration.GetSection("ExternalIntegrations").Get<List<ExternalIntegrationConfig>>()!;
            var config = configs.FirstOrDefault(x => x.Name.Equals(integrationName, StringComparison.OrdinalIgnoreCase));
            if (config == null) return null;

            var request = new HttpRequestMessage(new HttpMethod(config.HttpMethod), config.Url);
            request.Headers.Add("X-Access-Key", config.AccessKey);
            request.Headers.Add("X-Secret-Key", config.SecretKey);

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();

            var dtoType = config.DtoType switch
            {
                "NotificationStatusDto" => typeof(NotificationStatusDto),
                "PaymentStatusDto" => typeof(PaymentStatusDto),
                "UserStatusDto" => typeof(UserStatusDto),
                _ => null
            };

            if (dtoType == null) return null;

            // Deserialize into List<T> (as object)
            var listObject = JsonConvert.DeserializeObject(json, typeof(List<>).MakeGenericType(dtoType));
            if (listObject is not System.Collections.IEnumerable enumerable) return null;

            // Convert to List<object>
            var result = new List<object>();
            foreach (var item in enumerable)
            {
                result.Add(item);
            }

            return result;
        }




    }
}
