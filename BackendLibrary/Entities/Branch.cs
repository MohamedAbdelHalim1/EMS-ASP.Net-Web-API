
using System.Text.Json.Serialization;

namespace BackendLibrary.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]

        public List<ApplicationUser>? Users { get; set; }  //this mean that 'Employees' may be List<Employee> or Null


        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
