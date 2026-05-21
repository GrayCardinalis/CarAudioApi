using CarAudioApi.DTOs;

namespace CarAudioApi.DTOs
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandResponseDto>> GetAllAsync();
        Task<BrandResponseDto?> GetByIdAsync(int id);
        Task<BrandResponseDto> CreateAsync(CreateBrandDto createDto);
    }
}
