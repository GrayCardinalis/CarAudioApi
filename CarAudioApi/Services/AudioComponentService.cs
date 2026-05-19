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
            var components = await _context.AudioComponents.ToListAsync();
            //Ручной маппинг, перенос данных из модели в DTO
            return components.Select(c => new ComponentResponseDto
            {
                Id = c.Id,
                FullName = $"{c.Brand} {c.Model}",
                Type = c.Type
            });
        }
        public async Task<ComponentResponseDto?> GetByIdAsync(int id)
        {
            var component = await _context.AudioComponents.FindAsync(id);
            if (component == null) return null;
            return new ComponentResponseDto
            {
                Id = component.Id,
                FullName = $"{component.Brand} {component.Model}",
                Type = component.Type
            };
        }

        public async Task<ComponentResponseDto> CreateAsync(CreateComponentDto createDto)
        {
            var component = new AudioComponent
            {
                Brand = createDto.Brand,
                Model = createDto.Model,
                Type = createDto.Type,
                RetailPrice = createDto.RetailPrice
            };
            _context.AudioComponents.Add(component);
            await _context.SaveChangesAsync();
            return new ComponentResponseDto
            {
                Id = component.Id,
                FullName = $"{component.Brand} {component.Model}",
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
