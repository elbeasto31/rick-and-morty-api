using System.Collections.Generic;

namespace RickAndMortyAPI.Models
{
    public class Episode
    {
        public string Name { get; set; }
        public List<string> Characters { get; set; }

        public override string ToString() => Name;
    }
}