using Cooking.Infrastructure.Test;
using FluentAssertions;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.Commands.Delete;
using MyShop.Domain;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;
using MyShop.Persistence;
using MyShop.Persistence.Shops;

namespace MyShop.Application.UnitTests.Shops.Commands
{
    public class DeleteShopCommandHandlerTests
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public DeleteShopCommandHandlerTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<ApplicationDbContext>();
            _shopRepository = new ShopRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public async Task Handle_should_return_success_result()
        {
            var shop = Shop.Create("فروشگاه تستی", "شیراز خ زند", true);
            _context.SaveEntity(shop.Value);
            var command = new DeleteShopCommand(shop.Value.Id);
            var handler = new DeleteShopCommandHandler(_shopRepository, _unitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Error.Message.Should().BeEmpty();
        }
    }
}
