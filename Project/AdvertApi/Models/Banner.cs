using System.Text.Json.Serialization;

namespace AdvertApi.Models
{
    public class Banner
    {
        public int IdAdvertisement { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int IdCampaing { get; set; }

        [JsonIgnore]
        public virtual Campaing Campaing { get; set; }

        public decimal Area { get; set; }
    }
}
