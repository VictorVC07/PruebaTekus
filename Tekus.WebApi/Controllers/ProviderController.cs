using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderDto providerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var provider = await _providerService.CreateProviderAsync(providerDto);
            return CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, provider);
        }

    }
}
