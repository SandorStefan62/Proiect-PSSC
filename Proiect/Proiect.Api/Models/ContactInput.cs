using System.ComponentModel.DataAnnotations;

namespace Proiect.Api.Models
{
    public class ContactInput
    {
        
        [Required]
        [RegularExpression("[A-Za-z0-9]+")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9]+")]
        public string LastName { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression("[0-9]+")]
        public string TelephoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
