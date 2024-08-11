using AgeCalculator;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace AnimalFriends.Registration.API.Attributes
{
    public sealed class Over18AgeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime? date = value as DateTime?;
            if (date == null)
            {
                return true;
                //throw new ArgumentNullException(nameof(date));
            }

            var dob = new Age(date.Value.Date, DateTime.Now.Date);
            if (dob.Years >= 18)
            {
                return true;
            }

            return false;
        }

    }
}
