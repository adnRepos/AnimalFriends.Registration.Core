using Dapper.Contrib.Extensions;

namespace AnimalFriends.Registration.API.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
    public sealed class DbIgnoreAttribute : WriteAttribute
    {
        public DbIgnoreAttribute() : base(false) { }
    }
}
