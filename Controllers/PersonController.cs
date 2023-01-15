using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RickAndMortyAPI.Models.RequestModels;
using RickAndMortyAPI.Services.Interfaces;
using RickAndMortyAPI.Utils.Constants;
using RickAndMortyAPI.Utils.Exceptions;

namespace RickAndMortyAPI.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    [ResponseCache(CacheProfileName = "Default")]
    public class PersonController : ControllerBase
    {
        private IPersonService personService;
        private IEpisodeService episodeService;
        
        public PersonController(IPersonService _personService, IEpisodeService _episodeService)
        {
            personService = _personService;
            episodeService = _episodeService;
        }
        
        #region Actions

        [HttpGet]
        [Route("person")]
        public async Task<ObjectResult> GetPerson(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                return BadRequest(Messages.BadRequestMessage);

            try
            {
                var person = await personService.GetPerson(name);
                return Ok(person);
            }
            catch (FailedHttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(Messages.NotFoundMessage);
                
                return BadRequest(Messages.BadRequestMessage);
            }
        }        
        
        [HttpPost]
        [Route("check-person")]
        public async Task<IActionResult> CheckPerson([FromBody] CheckPersonRequestModel body)
        {
            try
            {
                var person = await personService.GetPerson(body.PersonName);
                var episode = await episodeService.GetEpisode(body.EpisodeName);

                return Ok(episode.Characters.Contains(person.Url));
            }
            catch (FailedHttpRequestException ex)
            {
                if(ex.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(Messages.NotFoundMessage);
                
                return BadRequest(Messages.BadRequestMessage);
            }
        }

        #endregion

    }
}