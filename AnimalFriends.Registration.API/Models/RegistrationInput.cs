using System.ComponentModel.DataAnnotations;
using AnimalFriends.Registration.API.Attributes;

namespace AnimalFriends.Registration.API.Models
{
    public class RegistrationInput
    {
        public const string ReferenceNumberRegX = @"^[A-Z]{2}-[0-9]{6}$";
        public const string EmailregX = "^[a-z0-9]{4,}@[a-z0-9]{2,}.(com|co.uk)";
        public const string FirstLastNameRegX = @"^[a-z]{3,50}$";

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[a-z]{3,50}$", ErrorMessage = " Policy holder’s first name must be between 3 and 50 chars")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[a-z]{3,50}$", ErrorMessage = " Policy holder’s last name must be between 3 and 50 chars")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[A-Z]{2}-[0-9]{6}$", ErrorMessage = "Policy Reference number must match the following format 'XX-999999'")]
        public required string ReferenceNumber { get; set; }

        // credit - slight modified to match least chars and uk domain etc
        // https://stackabuse.com/validate-email-addresses-with-regular-expressions-in-javascript/
        
        [RegularExpression("^[a-z0-9]{4,}@[a-z0-9]{2,}.(com|co.uk)",
         ErrorMessage = @"email address should contain a string of at least 4 alph numeric chars followed by an '@' sign 
                          and then another string of at least 2 alpha numeric chars. The email address should end in either '.com' or '.co.uk'")]
        public string? Email { get; set; }

        [Over18AgeValidation(ErrorMessage ="Age Must be Over 18 or More")]
        public DateTime? DateOfBirth { get; set; }
    }
}
