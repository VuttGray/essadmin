using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.WebHost.Models
{
    public class EditMessageRequest
    {
        public DateTime? SentDate { get; set; }
        public int Attempts { get; set; }
    }
}
