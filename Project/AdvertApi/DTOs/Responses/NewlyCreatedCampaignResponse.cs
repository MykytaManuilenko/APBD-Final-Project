using System;
using AdvertApi.Models;

namespace AdvertApi.DTOs.Responses
{
    public class NewlyCreatedCampaignResponse
    {
        public int IdCampaing { get; set; }
        public int IdClient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }
        public int FromIdBuilding { get; set; }
        public int ToIdBuilding { get; set; }

        public Banner Banner1 { get; set; }
        public Banner Banner2 { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
