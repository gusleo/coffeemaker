using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.auth.Model
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
    }
}
