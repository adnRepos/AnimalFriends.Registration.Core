using AnimalFriends.Registration.API.Models;
using FluentValidation;

namespace AnimalFriends.Registration.API.Services
{
    public class RegistrationValidator : AbstractValidator<RegistrationInput>
    {
        public RegistrationValidator()
        {
            RuleFor(input => input).Must(person => !string.IsNullOrWhiteSpace(person.Email) && person.DateOfBirth.HasValue == false)
            .WithMessage("*Either Emial or Date of birth is required");
        }


    }
}
