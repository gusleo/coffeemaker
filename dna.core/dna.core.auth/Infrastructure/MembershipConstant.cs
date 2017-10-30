using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.auth.Infrastructure
{
    public static class MembershipConstant
    {
        public const string Member = "Member";
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Owner = "Owner";
        public const string Staff = "Staff";
        public const string Operator = "Operator";
        public const string MultipleRolesAdmin = "SuperAdmin,Admin,Owner,Staff,Operator";

    }
}
