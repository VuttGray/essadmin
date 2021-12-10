﻿using System;
using System.Collections.Generic;
using ESS.Admin.Core.Domain.Administration;

namespace ESS.Admin.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<Message> Messages => GetMessages();

        public static IEnumerable<User> Users => new List<User>()
        {
            new User()
            {
                RecordId = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "iivanov@mail.ru"
            },
            new User()
            {
                RecordId = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                FirstName = "Petr",
                LastName = "Petrov",
                Email = "ppetrov@mail.ru"
            },
        };

        private static IEnumerable<Message> GetMessages()
        {
            var messages = new List<Message>();
            var message = new Message
            {
                RecordId = Guid.NewGuid(), // Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Subject = "Test message",
                Body = "This is first test message to check the getting information"
            };
            message.AddRecipients("iivanov@ficmail.ru");
            messages.Add(message);

            message = new Message
            {
                RecordId = Guid.NewGuid(), //Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Subject = "Alert!", 
                Body = "Check everything as something is wrong!"
            };
            message.AddRecipients("ppetrov@ficmail.ru");
            messages.Add(message);

            return messages;
        }
    }
}