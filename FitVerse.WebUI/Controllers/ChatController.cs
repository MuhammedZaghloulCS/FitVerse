using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FitVerse.Core.Models;
using FitVerse.Core.IService;
using FitVerse.Data.Models;
using FitVerse.Core.Helpers;
using FitVerse.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitVerse.Web.Controllers
{
    public class CreateChatRequest
    {
        public string OtherUserId { get; set; }
    }
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICoachService _coachService;
        private readonly IClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;

        public ChatController(IChatService chatService, IMessageService messageService, UserManager<ApplicationUser> userManager, ICoachService coachService, IClientService clientService, IUnitOfWork unitOfWork)
        {
            _chatService = chatService;
            _messageService = messageService;
            _userManager = userManager;
            _coachService = coachService;
            _clientService = clientService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> ClientChat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chats = await _chatService.GetUserChatsAsync(userId);
            
            // Get all coaches for client to start new chats - we need actual Coach entities with UserId
            var coachEntities = await GetAllCoachEntitiesAsync();
            var coaches = coachEntities;
            
            ViewBag.UserId = userId;
            ViewBag.Chats = chats;
            ViewBag.AvailableCoaches = coaches;
            return View();
        }

        public async Task<IActionResult> CoachChat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chats = await _chatService.GetUserChatsAsync(userId);
            
            // Get all clients for coach to start new chats - we need actual Client entities with UserId
            // Since ClientDashVM doesn't have UserId, let's get the actual Client entities
            var clientEntities = await GetAllClientEntitiesAsync();
            var clients = clientEntities;
            
            ViewBag.UserId = userId;
            ViewBag.Chats = chats;
            ViewBag.AvailableClients = clients;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatRequest request)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = await _userManager.FindByIdAsync(currentUserId);
                
                // Debug logging
                Console.WriteLine($"Current User ID: {currentUserId}");
                Console.WriteLine($"Other User ID: {request?.OtherUserId}");
                
                if (request == null || string.IsNullOrEmpty(request.OtherUserId))
                {
                    return Json(new { success = false, message = "Invalid request: OtherUserId is required" });
                }
                
                var otherUser = await _userManager.FindByIdAsync(request.OtherUserId);
                
                if (otherUser == null)
                {
                    return Json(new { success = false, message = $"User not found with ID: {request.OtherUserId}" });
                }

                // Determine who is coach and who is client
                string clientId, coachId;
                if (await _userManager.IsInRoleAsync(currentUser, RoleConstants.Coach))
                {
                    coachId = currentUserId;
                    clientId = request.OtherUserId;
                }
                else
                {
                    clientId = currentUserId;
                    coachId = request.OtherUserId;
                }

                var chat = await _chatService.CreateChatAsync(clientId, coachId);
                
                return Json(new { 
                    success = true, 
                    chatId = chat.Id, 
                    otherUserName = otherUser.UserName 
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetChatMessages(int chatId)
        {
            var messages = await _messageService.GetChatMessagesAsync(chatId);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var messageData = messages.Select(m => new
            {
                Id = m.Id,
                Content = m.Content,
                SentAt = m.SentAt.ToString("HH:mm"),
                SenderId = m.SenderId,
                SenderName = m.Sender?.UserName ?? "Unknown",
                IsCurrentUser = m.SenderId == currentUserId,
                IsRead = m.IsRead
            });

            return Json(messageData);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int chatId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _messageService.MarkMessagesAsReadAsync(chatId, userId);
            
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserChats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chats = await _chatService.GetUserChatsAsync(userId);
            
            var chatData = new List<object>();
            
            foreach (var chat in chats)
            {
                ApplicationUser otherUser;
                if (chat.ClientId == userId)
                {
                    otherUser = chat.Coach?.User;
                }
                else
                {
                    otherUser = chat.Client?.User;
                }
                
                var latestMessage = await _messageService.GetLatestMessageInChatAsync(chat.Id);
                var unreadCount = chat.Messages?.Count(m => m.ReciverId == userId && !m.IsRead) ?? 0;
                
                chatData.Add(new
                {
                    Id = chat.Id,
                    OtherUserId = otherUser?.Id,
                    OtherUserName = otherUser?.FullName ?? "Unknown",
                    OtherUserAvatar = $"https://ui-avatars.com/api/?name={otherUser?.UserName}&background=10b981&color=fff",
                    LatestMessage = latestMessage?.Content ?? "No messages yet",
                    LatestMessageTime = latestMessage?.SentAt.ToString("HH:mm") ?? "",
                    UnreadCount = unreadCount,
                    IsOnline = false // You can implement online status tracking
                });
            }
            
            return Json(chatData);
        }

        private async Task<IEnumerable<Coach>> GetAllCoachEntitiesAsync()
        {
            return await _unitOfWork.Coaches.GetQueryable()
                .Where(c => c.User.Status=="Active" && c.UserId != null)
                .Include(c => c.User)
                .ToListAsync();
        }

        private async Task<IEnumerable<Client>> GetAllClientEntitiesAsync()
        {
            return await _unitOfWork.Clients.GetQueryable()
                .Where(c => c.User.Status == "Active" && c.UserId != null)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}
