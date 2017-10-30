using dna.core.auth.Entity;
using dna.core.auth.Infrastructure;
using dna.core.auth.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Linq;
using dna.core.libs.Sender;
using Microsoft.AspNetCore.Authentication;

namespace dna.core.auth
{

    public partial class AuthenticationService : IAuthenticationService
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly RoleManager<ApplicationRole> _roleManager;
        private readonly HttpContext _context;
       

        private readonly ISenderFactory _sender;
       
        public AuthenticationService(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, 
            ISenderFactory sender)
        {
            _context = contextAccessor.HttpContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _sender = sender;

           
          
        }

        public async Task<AuthResult> InitRoleAndUser()
        {
            await InitializeRolesAndUser();
            return new AuthResult() { Suceess = true, Message = AuthResult.SUCCESS};
        }

        protected async Task<ApplicationUser> GetCurrentUser()
        {
            var claim = _context.User.Claims.Where(c => c.Type.Equals("sub")).FirstOrDefault();
            return await _userManager.FindByIdAsync(claim.Value);
        }

        public bool IsAuthenticate()
        {
            return _context.User.Identity.IsAuthenticated;
        }
        
        public string GetUserRole()
        {
            var res =_context.User.Claims.Where(x => x.Type.Equals("role")).FirstOrDefault();
            if ( res != null )
                return res.Value;
            else
                return "";
        }
        public bool IsSuperAdmin()
        {
            return _context.User.IsInRole(MembershipConstant.SuperAdmin);
        }

        public async Task<IdentityResult> Register(RegisterModel model, bool emailConfirmation)
        {
          
            var user = new ApplicationUser()
            {
                UserName = String.IsNullOrWhiteSpace(model.Username) ? model.Email : model.Username,
                PhoneNumber = model.CellPhone,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if ( result.Succeeded )
            {
                if ( !String.IsNullOrWhiteSpace(model.Email) && emailConfirmation == true )
                {
                    /*var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    string template = this.GetEmailTemplate(new EmailOptions()
                    {
                        HostingEnvirontment = _hostingEnvirontment,
                        EmailType = EmailOptions.Welcome
                    });
                    template = template.Replace("CONFIRMATION_LINK", callbackUrl);

                    //send email confirmation to user
                    
                    ISender _sender = _senderFactory.Create("sendgrid");
                   await _sender.SendAsync(model.Email, "Email Konfirmasi Aktiviz.ID", template);*/
                }

                
                await _userManager.AddToRoleAsync(user, MembershipConstant.Member);

                if(emailConfirmation == false)
                    await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;


        }

        public async Task<ClaimsIdentity> Login(LoginModel model)
        {
            
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
            if ( result.Succeeded )
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var claims = await _userManager.GetClaimsAsync(user);

                return await Task.FromResult(new ClaimsIdentity(new GenericIdentity(model.Username, "Token"), claims));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
        
        public async Task<AuthResult> ConfirmEmail(string userId, string code)
        {
            
            var message = new AuthResult();
            if ( userId == null || code == null )
            {
                message.Message = AuthResult.ERROR;
            }else
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                if ( user == null )
                {
                    message.Message = AuthResult.USER_NOTFOUND;
                }
                else
                {
                    var result = await _userManager.ConfirmEmailAsync(user, code);
                    if ( result.Succeeded )
                    {
                        user.Status = UserStatus.Active;
                        await _userManager.UpdateAsync(user);
                        message.Suceess = true;
                        message.Message = AuthResult.SUCCESS;
                    }
                }
            }
            return message;
        }
       
        /*public async Task<JsonResult> GetLoginUser()
        {           
            return new JsonResult(await GetCurrentUser());
        }*/
        public Nullable<int> GetUserId()
        {
            // remove try catch is already published to public
            try
            {
                string Id = _context.User.Claims
                            .Where(c => c.Type.Equals("sub")).FirstOrDefault().Value;
                if ( !String.IsNullOrWhiteSpace(Id) )
                {
                    return int.Parse(Id);
                }
                return null;
            }catch(Exception ex )
            {
                return 1;
            }
            
        }

        public async Task<SignInResult> Login(LoginModel model, bool lockoutOnFailure = false)
        {
            return  await _signInManager.PasswordSignInAsync(model.Username, model.Password, 
                        model.RememberLogin, lockoutOnFailure: lockoutOnFailure);
        }
        public async Task<AuthResult> Logout()
        {
           
            var message = new AuthResult();
            try
            {
                await _signInManager.SignOutAsync();
                message.Suceess = true;
                message.Message = AuthResult.SUCCESS;
            }catch(Exception ex )
            {
                message.Message = ex.Message;
            }
            return message;
            
        }
        //TODO: Fixing external login
        public AuthenticationProperties ConfigureExternalAuthentication(string provider, string returnUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);

        }
        public async Task<ExternalLoginInfoModel> GetExternalLogin(bool isPersistent = false)
        {
            try
            {
               
                var info = await _signInManager.GetExternalLoginInfoAsync();
                // Sign in the user with this external login provider if the user already has a login.
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: isPersistent);
                return new ExternalLoginInfoModel
                {
                    Info = info,
                    Result = result
                };
            }
            catch ( Exception ex )
            {
                return null;
            }
        }

        public async Task<IdentityResult> RegisterWithPhoneNumber(string phoneNumber)
        {
            
            
            var username = Guid.NewGuid();
            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var user = new ApplicationUser()
            {
                UserName = username.ToString(),
                PhoneNumber = phoneNumber
            };
            var result = await _userManager.CreateAsync(user, password);
            

            if ( result.Succeeded )
            {
                
                await _userManager.AddToRoleAsync(user, MembershipConstant.Member);
                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
                await _signInManager.SignInAsync(user, isPersistent: false);
                ISender sender = _sender.Create("sms");
                var response = await sender.SendAsync(phoneNumber, "", String.Format("Your code verification: {0}", code));
               
                            
            }

            return result;
        }

        public Task<AuthResult> AddPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        

        public Task<AuthResult> AddEmail(string email)
        {
            throw new NotImplementedException();
        }
        public async Task<AuthResult> GenerateActivationCode(string provider, string receiver)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            var message = new AuthResult();

            if ( user == null )
                message.Message = AuthResult.USER_NOTFOUND;

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, provider);
            if ( string.IsNullOrWhiteSpace(code) )
            {
                message.Message = AuthResult.FAILED_GENERATE_TOKEN;
            }

            
            if (provider == ProviderConstant.SMS )
            {
                var tokenMessage = "Your security code is: " + code;
                ISender sender = _sender.Create("sms");
                await sender.SendAsync(user.PhoneNumber, "", tokenMessage);

                message.Suceess = true;
                message.Message = AuthResult.SUCCESS;
            }else
            {
                //activation code for email
                message.Suceess = true;
                message.Message = AuthResult.SUCCESS;
            }

            return message;
        }

        public async Task<AuthResult> VerifyCode(VerifyCodeModel model)
        {
            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            var message = new AuthResult();
            if ( result.Succeeded )
            {
                message.Suceess = true;
                message.Message = AuthResult.SUCCESS;
            }
            if ( result.IsLockedOut )
            {
                message.Message = "User account locked out.";
            }
            else
            {
                message.Message = "Invalid code.";
            }

            return message;
        }
    }
}
