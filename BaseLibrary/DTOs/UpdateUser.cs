using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class UpdateUser
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Fullname { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }

    }
}
