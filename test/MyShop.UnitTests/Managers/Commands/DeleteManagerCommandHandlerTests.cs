using FluentAssertions;
using MyShop.Application.Services.Managers;
using MyShop.Application.Services.Managers.Commands.Delete;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Persistence.Managers;
using MyShop.Common.Tests;

namespace MyShop.Application.UnitTests.Managers.Commands
{
    public class DeleteManagerCommandHandlerTests : TestHelp<IManagerRepository>
    {
        public DeleteManagerCommandHandlerTests() 
        {
            Repository = new ManagerRepository(Context);
        }

        [Fact]
        public async Task Handle_should_delete_manager_successful()
        {
            var manager = Manager.Create("John", "Doe", 35, "09172223344").Value;
            Context.SaveEntity(manager);
            var command = new DeleteManagerCommand(manager.Id);
            var handler = new DeleteManagerCommandHandler(Repository, UnitOfWork);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            var dbExpected = Context.Managers.SingleOrDefault(x => x.Id == manager.Id);
            dbExpected.Should().BeNull();
        }

        [Theory]
        [InlineData(-1)]
        public async Task Handle_should_return_notFound_error(int invalidId)
        {
            var manager = Manager.Create("John", "Doe", 35, "09172223344").Value;
            Context.SaveEntity(manager);
            var command = new DeleteManagerCommand(invalidId);
            var handler = new DeleteManagerCommandHandler(Repository, UnitOfWork);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Manager.Delete.NotFound");
            var dbExpected = Context.Managers.SingleOrDefault(x => x.Id == manager.Id);
            dbExpected.Should().NotBeNull();
        }
    }
}
