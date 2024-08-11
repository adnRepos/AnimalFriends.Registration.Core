namespace AnimalFriends.Registration.API.Models
{

    /// <summary>
    /// Dbo entity matches db table in store
    /// </summary>
    public class RegistrationDbo
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ReferenceNumber { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

       
    }
}
