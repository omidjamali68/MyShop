using FluentAssertions;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.Commands.Create;
using MyShop.Application.UnitTests;
using MyShop.Domain.SeedWork;
using MyShop.Persistence.Shops;

namespace MyShop.UnitTests.Shops.Commands
{
    public class CreateShopCommandHandlerTests : TestHelp<IShopRepository>
    {        
        public CreateShopCommandHandlerTests()
        {
            Repository = new ShopRepository(Context);
        }

        [Fact]
        public async Task Handle_should_return_success_result()
        {
            var command = new CreateShopCommand("فروشگاه تستی", "شیراز خ زند");
            var handler = new CreateShopCommandHandler(Repository, UnitOfWork);

            Result<int> result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Error.Message.Should().BeEmpty();            
            var dbExpected = Context.Shops
                .FirstOrDefault(x => x.Id == result.Value);
            dbExpected.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_should_fail_when_Name_is_empty()
        {
            var command = new CreateShopCommand("","تست آباد");
            var handler = new CreateShopCommandHandler(Repository, UnitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();            
            result.Error.Code.Should().Be("Shop.Name.Empty");
            var dbExpected = Context.Shops
                .FirstOrDefault(x => (string)x.Name == command.Name && x.Address == command.Address);
            dbExpected.Should().BeNull();  
        }
        
    }
}
