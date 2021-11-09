using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.WebHost.Models
{
    public class CreateAndEditUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RecordStatus { get; set; }
    }
}
