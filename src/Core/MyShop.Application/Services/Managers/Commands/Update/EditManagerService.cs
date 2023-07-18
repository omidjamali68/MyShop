using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Dto;
using MyShop.Common.Messages;

namespace MyShop.Application.Services.Managers.Commands.Update
{
    public class EditManagerService : IEditManagerService
    {
        private readonly IApplicationDbContext _context;

        public EditManagerService(IApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task<ServiceResultDto> Execute(EditManagerDto dto)
        {
            var result = ServiceResultDto.Create();
            var manager = await _context.Managers.FindAsync(dto.Id);

            manager?.Update(dto.FirstName, dto.LastName, dto.Age);

            if (!manager.Result.IsSucces)
            {
                result.SetErrors(manager.Result.Messeges);
                return result;
            }

            await _context.SaveChangesAsync();

            result.SuccessFully(
                string.Format(Notifications.SuccessfullyUpdated, DataDictionary.Manager));
            return result;
        }
    }
}
