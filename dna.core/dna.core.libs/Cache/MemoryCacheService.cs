using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Cache
{
    public class MemoryCacheService : ICacheService
    {
        protected IMemoryCache Cache;
        private static int DefaultCacheDuration => 60;

        public MemoryCacheService(IMemoryCache cache)
        {
            Cache = cache;
        }
        public T Get<T>(string key) where T : class
        {
            object result;
            if ( Cache.TryGetValue(key, out result) )
            {
                return result as T;
            }
            return null;
        }

        public void Store(string key, object content)
        {
            Store(key, content, DefaultCacheDuration);
        }

        public void Store(string key, object content, int duration)
        {
            object cached;
            if ( Cache.TryGetValue(key, out cached) )
            {
                Cache.Remove(key);
            }

            Cache.Set(key, content,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(duration),
                    Priority = CacheItemPriority.Low
                });
        }
    }
}
