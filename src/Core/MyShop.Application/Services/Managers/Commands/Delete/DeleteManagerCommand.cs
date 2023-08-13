using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Managers.Commands.Delete
{
    public sealed record DeleteManagerCommand(int ManagerId) : ICommand;        
}
