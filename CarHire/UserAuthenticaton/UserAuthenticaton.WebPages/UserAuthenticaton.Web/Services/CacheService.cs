using Microsoft.Extensions.Caching.Memory;
using System;
using UserAuthenticaton.Web.Utils;

namespace UserAuthenticaton.Web.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void SetToken(string userToken)
        {
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(8));

            memoryCache.Set(CacheKeys.USERTOKEN, userToken, cacheEntryOptions);
        }

        public string GetToken()
        {
            string token = "";
            if (!memoryCache.TryGetValue(CacheKeys.USERTOKEN, out token))
            {
            }

            return token;
        }

    }
}
