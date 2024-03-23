
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class AccountBase
    {

        //Email validation
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        //Password validation
        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }

    }
}
