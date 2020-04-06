using ChatApplication.Configuration;
using ChatApplication.Contracts;
using ChatApplication.Model;
using ChatApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ChatApplication.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendPrivateMessage(Message message)
        {
            message.SenderId = Helper.Constants.BuildObjectId(message.SenderId);
            message.RecipientId = Helper.Constants.BuildObjectId(message.SenderId);

            await Clients.User(message.RecipientId.ToString()).SendAsync("MessageReceived", message);
        }
    }
}