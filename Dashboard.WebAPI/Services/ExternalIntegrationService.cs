
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dashboard.WebAPI.Configurations;
using Dashboard.WebAPI.Constants;
using Dashboard.WebAPI.DTO;
using Dashboard.WebAPI.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Dashboard.WebAPI.Services
{
    public class ExternalIntegrationService : IExternalIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private readonly List<ExternalIntegrationConfig> _integrations;

        public ExternalIntegrationService(
            IHttpClientFactory httpClientFactory,
            IOptions<List<ExternalIntegrationConfig>> options)
        {
            _httpClientFactory = httpClientFactory;
            _integrations = options.Value;
        }

        public async Task<List<NormalizedResultDto>> FetchAllAsync()
        {
            var results = new List<NormalizedResultDto>();

            foreach (var integration in _integrations)
            {
                var client = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage(
                    new HttpMethod(integration.HttpMethod), integration.Url);

                request.Headers.Add(IntegrationHeaderNames.AccessKey, integration.AccessKey);
                request.Headers.Add(IntegrationHeaderNames.SecretKey, integration.SecretKey);

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) continue;

                var json = await response.Content.ReadAsStringAsync();
                var jToken = JToken.Parse(json);

                var result = new NormalizedResultDto
                {
                    ServiceName = integration.Name,
                    PendingApproval = ExtractInt(jToken, integration.PendingApproval),
                    PendingCorrection = ExtractInt(jToken, integration.PendingCorrection),
                    PendingSubmission = ExtractInt(jToken, integration.PendingSubmission)
                };

                results.Add(result);
            }

            return results;
        }

        private int ExtractInt(JToken token, string jsonPath)
        {
            var selected = token.SelectToken(jsonPath);
            return selected?.Value<int>() ?? 0;
        }

    }
}
