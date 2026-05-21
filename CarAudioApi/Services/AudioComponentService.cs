using CarAudioApi.Data;
using CarAudioApi.DTOs;
using CarAudioApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarAudioApi.Services
{
    public class AudioComponentService : IAudioComponentService
    {
        private readonly AppDbContext _context;

        public AudioComponentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComponentResponseDto>> GetAllAsync()
        {
            var components = await _context.AudioComponents
                .Include(c=>c.Brand) // Подгружаем данные о бренде
                // Отключает Change Tracker в EF Core: данные загружаются только для чтения (Read-Only),для ускорения Read-Only запросов
                .AsNoTracking()
                .ToListAsync();// Превращает LINQ в SQL, отправляет в БД и асинхронно материализует результат в List

            //Ручной маппинг, перенос данных из модели в DTO
            return components.Select(c => new ComponentResponseDto
            {
                Id = c.Id,
                FullName = $"{c.Brand.Name} {c.Model}",
                Type = c.Type
            });
        }
        public async Task<ComponentResponseDto?> GetByIdAsync(int id)
        {
            var component = await _context.AudioComponents
                .Include(c => c.Brand)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id); // FindAsync не поддерживает Include!
            if (component == null) return null;
            return new ComponentResponseDto
            {
                Id = component.Id,
                FullName = $"{component.Brand.Name} {component.Model}",
                Type = component.Type
            };
        }

        public async Task<ComponentResponseDto> CreateAsync(CreateComponentDto createDto)
        {
            var component = new AudioComponent
            {
                BrandId = createDto.BrandId, // Присваиваем внешний ключ!
                Model = createDto.Model,
                Type = createDto.Type,
                RetailPrice = createDto.RetailPrice
            };

            _context.AudioComponents.Add(component);
            await _context.SaveChangesAsync();

            // Внимание: чтобы вернуть корректный DTO с именем бренда сразу после создания, 
            // нам нужно будет подгрузить бренд из базы. Но пока оставим упрощенно, 
            // чтобы ты просто прошел этап компиляции.
            return new ComponentResponseDto
            {
                Id = component.Id,
                FullName = $"Бренд ID {component.BrandId} {component.Model}",
                Type = component.Type
            };
        }
        public Task<bool> DeleteComponentAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateComponentAsync(int id, UpdateComponentDto updateDto)
        {
            throw new NotImplementedException();
        }
        //Внедряем контекст PostgreSQL через конструктор

    }
}
