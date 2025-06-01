using Dashboard.WebAPI.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IIntegrationService _integrationService;

        public IntegrationController(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }

        [HttpGet("{integrationName}")]
        public async Task<IActionResult> Get(string integrationName)
        {
            var result = await _integrationService.GetDataAsync(integrationName);
            if (result == null || !result.Any()) return NotFound();

            return Ok(result);
        }

      

    }

}
