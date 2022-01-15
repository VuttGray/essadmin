using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.Admin.Core.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string title, string id)
            : base($"{title} with key [{id}] not found")
        {

        }
    }
}
