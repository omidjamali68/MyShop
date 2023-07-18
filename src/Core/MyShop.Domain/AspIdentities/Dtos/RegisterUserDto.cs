using System.ComponentModel.DataAnnotations;

namespace MyShop.Domain.AspIdentities.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [RegularExpression("0123456789")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? Role { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
