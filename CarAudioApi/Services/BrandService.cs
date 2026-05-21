using CarAudioApi.Data;
using CarAudioApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CarAudioApi.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<BrandResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<BrandResponseDto?> IBrandService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        Task<BrandResponseDto> IBrandService.CreateAsync(CreateBrandDto createDto)
        {
            throw new NotImplementedException();
        }
    }
}
