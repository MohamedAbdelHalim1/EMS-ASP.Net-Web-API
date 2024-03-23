
using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.GeneralDepartmentValidation
{
    public class CreateGeneralDepartment
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Name { get; set; }

       

    }
}
