using FluentAssertions;
using Moq;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.Commands.Create;
using MyShop.Domain;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;

namespace MyShop.UnitTests.Shops.Commands
{
    public class CreateShopCommandHandlerTests
    {
        private readonly Mock<IShopRepository> _shopRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateShopCommandHandlerTests()
        {
            _shopRepository = new();
            _unitOfWork = new();
        }

        [Fact]
        public async Task Handle_should_return_success_result()
        {
            var command = new CreateShopCommand("فروشگاه تستی", "شیراز خ زند");
            var handler = new CreateShopCommandHandler(_shopRepository.Object, _unitOfWork.Object);

            Result result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Error.Message.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_should_fail_when_Name_is_empty()
        {
            var command = new CreateShopCommand("","تست آباد");
            var handler = new CreateShopCommandHandler(_shopRepository.Object, _unitOfWork.Object);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();            
            result.Error.Code.Should().Be("Shop.Name.Empty");
        }

        [Fact]
        public async Task Handle_should_call_addOnRepository_when_success()
        {
            var command = new CreateShopCommand("فروشگاه تستی", "تست آباد");
            var handler = new CreateShopCommandHandler(_shopRepository.Object, _unitOfWork.Object);

            Result result = await handler.Handle(command, default);

            _shopRepository.Verify(
                x => x.Insert(It.Is<Shop>(s => s.Name.Value == command.Name)), 
                Times.Once);
        }

        [Fact]
        public async Task Handle_should_not_call_unitOfWork_when_shop_is_not_valid()
        {
            var command = new CreateShopCommand("", "تست آباد");
            var handler = new CreateShopCommandHandler(_shopRepository.Object, _unitOfWork.Object);

            Result result = await handler.Handle(command, default);

            _unitOfWork.Verify(
                x => x.SaveChangeAsync(It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
