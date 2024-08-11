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
        [RegularExpression(@"^[A-Z]{2}-[0-9]{6}$", ErrorMessage = "Policy Reference number should match the following format 'XX-999999'")]
        public required string ReferenceNumber { get; set; }

        // credit - slight modified to match least chars etc
        // https://stackabuse.com/validate-email-addresses-with-regular-expressions-in-javascript/
        
        [RegularExpression("^[a-z0-9]{4,}@[a-z0-9]{2,}.(com|co.uk)",
         ErrorMessage = @"email address should contain a string of at least 4 alph numeric chars followed by an '@' sign 
                          and then another string of at least 2 alpha numeric chars. The email address should end in either '.com' or '.co.uk'")]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
