using ChatApplication.Configuration;
using ChatApplication.Model;
using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        private MongoClient client;
        private IMongoDatabase database;
        private readonly Collections collections;

        public ChatHub(IChatDatabaseSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
            collections = settings.Collections;
        }
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("MessageReceived", message);
        }

        private void GetUsers()
        {
            var users = database.GetCollection<User>(collections.UserCollectionName);
        }
    }
}