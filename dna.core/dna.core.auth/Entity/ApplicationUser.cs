using dna.core.auth.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace dna.core.auth.Entity
{

    public class ApplicationUser : IdentityUser<int>
    {

        public UserStatus Status { get; set; }
    }

    /*
    public class AppUserLogin : IdentityUserLogin<int> { }


    public class AppUserRole : IdentityUserRole<int>
    {

        public virtual ApplicationRole Role { get; set; }
    }


    public class AppUserClaim : IdentityUserClaim<int> { }

    public class AppUserToken : IdentityUserToken<int> { }
    */

    public class ApplicationRole : IdentityRole<int> {
        public ApplicationRole() : base()
        {
        }
    }
}
