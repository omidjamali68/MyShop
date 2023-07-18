using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Domain.AspIdentities.Dtos
{
    public class LoginDto
    {
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "نام کاربری وارد نشده است")]
        [RegularExpression(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$",
            ErrorMessage = "نام کاربری فقط میتواند شامل عدد باشد")]
        [MinLength(10, ErrorMessage = "حداقل کلمه عبور باید 10 کاراکتر باشد")]
        public string UserName { get; set; }
        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور وارد نشده است")]
        [MinLength(8, ErrorMessage = "حداقل کلمه عبور باید 8 کاراکتر باشد")]
        public string Password { get; set; }
    }
}
