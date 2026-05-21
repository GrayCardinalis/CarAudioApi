using CarAudioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAudioApi.Data
{
    public class AppDbContext: DbContext
    {
        //Конструктор, принимающий настройки (включая нашу строку подключения)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<AudioComponent> AudioComponents { get; set; }
        public DbSet<Brand> Brands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AudioComponent>()
                .HasOne(c => c.Brand)// У компонента ОДИН бренд
                .WithMany(b => b.Components)   // У бренда МНОГО компонентов
                .HasForeignKey(c => c.BrandId) // Внешний ключ - BrandId
                .OnDelete(DeleteBehavior.Restrict); // Поведение при удалении
        }
    }
}
