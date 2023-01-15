using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RickAndMortyAPI.Models;
using RickAndMortyAPI.Models.ResponseModels;
using RickAndMortyAPI.Services.Interfaces;
using static RickAndMortyAPI.Utils.RestClient;

namespace RickAndMortyAPI.Services.Impl
{
    public class EpisodeService : IEpisodeService
    {
        private const string GetEpisodeUrl = "https://rickandmortyapi.com/api/episode/?name={0}";
        private readonly IMemoryCache _memoryCache;
        
        public EpisodeService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        public async Task<Episode> GetEpisode(string name)
        {
            if (_memoryCache.TryGetValue(name, out var result))
                return result as Episode;
            
            var requestUrl = string.Format(GetEpisodeUrl, name);
            var response = await Get<EpisodeResponseModel>(requestUrl);

            var episode = response.Results.First();
            _memoryCache.Set(name, episode);
            
            return  episode;
        }
    }
}