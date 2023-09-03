using MyShop.Application.Services.Products;
using MyShop.Application.Services.Products.Commands.Add;
using MyShop.Domain.Aggregates.Products.Entities;
using MyShop.Persistence.Products;
using MyShop.Common.Tests;
using Microsoft.AspNetCore.Http;
using Moq;
using FluentAssertions;

namespace MyShop.Application.UnitTests.Products.Commands
{
    public class AddProductCommandHandlerTests : TestHelp<IProductRepository>
    {
        public AddProductCommandHandlerTests()
        {
            Repository = new ProductRepoository(Context);
        }

        [Fact]
        public async Task Handle_should_add_product_successful()
        {
            var features = new List<AddNewProduct_FeaturesDto>
            {
                new AddNewProduct_FeaturesDto { DisplayName = "Beuty", Value = "100" }
            };
            var category = Category.Create("Cloths").Value;
            Context.SaveEntity(category);
            var files = new List<IFormFile>
            {
                CreateMockFile()
            };            
            var command = new AddProductCommand(
                "Pants", "Lee", "It's very good", 50000, 10, category.Id, true, files, features);
            var handler = new AddProductCommandHandler(Repository, UnitOfWork);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            var dbExpected = Context.Products.Single(x => x.Id == result.Value);
            dbExpected.Name.Value.Should().Be(command.Name);
            dbExpected.Brand.Should().Be(command.Brand);
            dbExpected.Price.Should().Be(command.Price);
            dbExpected.Description.Should().Be(command.Description);
            dbExpected.Quantity.Value.Should().Be(command.Quantity);
            dbExpected.CategoryId.Should().Be(command.CategoryId);
            dbExpected.Displayed.Should().Be(command.Displayed);
            dbExpected.ProductFeatures.Should().Contain(
                x => x.DisplayName == features.First().DisplayName &&
                x.Value == features.First().Value);
            //dbExpected.ProductImages.Should().Contain(
            //    x => x.Src == files.First().FileName);            
        }

        private IFormFile CreateMockFile()
        {
            var fileMock = new Mock<IFormFile>();            
            var content = "Hello World from a Fake File";                        
            string fileName = "test.jpg";            
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            return fileMock.Object;
        }
    }
}
