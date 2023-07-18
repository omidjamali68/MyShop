using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Managers.Queries.GetManager
{
    public interface IGetManagerService : IService
    {
        Task<ResultGetManagerDto> Execute(GetManagerDto dto);
    }
}
