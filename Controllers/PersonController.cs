using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RickAndMortyAPI.Filters.Exceptions;
using RickAndMortyAPI.Models.RequestModels;
using RickAndMortyAPI.Services.Interfaces;
using RickAndMortyAPI.Utils.Constants;

namespace RickAndMortyAPI.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    [ResponseCache(CacheProfileName = "Default")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IEpisodeService _episodeService;
        
        public PersonController(IPersonService personService, IEpisodeService episodeService)
        {
            _personService = personService;
            _episodeService = episodeService;
        }
        
        #region Actions

        [HttpGet]
        [FailedHttpException]
        [Route("person")]
        public async Task<ObjectResult> GetPerson(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                return BadRequest(Messages.BadRequestMessage);

            var person = await _personService.GetPerson(name);
            return Ok(person);
        }        
        
        [HttpPost]
        [FailedHttpException]
        [Route("check-person")]
        public async Task<IActionResult> CheckPerson([FromBody] CheckPersonRequestModel body)
        {
            var person = await _personService.GetPerson(body.PersonName);
            var episode = await _episodeService.GetEpisode(body.EpisodeName);

            return Ok(episode.Characters.Contains(person.Url));
        }

        #endregion

    }
}