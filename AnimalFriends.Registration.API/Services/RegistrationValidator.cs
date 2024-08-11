using AnimalFriends.Registration.API.Models;
using FluentValidation;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AnimalFriends.Registration.API.Services
{
    public class RegistrationValidator : AbstractValidator<RegistrationInput>
    {
        public RegistrationValidator()
        {
            RuleFor(input => input).Must(x => !string.IsNullOrWhiteSpace(x.Email) || x.DateOfBirth.HasValue)
            .WithMessage("*Either Emial or Date of birth is required");
        }
       
        private bool Validate(RegistrationInput person)
        {
            if (!string.IsNullOrWhiteSpace(person.Email) || person.DateOfBirth.HasValue)
                return true;

            return false;
        }


    }
}
