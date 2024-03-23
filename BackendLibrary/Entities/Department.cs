

namespace BackendLibrary.Entities
{
    public class Department 
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public GeneralDepartment? GeneralDepartment { get; set; }
        public int? GeneralDepartmentId { get; set; }
    }
}
