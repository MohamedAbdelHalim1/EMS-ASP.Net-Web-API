

using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class CommonFields
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]

        //One department , branch , town , generaldepartment can have list of Employees - Employee belong to one department

        public List<Employee>? Employees { get; set; }  //this mean that 'Employees' may be List<Employee> or Null


    }
}
