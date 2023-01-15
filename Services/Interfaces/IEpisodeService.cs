using System.Threading.Tasks;
using RickAndMortyAPI.Models;

namespace RickAndMortyAPI.Services.Interfaces
{
    public interface IEpisodeService
    {
        Task<Episode> GetEpisode(string name);
    }
}