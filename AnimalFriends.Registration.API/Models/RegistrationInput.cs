using System.ComponentModel.DataAnnotations;
using AnimalFriends.Registration.API.Attributes;

namespace AnimalFriends.Registration.API.Models
{
    public class RegistrationInput
    {
       
        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[a-z]{3,50}$", ErrorMessage = " Policy holder’s first name should be between 3 and 50 chars")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[a-z]{3,50}$", ErrorMessage = " Policy holder’s last name should be between 3 and 50 chars")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "* Required")]
        public required string ReferenceNumber { get; set; }

       
        public string? Email { get; set; }

              
        public DateTime? DateOfBirth { get; set; }
    }
}
