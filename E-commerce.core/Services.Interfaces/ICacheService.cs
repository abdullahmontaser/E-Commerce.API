using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Services.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheKeyAsync(string cacheKey,object response ,TimeSpan expirtTime);
        
        Task<string> GetCacheKeyAsync(string cacheKey);
    }
}
