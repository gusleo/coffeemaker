using dna.core.auth.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dna.core.auth.Entity
{

    public class ApplicationUser : IdentityUser<int>
    {

        public UserStatus Status { get; set; }
    }

    
    public class AppUserLogin : IdentityUserLogin<int> { }


    public class AppUserRole : IdentityUserRole<int> { }

    public class AppUserClaim : IdentityUserClaim<int> { }

    public class AppUserToken : IdentityUserToken<int> { }
    

    public class ApplicationRole : IdentityRole<int> {
        public ApplicationRole() : base() { }
    }

    public class CustomRoleStore : RoleStore<ApplicationRole, DbContext, int>
    {
        public CustomRoleStore(DbContext context) : base(context) { }
    }

    public class CustomUserStore : UserStore<ApplicationUser, ApplicationRole, DbContext, int>
    {
        public CustomUserStore(DbContext context)
            : base(context)
        {
        }
    }

}
