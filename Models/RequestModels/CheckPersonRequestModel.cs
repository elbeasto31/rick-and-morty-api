using System;

namespace RickAndMortyAPI.Models.RequestModels
{
    public class CheckPersonRequestModel
    {
        public string PersonName { get; set; }
        public string EpisodeName { get; set; }
    }
}