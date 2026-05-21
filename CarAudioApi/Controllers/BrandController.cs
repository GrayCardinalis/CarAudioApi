using CarAudioApi.DTOs;
using CarAudioApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarAudioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandResponseDto>>> GetAllBrands()
        {
            var brands = await _service.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandResponseDto>> GetBrandById(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<BrandResponseDto>> CreateBrand(CreateBrandDto brandDto)
        {
            var createdBrand = await _service.CreateAsync(brandDto);
            return CreatedAtAction(
                actionName: nameof(GetBrandById),
                routeValues: new { id = createdBrand.Id },
                value: createdBrand);
        }
    }
}
