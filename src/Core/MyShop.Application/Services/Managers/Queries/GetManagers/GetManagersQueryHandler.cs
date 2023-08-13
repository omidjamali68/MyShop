using MyShop.Application.Interfaces;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Managers.Queries.GetManagers
{
    public sealed class GetManagersQueryHandler : IQueryHandler<GetManagersQuery, GetManagersResponse>
    {
        private readonly IManagerRepository _managerRepository;

        public GetManagersQueryHandler(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<Result<GetManagersResponse>> Handle(
            GetManagersQuery request, CancellationToken cancellationToken)
        {
            return await _managerRepository.GetAll(request.searchKey, request.page);
        }
    }
}
