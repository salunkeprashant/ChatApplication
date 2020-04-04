using ChatApplication.Configuration;
using ChatApplication.Contracts;
using ChatApplication.Model;
using ChatApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
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
            await Clients.User(message.username).SendAsync("MessageReceived", message);
        }
    }
}