using AnimalFriends.Registration.API.Models;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace AnimalFriends.Registration.API.Data
{
    public interface IRegistrationData
    {
        public Task<RegistrationDbo> Get(int id);
        public Task<int> Create(RegistrationDbo dbo);
    }
    public class RegistrationData : IRegistrationData
    {
        private readonly IDb _db;

        public RegistrationData(IDb db)
        {
            _db = db;
        }

        public async Task<int> Create(RegistrationDbo dbo)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrationDbo> Get(int id)
        {
           throw new NotImplementedException();
        }
    }
}
