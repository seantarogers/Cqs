namespace Cqs.Infrastructure.Dapper
{
    public interface IDapperConnectionFactory
    {
        IDapperConnection CreateConnection();
    }
}