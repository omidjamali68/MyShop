using Microsoft.Extensions.Caching.Distributed;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.Queries.GetShops;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;
using Newtonsoft.Json;

namespace MyShop.Persistence.Shops
{
    public class CachedShopRepository : IShopRepository
    {
        private readonly IShopRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _dbContext;

        public CachedShopRepository(
            IShopRepository shopRepository,
            IDistributedCache distributedCache,
            ApplicationDbContext dbContext)
        {
            _decorated = shopRepository;
            _distributedCache = distributedCache;
            _dbContext = dbContext;
        }

        public void Delete(Shop shop)
        {
            _decorated.Delete(shop);
        }

        public async Task<Shop?> FindById(int id)
        {
            string key = $"shop-{id}";

            string? cachedShop = await _distributedCache.GetStringAsync(key);
            Shop? shop;
            if (string.IsNullOrEmpty(cachedShop))
            {
                shop = await _decorated.FindById(id);

                if (shop is null)
                {
                    return shop;
                }

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(shop));

                return shop;
            }

            shop = JsonConvert.DeserializeObject<Shop>(
                cachedShop,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });

            if (shop is not null)
            {
                _dbContext.Set<Shop>().Attach(shop);
            }

            return shop;
        }

        public async Task<Result<GetShopsResponse>?> GetAll(string? searchKey, int page)
        {
            string key = $"get-all-shops";

            string? cachedShops = await _distributedCache.GetStringAsync(key);

            Result<GetShopsResponse>? shops;
            if (string.IsNullOrEmpty(cachedShops))
            {
                shops = await _decorated.GetAll(searchKey, page);

                if (shops is null)
                {
                    return shops;
                }

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(shops));

                return shops;
            }

            shops = JsonConvert.DeserializeObject<Result<GetShopsResponse>>(
                cachedShops,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });

            //if (shops is not null)
            //{
            //    _dbContext.Set<Shop>().AttachRange(shops);
            //}

            return shops;
        }

        public async Task Insert(Shop shop)
        {
            await _decorated.Insert(shop);
        }
    }
}
