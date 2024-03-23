using System.Text.Json.Serialization;

namespace BackendLibrary.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string? CityName { get; set; }
        [JsonIgnore]

        public List<Branch>? Branches { get; set; }  //each city may contain many branches
    }
}