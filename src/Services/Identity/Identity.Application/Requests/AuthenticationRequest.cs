using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

