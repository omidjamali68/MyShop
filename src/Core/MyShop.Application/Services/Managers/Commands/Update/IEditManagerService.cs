using MyShop.Common.Dto;
using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Managers.Commands.Update
{
    public interface IEditManagerService : IService
    {
        Task<ServiceResultDto> Execute(EditManagerDto dto);
    }
}
