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
        private readonly IPersonService _personService;
        private readonly IEpisodeService _episodeService;
        
        public PersonController(IPersonService personService, IEpisodeService episodeService)
        {
            _personService = personService;
            _episodeService = episodeService;
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
                var person = await _personService.GetPerson(name);
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
                var person = await _personService.GetPerson(body.PersonName);
                var episode = await _episodeService.GetEpisode(body.EpisodeName);

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