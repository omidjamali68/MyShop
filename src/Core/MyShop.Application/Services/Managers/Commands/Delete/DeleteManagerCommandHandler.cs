using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Managers.Commands.Delete
{
    public sealed class DeleteManagerCommandHandler : ICommandHandler<DeleteManagerCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteManagerCommandHandler(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.FindById(request.ManagerId);

            if (manager == null)
            {
                return Result.Failure(
                    Error.Create(
                        "Manager.Delete.NotFound",
                        string.Format(Validations.NotExist, DataDictionary.Manager)));
            }

            _managerRepository.Delete(manager);
            await _unitOfWork.SaveChangeAsync();

            return Result.Success();
        }
    }
}
