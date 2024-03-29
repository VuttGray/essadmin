﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.Admin.Worker.Settings
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string DefaultRecipient { get; set; }
    }
}
