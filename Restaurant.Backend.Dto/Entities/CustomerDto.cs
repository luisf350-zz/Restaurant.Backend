using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Backend.Dto.Entities
{
    public class CustomerDto
    {
        [Required]
        public Guid IdentificationTypeId { get; set; }

        [Required]
        public long IdentificationNumber { get; set; }

        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(250)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }

        public short Gender { get; set; }
    }
}
