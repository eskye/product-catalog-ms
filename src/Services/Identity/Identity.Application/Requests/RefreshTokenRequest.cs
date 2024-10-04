using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Requests
{
    public class RefreshTokenRequest
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}

