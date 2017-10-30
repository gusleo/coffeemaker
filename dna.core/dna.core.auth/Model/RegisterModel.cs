using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.auth.Model
{
    /// <summary>
    /// View model for user registration
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Register user username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Register user cellphone
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// Register user email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Register user password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Register user confirm password
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
