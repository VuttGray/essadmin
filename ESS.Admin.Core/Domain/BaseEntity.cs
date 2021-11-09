using System;

namespace ESS.Admin.Core.Domain
{
    public class BaseEntity
    {
        public Guid RecordId { get; set; }
        public int RecordStatus { get; set; } = 1;
        public bool IsDeleted { get; set; } = false;
    }
}
