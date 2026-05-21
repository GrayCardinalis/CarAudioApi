namespace CarAudioApi.Models
{
    public class AudioComponent
    {
        public int Id { get; set; }
        public string Brand{ get; set; } = string.Empty;
        public string Model{ get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
    }
}
