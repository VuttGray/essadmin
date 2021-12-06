using System;

namespace ESS.Admin.Core.Domain.Administration
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{LastName}, {FirstName}";

        public string Email { get; set; }
        public bool IsLocked { get; set; }
    }
}