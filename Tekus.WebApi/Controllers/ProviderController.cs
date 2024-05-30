using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;

namespace Tekus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            var proveedores = await _providerService.ListAsyncProviders();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var proveedor = await _providerService.GetProviderByIdAsync(id);
            if (proveedor == null)
            {
                return NotFound(new { message = $"Proveedor with ID {id} not found." });
            }
            return Ok(proveedor);
        }

        [HttpGet("provider-count-by-country")]
        public async Task<IActionResult> GetProviderCountByCountry()
        {
            var providerCounts = await _providerService.GetProviderCountByCountryAsync();
            return Ok(providerCounts);
        }
    }
}
