namespace CarAudioApi.Models
{
    public class AudioComponent
    {
        public int Id { get; set; }
        // ВРЕМЕННО ВОЗВРАЩАЕМ старую колонку, чтобы не потерять данные
        public int? BrandId{ get; set; }
        //Навигационное свойство
        public Brand? Brand { get; set; } = null!;
        public string Model{ get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
    }
}
