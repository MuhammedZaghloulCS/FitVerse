using FitVerse.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitVerse.WebUI.Filters
{
    /// <summary>
    /// Action filter to populate user information in ViewBag for all views
    /// </summary>
    public class UserInfoActionFilter : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserInfoActionFilter(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get the controller
            if (context.Controller is Controller controller)
            {
                // Check if user is authenticated
                if (context.HttpContext.User?.Identity?.IsAuthenticated == true)
                {
                    // Get user ID from claims
                    var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    if (!string.IsNullOrEmpty(userId))
                    {
                        // Get user from database
                        var user = await _userManager.FindByIdAsync(userId);
                        
                        if (user != null)
                        {
                            // Populate ViewBag with user information
                            controller.ViewBag.UserFullName = user.FullName ?? user.UserName;
                            controller.ViewBag.UserImagePath = user.ImagePath;
                            controller.ViewBag.UserEmail = user.Email;
                            controller.ViewBag.UserId = user.Id;
                            
                            // Get user roles
                            var roles = await _userManager.GetRolesAsync(user);
                            controller.ViewBag.UserRole = roles.FirstOrDefault() ?? "User";
                        }
                    }
                }
            }

            // Continue with the action execution
            await next();
        }
    }
}
