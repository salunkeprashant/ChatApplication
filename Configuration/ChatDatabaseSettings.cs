using System.Collections.Generic;

namespace ChatApplication.Configuration
{
    public class ChatDatabaseSettings : IChatDatabaseSettings
    {
        public Collections Collections { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IChatDatabaseSettings
    {
        Collections Collections { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class Collections {
        public string UserCollectionName { get; set; }
    }
}
