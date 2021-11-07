using System;

namespace ESS.Admin.WebHost.Models
{
    public class UserShortResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

    }
}