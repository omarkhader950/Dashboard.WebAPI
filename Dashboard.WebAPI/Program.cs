using Dashboard.WebAPI.ServiceContracts;
using Dashboard.WebAPI.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IIntegrationService, IntegrationService>();

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    TypeNameHandling = TypeNameHandling.None
};

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
