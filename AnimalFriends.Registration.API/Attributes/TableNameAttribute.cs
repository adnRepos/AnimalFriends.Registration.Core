
using Dapper.Contrib.Extensions;

namespace AnimalFriends.Registration.API.Attributes
{
    public sealed class TableNameAttribute : TableAttribute
    {
        public TableNameAttribute(string tableName) : base(tableName) { }
    }
}
