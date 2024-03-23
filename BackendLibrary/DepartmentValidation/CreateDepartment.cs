
using BackendLibrary.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendLibrary.DepartmentValidation
{
    public class CreateDepartment
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [ForeignKey(nameof(GeneralDepartmentId))]
        public int? GeneralDepartmentId { get; set; }


    }
}
