using MyShop.Application.Services.Managers.Queries.GetManager;
using MyShop.Application.Services.Managers.Queries.GetManagers;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.Core.RepositoryContracts;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Application.Services.Managers
{
    public interface IManagerRepository : IRepository
    {
        void Delete(Manager manager);
        Task<Manager?> FindById(int id);
        Task<Manager?> FindByMobileNumber(MobileNumber value);
        Task<Result<GetManagersResponse>> GetAll(string? searchKey, int page);
        Task<Result<GetManagerResponse>> GetById(int id);
    }
}
