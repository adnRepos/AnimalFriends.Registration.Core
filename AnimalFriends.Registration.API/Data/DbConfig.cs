namespace AnimalFriends.Registration.API.Data
{
    public interface IDbConfig
    {
        string ConnectionString { get; set; }
        string MasterConnectionString { set; get; } // need only DB not exists - to create db in Db.cs Init()
    }
    public class DbConfig : IDbConfig
    {
        public required string ConnectionString { set; get; }
        public required string MasterConnectionString { set; get; }
    }
}
