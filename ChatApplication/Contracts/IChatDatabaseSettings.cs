using ChatApplication.Configuration;

namespace ChatApplication.Contracts
{
    public interface IChatDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
