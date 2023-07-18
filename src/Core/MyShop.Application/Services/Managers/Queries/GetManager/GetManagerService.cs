using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Managers.Queries.GetManager
{
    public class GetManagerService : IGetManagerService
    {
        private readonly IApplicationDbContext _context;

        public GetManagerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultGetManagerDto> Execute(GetManagerDto dto)
        {
            var manager = await _context.Managers.FindAsync(dto.ManagerId);
            
            if (manager is null)            
                return new ResultGetManagerDto();
            
            return new ResultGetManagerDto
            {
                FirstName = manager.FirstName,
                LastName = manager.LastName,
                Age = manager.Age.Value,
                MobileNumber = manager.MobileNumber.Value,
            };
        }
    }
}
