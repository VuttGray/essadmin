using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.WebHost.Models
{
    public class CreateMessageRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Recipients { get; set; } = new List<string>();
    }
}
