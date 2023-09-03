using FluentAssertions;
using MyShop.Application.Services.Managers;
using MyShop.Application.Services.Managers.Queries.GetManager;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Persistence.Managers;
using MyShop.Common.Tests;

namespace MyShop.Application.UnitTests.Managers.Queries
{
    public class GetManagerQueryHandlerTests : TestHelp<IManagerRepository>
    {
        public GetManagerQueryHandlerTests()
        {
            Repository = new ManagerRepository(Context);
        }

        [Fact]
        public async Task Handle_should_return_manager_successful()
        {
            var manager = Manager.Create("John", "Doe", 35, "09177870290").Value;
            Context.SaveEntity(manager);
            var query = new GetManagerQuery(manager.Id);
            var handler = new GetManagerQueryHandler(Repository);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Value.FirstName.Should().Be(manager.FirstName);
            result.Value.LastName.Should().Be(manager.LastName);
            result.Value.Age.Should().Be(manager.Age.Value);
            result.Value.MobileNumber.Should().Be(manager.MobileNumber.Value);
        }
    }
}
