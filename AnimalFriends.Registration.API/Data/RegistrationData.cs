using AnimalFriends.Registration.API.Models;
using Dapper;

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
            const string Query = @"INSERT INTO[dbo].[Customer] ([FirstName],[LastName],[ReferenceNumber],[Email],[DateOfBirth])
                                    VALUES (@firstName,@LastName,@ReferenceNumber,@Email,@DateOfBirth) 
                                    SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var conn = await _db.CreateOpenConnection())
            {
                return await conn.ExecuteAsync(Query, dbo);
            };

        }

        public async Task<RegistrationDbo> Get(int id)
        {
            const string Query = @"SELECT * from Customer Where Customerid = @id";

            using (var conn = await _db.CreateOpenConnection())
            {
                return await conn.QuerySingleAsync<RegistrationDbo>(Query, new { id });
            };

        }
    }
}
