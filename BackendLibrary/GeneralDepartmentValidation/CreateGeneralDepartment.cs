
using BackendLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BackendLibrary.GeneralDepartmentValidation
{
    public class CreateGeneralDepartment
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Name { get; set; }

       

    }
}
