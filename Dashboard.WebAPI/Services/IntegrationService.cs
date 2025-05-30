﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dashboard.WebAPI.Configurations;
using Dashboard.WebAPI.DTO;
using Dashboard.WebAPI.ServiceContracts;
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

        public async Task<IntegrationStatusDto?> GetDataAsync(string integrationName)
        {
           
            var configs = _configuration.GetSection("ExternalIntegrations").Get<List<ExternalIntegrationConfig>>()!;

            var config = configs.FirstOrDefault(x => x.Name.Equals(integrationName, StringComparison.OrdinalIgnoreCase));
            if (config == null) return null;

            var request = new HttpRequestMessage(new HttpMethod(config.HttpMethod), config.Url);
            request.Headers.Add("X-Access-Key", config.AccessKey);
            request.Headers.Add("X-Secret-Key", config.SecretKey);

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            var token = JToken.Parse(json).SelectToken(config.JsonPath);
            if (token == null) return null;

            var dto = token.ToObject<IntegrationStatusDto>();
            dto!.Name = config.Name;
            return dto;
        }

       
    }
}
