using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdvertApi.DTOs.Responses.ModelsResponse;

namespace AdvertApi.DTOs.Responses
{
    public class CampaignResponse
    {
        public int IdCampaing { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }
        public int FromIdBuilding { get; set; }
        public int ToIdBuilding { get; set; }
        public List<BannerModelResponse> Banners { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
