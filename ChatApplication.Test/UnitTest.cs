using ChatApplication.Configuration;
using ChatApplication.Contracts;
using ChatApplication.Hubs;
using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApplication.Test
{
    public class UnitTest
    {

        [Test]
        public async Task ChatHubTestAsync()
        {
            // Mock clients for SignalR
            Mock<IHubCallerClients> mockClients = new Mock<IHubCallerClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            // Mock chat database settings
            Mock<IChatDatabaseSettings> mockChatDbSttrings = new Mock<IChatDatabaseSettings>();
            mockChatDbSttrings.Setup(x => x.DatabaseName).Returns("Chat");
            mockChatDbSttrings.Setup(x => x.ConnectionString).Returns("mongodb://localhost:27017");

            // Create instance and invoke methods
            ChatHub chatHub = new ChatHub() 
            {
                Clients = mockClients.Object
            };

            // Creat and send a message
            Message message = new Message();
            message.Content = "Hii";
            message.SentOn = DateTime.Today;
            message.Type = "Sent";
            message.RecipientId = new ObjectId().ToString();

            await chatHub.SendPrivateMessage(message);

            // Asserts, to verfiy 
            mockClients.Verify(clients => clients.All, Times.Once);

            // Note : To make following assertion work, we need to make some changes in Hub's 'SendAsync' calling method.
            // Extension method invoke 'SendCoreAsync' internally, Since we can't mock up extension methods we need to invoke them in 
            // such a way that it will match following arguments, e.g - SendAsync('MessageReceived' new[] {message});
            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "MessageReceived",
                    It.Is<object[]>(item => item != null),
                    default(CancellationToken)),
                Times.Once);


        }
    }
}