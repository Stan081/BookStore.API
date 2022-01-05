using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Repository.Dtos
{
    public class SignInDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}

