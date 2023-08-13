using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Managers.Commands.Update
{
    public sealed record EditManagerCommand(int Id, string FirstName, string LastName, byte Age) : ICommand;    
}
