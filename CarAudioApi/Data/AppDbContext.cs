using CarAudioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAudioApi.Data
{
    public class AppDbContext: DbContext
    {
        //Конструктор, принимающий настройки (включая нашу строку подключения)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AudioComponent> AudioComponents { get; set; }
    }
}
