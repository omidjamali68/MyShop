using Cooking.Infrastructure.Test;
using MyShop.Application.Services.Shops;
using MyShop.Domain;
using MyShop.Persistence.Shops;
using MyShop.Persistence;
using MyShop.Application.Services.Shops.Commands.Update;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;
using FluentAssertions;

namespace MyShop.Application.UnitTests.Shops.Commands
{
    public class ChangeShopStatusCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeShopStatusCommandHandlerTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<ApplicationDbContext>();
            _shopRepository = new ShopRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public async Task Handle_should_return_success_result()
        {
            var shop = Shop.Create("تست", "شیراز", true);
            _context.SaveEntity(shop.Value);
            var command = new ChangeShopStatusCommand(shop.Value.Id);
            var handler = new ChangeShopStatusCommandHandler(_shopRepository, _unitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            var dbExpected = _context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.IsActive.Should().BeFalse();
        }

        [Theory]
        [InlineData(-1)]
        public async Task Handle_should_return_not_found_error(int invalidId)
        {            
            var command = new ChangeShopStatusCommand(invalidId);
            var handler = new ChangeShopStatusCommandHandler(_shopRepository, _unitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Shop.ChangeStatus.ShopNotFound");
            var dbExpected = _context.Shops.SingleOrDefault(x => x.Id == invalidId);
            dbExpected.Should().BeNull();
        }
    }
}
