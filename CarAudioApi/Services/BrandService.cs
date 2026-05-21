using CarAudioApi.Data;
using CarAudioApi.DTOs;
using CarAudioApi.Models;
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
        public async Task<IEnumerable<BrandResponseDto>> GetAllAsync()
        {
            return await _context.Brands
                .AsNoTracking()
                .Select(b => new BrandResponseDto
                {
                    Id = b.Id,
                    FullName = b.Name,
                    Description = b.Description
                })
                .ToListAsync();
        }

        public async Task<BrandResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Brands
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BrandResponseDto
                {
                    Id = b.Id,
                    FullName = b.Name,
                    Description = b.Description
                })
                .FirstOrDefaultAsync();
        }
        public async Task<BrandResponseDto> CreateAsync(CreateBrandDto createDto)
        {
            // Маппинг (перенос) данных из DTO в полноценную модель БД
            var brand = new Brand
            {
                Name = createDto.Name,
                Description = createDto.Description
            };
            // Добавляем в контекст. SQL-запрос INSERT пока НЕ выполняется
            _context.Brands.Add(brand);

            // Сохраняем транзакцию в PostgreSQL
            await _context.SaveChangesAsync();

            // Возвращаем клиенту красивый DTO, а не сырую модель
            return new BrandResponseDto
            {
                Id = brand.Id,
                FullName = brand.Name,
                Description = brand.Description
            };
        }
    }
}
