using Cooking.Infrastructure.Test;
using FluentAssertions;
using MyShop.Application.Services.Managers;
using MyShop.Application.Services.Managers.Commands.Delete;
using MyShop.Application.Services.Managers.Commands.Update;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Persistence.Managers;

namespace MyShop.Application.UnitTests.Managers.Commands
{
    public class EditManagerCommandHandlerTests : TestHelp<IManagerRepository>
    {
        public EditManagerCommandHandlerTests() 
        {
            Repository = new ManagerRepository(Context);
        }

        [Fact]
        public async Task Handle_should_edit_manager_success()
        {
            var manager = Manager.Create("John", "Doe", 35, "09172223344").Value;
            Context.SaveEntity(manager);
            var command = new EditManagerCommand(manager.Id, "Jordan", "Baros", 31);
            var handler = new EditManagerCommandHandler(Repository, UnitOfWork);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            var dbExpected = Context.Managers.Should().Contain(
                x => x.Id == manager.Id &&
                x.MobileNumber == manager.MobileNumber &&
                x.FirstName == command.FirstName &&
                x.LastName == command.LastName &&
                x.Age.Value == command.Age);
        }

        [Theory]
        [InlineData(-1)]
        public async Task Handle_should_return_notFound_error(int invalidId)
        {
            var manager = Manager.Create("John", "Doe", 35, "09172223344").Value;
            Context.SaveEntity(manager);
            var command = new EditManagerCommand(invalidId, "Jordan", "Baros", 31);
            var handler = new EditManagerCommandHandler(Repository, UnitOfWork);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Manager.Edit.NotFound");
            var dbExpected = Context.Managers.Should().Contain(
                x => x.Id == manager.Id &&
                x.MobileNumber == manager.MobileNumber &&
                x.FirstName == manager.FirstName &&
                x.LastName == manager.LastName &&
                x.Age == manager.Age);
        }

        [Theory]
        [InlineData(12)]
        public async Task Handle_should_return_notValidAge_error(byte invalidAge)
        {
            var manager = Manager.Create("John", "Doe", 35, "09172223344").Value;
            Context.SaveEntity(manager);
            var command = new EditManagerCommand(manager.Id, "Jordan", "Baros", invalidAge);
            var handler = new EditManagerCommandHandler(Repository, UnitOfWork);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Manager.Age.MinAgeError");
            var dbExpected = Context.Managers.Should().Contain(
                x => x.Id == manager.Id &&
                x.MobileNumber == manager.MobileNumber &&
                x.FirstName == manager.FirstName &&
                x.LastName == manager.LastName &&
                x.Age == manager.Age);
        }
    }
}
