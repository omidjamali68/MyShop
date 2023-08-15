using MyShop.Application.Services.Managers;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.ShopManagers.Commands.Add;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Persistence.Shops;
using Cooking.Infrastructure.Test;
using FluentAssertions;
using MyShop.Persistence.Managers;
using MyShop.Domain.Aggregates.Managers;

namespace MyShop.Application.UnitTests.Shops.ShopManagers.Commands
{
    public class AddShopManagerCommandHandlerTests : TestHelp<IShopRepository>
    {
        private readonly IManagerRepository _managerRepository;
        public AddShopManagerCommandHandlerTests()
        {
            Repository = new ShopRepository(Context);
            _managerRepository = new ManagerRepository(Context);
        }

        [Fact]
        public async Task Handle_should_add_new_manger_to_shop()
        {
            var shop = Shop.Create("shop1", "tehran");
            Context.SaveEntity(shop.Value);
            var command = new AddShopMangerCommand(shop.Value.Id, "jhon","Doe",31,"09177870290");
            var handler = new AddShopManagerCommandHandler(Repository, UnitOfWork, _managerRepository);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.ShopManagers.Should().Contain(
                x => x.Manager.FirstName == command.FirstName &&
                x.Manager.LastName == command.LastName &&
                x.Manager.Age.Value == command.Age);
        }

        [Theory]
        [InlineData(-1)]
        public async Task Handle_should_return_shop_not_found_error(int invalidId)
        {
            var shop = Shop.Create("shop1", "tehran");
            Context.SaveEntity(shop.Value);
            var command = new AddShopMangerCommand(invalidId, "jhon", "Doe", 31, "09177870290");
            var handler = new AddShopManagerCommandHandler(Repository, UnitOfWork, _managerRepository);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("ShopManger.Add.NotFound");
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.ShopManagers.Should().NotContain(
                x => x.Manager.FirstName == command.FirstName &&
                x.Manager.LastName == command.LastName &&
                x.Manager.Age.Value == command.Age);
        }

        [Theory]
        [InlineData("091777777777777")]
        public async Task Handle_should_return_invalid_mobile_number_fixLenError(string invalidMobile)
        {
            var shop = Shop.Create("shop1", "tehran");
            Context.SaveEntity(shop.Value);
            var command = new AddShopMangerCommand(shop.Value.Id, "jhon", "Doe", 31, invalidMobile);
            var handler = new AddShopManagerCommandHandler(Repository, UnitOfWork, _managerRepository);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Manager.MobileNumber.FixLenError");
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.ShopManagers.Should().NotContain(
                x => x.Manager.FirstName == command.FirstName &&
                x.Manager.LastName == command.LastName &&
                x.Manager.Age.Value == command.Age);
        }

        [Theory]
        [InlineData("0917")]
        public async Task Handle_should_return_invalid_mobile_number_fixLenError2(string invalidMobile)
        {
            var shop = Shop.Create("shop1", "tehran");
            Context.SaveEntity(shop.Value);
            var command = new AddShopMangerCommand(shop.Value.Id, "jhon", "Doe", 31, invalidMobile);
            var handler = new AddShopManagerCommandHandler(Repository, UnitOfWork, _managerRepository);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Manager.MobileNumber.FixLenError");
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.ShopManagers.Should().NotContain(
                x => x.Manager.FirstName == command.FirstName &&
                x.Manager.LastName == command.LastName &&
                x.Manager.Age.Value == command.Age);
        }

        [Theory]
        [InlineData("08176662255")]
        public async Task Handle_should_return_invalid_mobile_number_regexError(string invalidMobile)
        {
            var shop = Shop.Create("shop1", "tehran");
            Context.SaveEntity(shop.Value);
            var command = new AddShopMangerCommand(shop.Value.Id, "jhon", "Doe", 31, invalidMobile);
            var handler = new AddShopManagerCommandHandler(Repository, UnitOfWork, _managerRepository);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Manager.MobileNumber.RegexError");
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.ShopManagers.Should().NotContain(
                x => x.Manager.FirstName == command.FirstName &&
                x.Manager.LastName == command.LastName &&
                x.Manager.Age.Value == command.Age);
        }

        [Fact]
        public async Task Handle_should_return_success_when_mangerExist()
        {
            var shop = Shop.Create("shop1", "tehran");
            Context.SaveEntity(shop.Value);
            var manager = Manager.Create("Jordan", "Baros", 35, "09178882255");
            Context.SaveEntity(manager.Value);
            var command = new AddShopMangerCommand(
                shop.Value.Id, 
                manager.Value.FirstName, 
                manager.Value.LastName, 
                manager.Value.Age.Value, 
                manager.Value.MobileNumber.Value);
            var handler = new AddShopManagerCommandHandler(Repository, UnitOfWork, _managerRepository);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.ShopManagers.Should().Contain(
                x => x.Manager.FirstName == command.FirstName &&
                x.Manager.LastName == command.LastName &&
                x.Manager.Age.Value == command.Age);
        }
    }
}
