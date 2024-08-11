using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper.Contrib.Extensions;

namespace AnimalFriends.Registration.API.Data
{
    public interface IDb
    {
        public Task<IDbConnection> CreateOpenConnection(ApplicationIntent intent = ApplicationIntent.ReadWrite, bool IsMaster = false);
        public Task InitDb();
    }


    public class Db : IDb
    {
        private readonly IDbConfig _config;
        public Db(IDbConfig config)
        {
            _config = config;
        }

        public async Task<IDbConnection> CreateOpenConnection(ApplicationIntent intent = ApplicationIntent.ReadWrite, bool IsMaster = false)
        {
            string connString = IsMaster ? _config.MasterConnectionString : _config.ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                await conn.OpenAsync();
            }
            catch (Exception ex)
            {
                var connInfo = new SqlConnectionStringBuilder(connString);
                ex.Data["UserID"] = connInfo.UserID;
                ex.Data["DataSource"] = connInfo.DataSource;
                ex.Data["InitialCatalog"] = connInfo.InitialCatalog;
                ex.Data["ApplicationIntent"] = connInfo.ApplicationIntent;

                throw;
            }

            return conn;
        }

        /// <summary>
        ///  This checks if Db and table exists if not then create 
        /// </summary>
        /// <returns></returns>
        public async Task InitDb()
        {
        }

        /// <summary>
        /// if table are not ...create
        /// </summary>
        /// <returns></returns>
        private async Task InitTables()
        {
        }

    }
}
