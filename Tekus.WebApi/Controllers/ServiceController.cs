using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Interfaces;

namespace Tekus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {

        private readonly IServicesService _servicesService;

        public ServiceController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> ListServices()
        {
            var services = await _servicesService.GetAllServicesAsync();
            return Ok(services);
        }
    }
}
