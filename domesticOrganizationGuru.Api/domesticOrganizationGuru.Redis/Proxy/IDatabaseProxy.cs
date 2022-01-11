using StackExchange.Redis;

namespace domesticOrganizationGuru.Redis.Proxy
{
    public interface IDatabaseProxy: IDatabase, IConnectionMultiplexer, ITransaction
    {
    }
}
