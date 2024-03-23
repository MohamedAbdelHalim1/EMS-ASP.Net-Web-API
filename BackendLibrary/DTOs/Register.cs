
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendLibrary.DTOs
{
    public class Register : AccountBase
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Fullname { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]  
        [Required]
        public string? ConfirmPassword { get; set; }

        [ForeignKey(nameof(BranchId))]
        public int? BranchId { get; set; }
    }
}
