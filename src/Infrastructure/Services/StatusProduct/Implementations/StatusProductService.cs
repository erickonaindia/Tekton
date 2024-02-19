using Microsoft.Extensions.Caching.Memory;
using Tekton.Application.Common.Interfaces;
using Tekton.Application.Common.Models;

namespace Tekton.Infrastructure.Services.StatusProduct.Implementations;
public class StatusProductService : IStatusProductService
{
    private readonly IMemoryCache _memoryCache;
    private readonly Dictionary<int, string> _statusDictionary;

    public StatusProductService(
        IMemoryCache memoryCache
        )
    {
        _memoryCache = memoryCache;
        _statusDictionary = StatusDictionary.GetStatus();
    }

    public Task<Dictionary<int, string>> GetProductStatus()
    {
        string cacheKey = $"Status_Products";
        var result = new Dictionary<int, string>();
        
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        if (_memoryCache.TryGetValue(cacheKey, out result!))
        {
            return Task.FromResult(result!);
        }

        result = _statusDictionary;
        _memoryCache.Set(cacheKey, result, cacheEntryOptions);

        return Task.FromResult(result!);
    }
}
