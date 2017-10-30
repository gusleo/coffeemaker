using dna.core.auth.Entity;
using dna.core.auth.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.auth
{
    public partial class AuthenticationService
    {
        protected async Task InitializeRolesAndUser()
        {
           
                
            IList<ApplicationRole> roles = new List<ApplicationRole>()
            {
                new ApplicationRole() {Name = MembershipConstant.SuperAdmin, NormalizedName =  MembershipConstant.SuperAdmin.ToUpper()},
                new ApplicationRole() {Name = MembershipConstant.Admin, NormalizedName =  MembershipConstant.Admin.ToUpper() },
                new ApplicationRole() {Name = MembershipConstant.Operator, NormalizedName =  MembershipConstant.Operator.ToUpper() },
                new ApplicationRole() {Name = MembershipConstant.Staff, NormalizedName =  MembershipConstant.Staff.ToUpper() },
                new ApplicationRole() {Name = MembershipConstant.Owner, NormalizedName =  MembershipConstant.Owner.ToUpper() },
                new ApplicationRole() {Name = MembershipConstant.Member, NormalizedName =  MembershipConstant.Member.ToUpper() }
            };
            foreach ( var role in roles )
            {
                var existRole = await _roleManager.FindByNameAsync(role.Name);
                if ( existRole == null )
                {
                    await _roleManager.CreateAsync(role);
                }
                    
            }
                
            var user = new ApplicationUser
            {
                UserName = "admin@smartmedika.com",
                NormalizedUserName = "admin@smartmedika.com",
                Email = "admin@smartmedika.com",
                NormalizedEmail = "admin@smartmedika.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var existUser = await _userManager.FindByNameAsync(user.UserName);
            if ( existUser == null )
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Test123!");
                user.PasswordHash = hashed;

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, MembershipConstant.SuperAdmin);
            }                   
                
                       
        }
    }
}
