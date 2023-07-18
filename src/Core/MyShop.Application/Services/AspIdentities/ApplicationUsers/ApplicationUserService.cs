using Microsoft.AspNetCore.Identity;
using MyShop.Application.Services.AspIdentities.ApplicationUsers.Contracts;
using MyShop.Domain.AspIdentities;
using MyShop.Domain.AspIdentities.Dtos;
using System.Security.Claims;

namespace MyShop.Application.Services.AspIdentities.ApplicationUsers
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUserService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<UserStatus> LoginAsync(LoginDto loginDto)
        {
            var status = new UserStatus();

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                status.Code = 0;
                status.Message = "نام کاربری صحیح نمیباشد";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                status.Code = 0;
                status.Message = "کلمه عبور صحیح نمیباشد";
                return status;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(
                user, loginDto.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                status.Code = 1;
                status.Message = "خوش آمدید";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.Code = 0;
                status.Message = "کاربر در حالت تعلیق میباشد";
                return status;
            }
            else
            {
                status.Code = 0;
                status.Message = "مشکل در ورود کاربر";
                return status;
            }
        }

        public async Task LogooutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserStatus> RegistrationAsync(RegisterUserDto registerUserDto)
        {
            var status = new UserStatus();

            var userExists = await _userManager.FindByNameAsync(registerUserDto.UserName);

            if (userExists != null)
            {
                status.Code = 0;
                status.Message = "کاربر موجود میباشد";
                return status;
            }

            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = registerUserDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            if (!result.Succeeded)
            {
                status.Code = 0;
                status.Message = "در ایجاد کاربر مشکلی رخ داده است.";
                return status;
            }

            if (!await _roleManager.RoleExistsAsync(registerUserDto.Role))
                await _roleManager.CreateAsync(new ApplicationRole(registerUserDto.Role));

            if (await _roleManager.RoleExistsAsync(registerUserDto.Role))
                await _userManager.AddToRoleAsync(user, registerUserDto.Role);

            status.Code = 1;
            status.Message = "کاربر با موفقیت ثبت شد.";
            return status;
        }
    }
}
