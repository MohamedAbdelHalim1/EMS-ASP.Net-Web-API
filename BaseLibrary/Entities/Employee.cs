

namespace BaseLibrary.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CivilId { get; set; }
        public string? FileNumber { get; set; }
        public string? Fullname { get; set; }
        public string? JobName { get; set; }
        public string? Address { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Photo { get; set; }
        public string? Other { get; set; }


        // relationships belongs to employees ,Since Employee is a 'One' in one-many relationships in the following
        public GeneralDepartment? GeneralDepartment { get; set; }  //one Employee belongs to one GeneralDepartment
        public int GeneralDepartmentId { get; set; }  //forign Key

        //---------------------------------------
        public Department? Department { get; set; }  //Department {get;set;} is a name of relationship if you want to bring employee belong to this branch , AS IN LARAVEL
        public int DepartmentId { get; set; }
        //---------------------------------------
        public Branch? Branch { get; set; }
        public int BranchId { get; set; }
        //---------------------------------------
        public Town? Town { get; set; }
        public int TownId { get; set; }
    }
}
