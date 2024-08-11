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
            // create database if it doesn't exist
            const string db_sql = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TaskDb') CREATE DATABASE [TaskDb];";
            using (var conn = await CreateOpenConnection(IsMaster: true))
            {
                await conn.ExecuteAsync(db_sql);
            };

            // create tables if dones't exists
            await InitTables();
        }

        /// <summary>
        /// if table are not ...create
        /// </summary>
        /// <returns></returns>
        private async Task InitTables()
        {
            const string table_sql = @$" USE [TaskDb] 
                                      IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE  TABLE_NAME = 'Customer')) 
                                       BEGIN
                                            CREATE TABLE [dbo].[Customer](
	                                        [CustomerId] [int] IDENTITY(1,1) NOT NULL,
	                                        [FirstName] [nvarchar](50) NOT NULL,
	                                        [LastName] [nvarchar](50) NOT NULL,
	                                        [ReferenceNumber] [nvarchar](10) NOT NULL,
	                                        [Email] [nvarchar](100) NULL,
	                                        [DateOfBirth] [date] NULL,
                                            CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
                                        (
	                                        [CustomerId] ASC
                                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                                        ) ON [PRIMARY]
                                        
                                       END";
            using (var conn = await CreateOpenConnection(IsMaster: true))
            {
                await conn.ExecuteAsync(table_sql);
            };
        }

    }
}
