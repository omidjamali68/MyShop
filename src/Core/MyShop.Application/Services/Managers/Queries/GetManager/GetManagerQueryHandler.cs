using MyShop.Application.Interfaces;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Managers.Queries.GetManager
{
    internal sealed class GetManagerQueryHandler : IQueryHandler<GetManagerQuery, GetManagerResponse>
    {
        private readonly IManagerRepository _managerRepository;

        public GetManagerQueryHandler(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<Result<GetManagerResponse>> Handle(
            GetManagerQuery request, CancellationToken cancellationToken)
        {
            return await _managerRepository.GetById(request.ManagerId);
        }
        
    }
}
