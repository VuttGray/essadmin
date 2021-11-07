﻿using System;

namespace ESS.Admin.Core.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
