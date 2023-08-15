using Cooking.Infrastructure.Test;
using FluentAssertions;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.Commands.Delete;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;
using MyShop.Persistence.Shops;

namespace MyShop.Application.UnitTests.Shops.Commands
{
    public class DeleteShopCommandHandlerTests : TestHelp<IShopRepository>
    {        
        public DeleteShopCommandHandlerTests()
        {
            Repository = new ShopRepository(Context);
        }

        [Fact]
        public async Task Handle_should_return_success_result()
        {
            var shop = Shop.Create("فروشگاه تستی", "شیراز خ زند", true);
            Context.SaveEntity(shop.Value);
            var command = new DeleteShopCommand(shop.Value.Id);
            var handler = new DeleteShopCommandHandler(Repository, UnitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Error.Message.Should().BeEmpty();
        }

        [Theory]
        [InlineData(-1)]
        public async Task Handle_should_return_not_found_error(int invalidId)
        {            
            var command = new DeleteShopCommand(invalidId);
            var handler = new DeleteShopCommandHandler(Repository, UnitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Shop.Delete.NotFound");            
        }
    }
}
