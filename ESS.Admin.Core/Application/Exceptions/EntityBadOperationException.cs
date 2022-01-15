using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.Admin.Core.Application.Exceptions
{
    public class EntityBadOperationException : Exception
    {
        public EntityBadOperationException() { }

        public EntityBadOperationException(string title, string id, string message)
            : base($"{title} [{id}]{message}")
        {

        }
    }
}
