using CarAudioApi.DTOs;
using CarAudioApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarAudioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioComponentsController : ControllerBase
    {
        private readonly IAudioComponentService _service;
        
        public AudioComponentsController(IAudioComponentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentResponseDto>>> GetAll()
        {
            var components = await _service.GetAllAsync();
            return Ok(components);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentResponseDto>> GetById(int id)
        {
            var component = await _service.GetByIdAsync(id);
            if (component == null) return NotFound();
            return Ok(component);
        }

        [HttpPost]
        public async Task<ActionResult<ComponentResponseDto>> Create(CreateComponentDto dto)
        {
            var createdComponent = await _service.CreateAsync(dto);
            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = createdComponent.Id },
                value: createdComponent);
        }
    }
}
