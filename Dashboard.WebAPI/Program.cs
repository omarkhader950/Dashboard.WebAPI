using Dashboard.WebAPI.ServiceContracts;
using Dashboard.WebAPI.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IExternalIntegrationService, ExternalIntegrationService>();


builder.Services.Configure<List<Dashboard.WebAPI.Configurations.ExternalIntegrationConfig>>(
    builder.Configuration.GetSection("ExternalIntegrations"));

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    TypeNameHandling = TypeNameHandling.None
};

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
