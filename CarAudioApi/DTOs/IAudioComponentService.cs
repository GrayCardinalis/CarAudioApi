using CarAudioApi.DTOs;
namespace CarAudioApi.DTOs
{
    public interface IAudioComponentService
    {
        Task<IEnumerable<ComponentResponseDto>> GetAllAsync();
        Task<ComponentResponseDto?> GetByIdAsync(int id);
        Task<ComponentResponseDto> CreateAsync(CreateComponentDto createDto);
        Task<bool> UpdateComponentAsync(int id, UpdateComponentDto updateDto);
        Task<bool> DeleteComponentAsync(int id);
    }
}
