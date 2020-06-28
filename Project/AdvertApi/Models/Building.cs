using System.Collections.Generic;

namespace AdvertApi.Models
{
    public class Building
    {
        public int IdBuilding { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public decimal Height { get; set; }

        public virtual ICollection<Campaing> CampaingsFrom { get; set; }
        public virtual ICollection<Campaing> CampaingsTo { get; set; }
    }
}
