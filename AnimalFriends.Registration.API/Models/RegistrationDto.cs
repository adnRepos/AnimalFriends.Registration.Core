namespace AnimalFriends.Registration.API.Models
{
    public class RegistrationDto
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ReferenceNumber { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
