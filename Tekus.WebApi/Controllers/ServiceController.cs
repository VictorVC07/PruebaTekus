using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;

namespace Tekus.WebApi.Controllers
{
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                return BadRequest();
            }

            var createdService = await _servicesService.CreateServiceAsync(serviceDto);
            return CreatedAtAction(nameof(ListServices), new { id = createdService.Id }, createdService);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ServiceDto ServiceDto)
        {
            if (ServiceDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedService = await _servicesService.UpdateServiceAsync(ServiceDto);
                return Ok(updatedService);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("service-count-by-country")]
        public async Task<IActionResult> GetServiceCountByCountry()
        {
            var serviceCounts = await _servicesService.GetServiceCountByCountryAsync();
            return Ok(serviceCounts);
        }
    }
}
