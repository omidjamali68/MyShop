using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Managers.Queries.GetManagers
{
    public interface IGetManagersService : IService
    {
        Task<ResultGetManagersDto> Execute(RequestGetManagersDto dto);
    }
}
