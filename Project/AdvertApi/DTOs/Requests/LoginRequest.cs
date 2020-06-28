using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTOs.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
