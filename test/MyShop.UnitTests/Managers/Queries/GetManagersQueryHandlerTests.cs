using Cooking.Infrastructure.Test;
using FluentAssertions;
using MyShop.Application.Services.Managers;
using MyShop.Application.Services.Managers.Queries.GetManagers;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Persistence.Managers;

namespace MyShop.Application.UnitTests.Managers.Queries
{
    public class GetManagersQueryHandlerTests : TestHelp<IManagerRepository>
    {
        public GetManagersQueryHandlerTests()
        {
            Repository = new ManagerRepository(Context);
        }

        [Fact]
        public async Task Handle_should_return_managers_successful()
        {
            var managerJohn = Manager.Create("John", "Doe", 31, "09177870290").Value;
            Context.SaveEntity(managerJohn);
            var managerTim = Manager.Create("Tim", "Baros", 35, "09372216032").Value;
            Context.SaveEntity(managerTim);
            var query = new GetManagersQuery(null, 1);
            var handler = new GetManagersQueryHandler(Repository);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Value.Rows.Should().Be(2);
            result.Value.Data.Should().Contain(
                x => x.Id == managerJohn.Id &&
                x.FirstName == managerJohn.FirstName &&
                x.LastName == managerJohn.LastName &&
                x.Age == managerJohn.Age.Value &&
                x.Mobile == managerJohn.MobileNumber.Value);
            result.Value.Data.Should().Contain(
                x => x.Id == managerTim.Id &&
                x.FirstName == managerTim.FirstName &&
                x.LastName == managerTim.LastName &&
                x.Age == managerTim.Age.Value &&
                x.Mobile == managerTim.MobileNumber.Value);
        }

        [Fact]
        public async Task Handle_should_return_managers_with_searchKey_successful()
        {
            var managerJohn = Manager.Create("John", "Doe", 31, "09177870290").Value;
            Context.SaveEntity(managerJohn);
            var managerTim = Manager.Create("Tim", "Baros", 35, "09372216032").Value;
            Context.SaveEntity(managerTim);
            var query = new GetManagersQuery("jo", 1);
            var handler = new GetManagersQueryHandler(Repository);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Value.Rows.Should().Be(1);
            result.Value.Data.Should().Contain(
                x => x.Id == managerJohn.Id &&
                x.FirstName == managerJohn.FirstName &&
                x.LastName == managerJohn.LastName &&
                x.Age == managerJohn.Age.Value &&
                x.Mobile == managerJohn.MobileNumber.Value);
        }

        [Fact]
        public async Task Handle_should_return_managers_with_persion_searchKey_successful()
        {
            var managerJohn = Manager.Create("علی", "حسینی", 31, "09177870290").Value;
            Context.SaveEntity(managerJohn);
            var managerTim = Manager.Create("جعفر", "سلمانی", 35, "09372216032").Value;
            Context.SaveEntity(managerTim);
            var query = new GetManagersQuery("عل", 1);
            var handler = new GetManagersQueryHandler(Repository);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Value.Rows.Should().Be(1);
            result.Value.Data.Should().Contain(
                x => x.Id == managerJohn.Id &&
                x.FirstName == managerJohn.FirstName &&
                x.LastName == managerJohn.LastName &&
                x.Age == managerJohn.Age.Value &&
                x.Mobile == managerJohn.MobileNumber.Value);
        }
    }
}
