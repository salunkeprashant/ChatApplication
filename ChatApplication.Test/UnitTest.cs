using ChatApplication.Configuration;
using ChatApplication.Hubs;
using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ChatApplication.Test
{
    public class UnitTest
    {

        [Test]
        public async Task ChatHubTestAsync()
        {
            Mock<IHubCallerClients> mockClients = new Mock<IHubCallerClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();

            Mock<IChatDatabaseSettings> mockChatDbSttrings = new Mock<IChatDatabaseSettings>();

            mockChatDbSttrings.Setup(x => x.DatabaseName).Returns("Chat");
            mockChatDbSttrings.Setup(x => x.ConnectionString).Returns("mongodb://localhost:27017");
            mockChatDbSttrings.Setup(x => x.Collections).Returns(new Collections() { UserCollectionName = "Users"});

            ChatHub chatHub = new ChatHub(mockChatDbSttrings.Object);

            await chatHub.SendMessage(new Message());


            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);
        }
    }
}