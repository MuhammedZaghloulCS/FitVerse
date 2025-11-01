using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using FitVerse.Core.IService;
using FitVerse.Data.Models;

namespace FitVerse.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private static readonly Dictionary<string, string> _userConnections = new(); // userId → connectionId

        public ChatHub(IChatService chatService, IMessageService messageService)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        public async Task SendMessage(string chatId, string receiverId, string message)
        {
            var senderId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(senderId))
                return;

            // إنشاء الرسالة وحفظها
            var newMessage = new Message
            {
                ChatId = int.Parse(chatId),
                SenderId = senderId,
                ReciverId = receiverId,
                Content = message,
                SentAt = DateTime.Now,
                IsRead = false
            };

            await _messageService.CreateAsync(newMessage);

            var messageData = new
            {
                Id = newMessage.Id,
                ChatId = chatId,
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message,
                SentAt = newMessage.SentAt.ToString("HH:mm"),
                IsRead = false
            };

            // إرسال الرسالة للمستقبل لو متصل
            if (_userConnections.TryGetValue(receiverId, out var receiverConnectionId))
            {
                await Clients.User(receiverId).SendAsync("ReceiveMessage", messageData);
            }

            // إرسال نسخة للمرسل لتحديث الـ UI
            if (_userConnections.TryGetValue(senderId, out var senderConnectionId))
            {
                await Clients.User(senderId).SendAsync("ReceiveMessage", messageData);
            }
        }

        public async Task MarkMessageAsRead(string messageId)
        {
            var userId = Context.UserIdentifier;
            var message = await _messageService.GetByIdAsync(int.Parse(messageId));

            if (message != null && message.ReciverId == userId)
            {
                message.IsRead = true;
                await _messageService.UpdateAsync(message);

                if (_userConnections.TryGetValue(message.SenderId, out var senderConnectionId))
                {
                    await Clients.Client(senderConnectionId).SendAsync("MessageRead", messageId);
                }
            }
        }

        public async Task UserTyping(string receiverId)
        {
            var userId = Context.UserIdentifier;
            if (_userConnections.TryGetValue(receiverId, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("UserTyping", userId);
            }
        }

        public async Task UserStoppedTyping(string receiverId)
        {
            var userId = Context.UserIdentifier;
            if (_userConnections.TryGetValue(receiverId, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("UserStoppedTyping", userId);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections[userId] = Context.ConnectionId;
                await Clients.All.SendAsync("UserOnline", userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections.Remove(userId);
                await Clients.All.SendAsync("UserOffline", userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }

}
