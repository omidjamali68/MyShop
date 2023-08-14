using Cooking.Infrastructure.Test;
using FluentAssertions;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.Commands.Create;
using MyShop.Domain;
using MyShop.Domain.SeedWork;
using MyShop.Persistence;
using MyShop.Persistence.Shops;

namespace MyShop.UnitTests.Shops.Commands
{
    public class CreateShopCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateShopCommandHandlerTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<ApplicationDbContext>();
            _shopRepository = new ShopRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public async Task Handle_should_return_success_result()
        {
            var command = new CreateShopCommand("فروشگاه تستی", "شیراز خ زند");
            var handler = new CreateShopCommandHandler(_shopRepository, _unitOfWork);

            Result<int> result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Error.Message.Should().BeEmpty();            
            var dbExpected = _context.Shops
                .FirstOrDefault(x => x.Id == result.Value);
            dbExpected.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_should_fail_when_Name_is_empty()
        {
            var command = new CreateShopCommand("","تست آباد");
            var handler = new CreateShopCommandHandler(_shopRepository, _unitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();            
            result.Error.Code.Should().Be("Shop.Name.Empty");
            var dbExpected = _context.Shops
                .FirstOrDefault(x => (string)x.Name == command.Name && x.Address == command.Address);
            dbExpected.Should().BeNull();  
        }
        
    }
}
