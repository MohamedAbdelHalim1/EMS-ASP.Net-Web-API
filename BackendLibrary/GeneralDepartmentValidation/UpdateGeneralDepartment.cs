
using System.ComponentModel.DataAnnotations;

namespace BackendLibrary.GeneralDepartmentValidation
{
    public class UpdateGeneralDepartment
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
