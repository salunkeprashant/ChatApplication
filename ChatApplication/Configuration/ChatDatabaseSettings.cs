using ChatApplication.Contracts;

namespace ChatApplication.Configuration
{
    public class ChatDatabaseSettings : IChatDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
