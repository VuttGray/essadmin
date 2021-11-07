using System;
using System.Collections.Generic;
using ESS.Admin.Core.Domain.Administration;

namespace ESS.Admin.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<User> Users => new List<User>()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "iivanov@mail.ru"
            },
            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Petr",
                LastName = "Petrov",
                Email = "ppetrov@mail.ru"
            },
        };
        public static IEnumerable<Message> Messages => new List<Message>()
        {
            new Message()
            {
                Id = Guid.NewGuid(),
                Subject = "Test message",
                Body = "This is first test message to check the getting information",
            },
            new Message()
            {
                Id = Guid.NewGuid(),
                Subject = "Alert!",
                Body = "Check everything as something is wrong!",
            },
        };
    }
}