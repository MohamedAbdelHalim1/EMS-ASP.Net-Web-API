
namespace BackendLibrary.Entities
{
    public class GeneralDepartment 
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Department>? Departments { get; set; }
    }
}
