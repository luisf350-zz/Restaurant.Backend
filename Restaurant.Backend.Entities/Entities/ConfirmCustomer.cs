using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Backend.Entities.Entities
{
    public class ConfirmCustomer : EntityBase
    {
        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required]
        [MaxLength(500)]
        public string UniqueEmailKey { get; set; }

        public DateTimeOffset ExpirationEmail { get; set; }

        public int UniquePhoneKey { get; set; }

        public int PhoneActivationAttempt { get; set; }

        public DateTimeOffset ExpirationPhone { get; set; }
    }
}
