using MyShop.Common.Dto;
using MyShop.Domain.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services.Shops.ShopManagers.Commands.Add
{
    public interface IAddShopManagerService : IService
    {
        Task<ServiceResultDto> Execute(AddShopManagerDto dto);
    }
}
