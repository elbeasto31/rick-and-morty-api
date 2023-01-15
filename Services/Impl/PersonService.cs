using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RickAndMortyAPI.Models;
using RickAndMortyAPI.Models.ResponseModels;
using RickAndMortyAPI.Services.Interfaces;
using static RickAndMortyAPI.Utils.RestClient;

namespace RickAndMortyAPI.Services.Impl
{
    public class PersonService : IPersonService
    {
        private const string GetPersonUrl = "https://rickandmortyapi.com/api/character/?name={0}";
        
        public async Task<Person> GetPerson(string name)
        {
            var requestUrl = string.Format(GetPersonUrl, name);
            var response = await Get<PersonResponseModel>(requestUrl);

            return  response.Results.First();
        }
    }
}