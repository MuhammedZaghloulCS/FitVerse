using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class AllowDuplicateUsernameValidator<TUser> : UserValidator<TUser>
    where TUser : class
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
    {
        var result = await base.ValidateAsync(manager, user);

        // Remove the "DuplicateUserName" error if it exists
        var errors = result.Errors.Where(e => e.Code != "DuplicateUserName").ToList();

        return errors.Count() == result.Errors.Count()
     ? result
     : IdentityResult.Success;

    }
}
