using System;
using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTOs.Requests
{
    public class CreateNewCampaignRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public int IdClient { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public decimal PricePerSquareMeter { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public int FromIdBuilding { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public int ToIdBuilding { get; set; }
    }
}
