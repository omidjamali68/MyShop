using MyShop.Domain.AspIdentities.Dtos;
using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.AspIdentities.ApplicationUsers.Contracts
{
    public interface IApplicationUserService : IService
    {
        Task<UserStatus> LoginAsync(LoginDto loginDto);
        Task<UserStatus> RegistrationAsync(RegisterUserDto registerUserDto);
        Task LogooutAsync();
    }
}
