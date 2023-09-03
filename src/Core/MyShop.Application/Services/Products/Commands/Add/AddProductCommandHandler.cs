using Microsoft.AspNetCore.Http;
using MyShop.Application.Interfaces;
using MyShop.Domain;
using MyShop.Domain.Aggregates.Products;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Products.Commands.Add
{
    internal sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {            
            var product = Product.Create(
                request.Name, 
                request.Quantity, 
                request.Brand, 
                request.Description, 
                request.Price, 
                request.Displayed, 
                request.CategoryId);

            if (product.IsFailure)
            {
                return Result.Failure<int>(product.Error);
            }

            foreach(var item in request.Images)
            {
                var fileUrl = UploadFile(item);
                var result = product.Value.AssignImage(fileUrl);
                if (result.IsFailure)
                {
                    return Result.Failure<int>(result.Error);
                }
            }

            foreach (var item in request.Features)
            {                
                var feature = new KeyValuePair<string, string> (
                     item.DisplayName, item.Value
                );

                var result = product.Value.AddFeature(feature);

                if (result.IsFailure)
                {
                    return Result.Failure<int>(result.Error);
                }
            }

            await _repository.Add(product.Value);
            await _unitOfWork.SaveChangeAsync();

            return Result.Success(product.Value.Id);
        }

        private string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return string.Empty;
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return folder + fileName;
            }
            return string.Empty;
        }       
    }
}
