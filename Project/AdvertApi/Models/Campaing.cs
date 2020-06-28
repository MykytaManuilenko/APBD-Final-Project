using System;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public class Campaing
    {
        public int IdCampaing { get; set; }
        public int IdClient { get; set; }
        public virtual Client Client { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }

        public int FromIdBuilding { get; set; }
        public virtual Building FromBuilding { get; set; }

        public int ToIdBuilding { get; set; }
        public virtual Building ToBuilding { get; set; }

        public virtual ICollection<Banner> Banners { get; set; }
    }
}
