using Dashboard.WebAPI.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IExternalIntegrationService _service;

        public IntegrationController(IExternalIntegrationService service)
        {
            _service = service;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var results = await _service.FetchAllAsync();
            return Ok(results);
        }



    }

}
