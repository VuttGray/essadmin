using System;

namespace ESS.Admin.WebHost.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

    }
}