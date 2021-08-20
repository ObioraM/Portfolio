using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Contact
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Display(Name = "Full Name")]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{8,11}$")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(10)]
        public string Message { get; set; }
    }
}
