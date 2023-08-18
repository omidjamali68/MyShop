using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Managers.Commands.Update
{
    internal sealed class EditManagerCommandHandler : ICommandHandler<EditManagerCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditManagerCommandHandler(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.FindById(request.Id);

            if (manager is null)
            {
                return Result.Failure(
                    Error.Create(
                        "Manager.Edit.NotFound",
                        string.Format(Validations.NotExist, DataDictionary.Manager)));
            }

            var result = manager.Update(request.FirstName, request.LastName, request.Age);

            if (result.IsFailure)
            {
                return result;
            }

            await _unitOfWork.SaveChangeAsync();

            return Result.Success();
        }
        
    }
}
