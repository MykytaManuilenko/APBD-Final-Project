using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTOs.Requests
{
    public class RegistrateNewClientRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
