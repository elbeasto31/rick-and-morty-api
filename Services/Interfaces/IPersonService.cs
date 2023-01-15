using System.Threading.Tasks;
using RickAndMortyAPI.Models;

namespace RickAndMortyAPI.Services.Interfaces
{
    public interface IPersonService
    {
        Task<Person> GetPerson(string name);
    }
}