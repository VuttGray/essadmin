using ESS.Admin.Core.Domain.Administration;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ESS.UnitTests.Core.Domain.Administration
{
    public class MessageTests
    {
        [Fact]
        public void SetContext_SuccessParsing()
        {
            //Arrange
            var message = new Message { 
                RecordId = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Subject = "$PROJECT_NAME: new project team added",
                Body = "A new team member $TEAM_MEMBER_FULL_NAME has been added in $PROJECT_NAME",
                Recipients = new List<string> { "$PM_EMAIL", "tester@testmail.com", "PC_EMAILS" }
            };

            var context = new Dictionary<string, string> {
                { "PROJECT_NAME", "TEST-01" },
                { "TEAM_MEMBER_FULL_NAME", "Sidorov, Ivan" },
                { "PM_EMAIL", "pm@testmail.com" },
                { "PC_EMAILS", "pc1@testmail.com;pc2@testmail.com" },
            };
            var expectedSubject = "TEST-01: new project team added";
            var expectedBody = "A new team member Sidorov, Ivan has been added in TEST-01";
            var expectedRecipients = new List<string> { "tester@testmail.com", "pm@testmail.com", "pc1@testmail.com", "pc2@testmail.com" };

            //Act
            message.SetContext(context);

            //Assert
            message.Subject.Should().Be(expectedSubject);
            message.Body.Should().Be(expectedBody);
            message.Recipients.Should().BeEquivalentTo(expectedRecipients);
        }

        [Fact]
        public void Message_DefaultValues()
        {
            //Arrange
            var expectedAttempts = 0;
            var expectedStatus = 1;
            var now = DateTime.Now;

            //Act
            var message = new Message { };

            //Assert
            message.RecordId.Should().BeEmpty();
            message.Subject.Should().BeNull();
            message.Body.Should().BeNull();
            message.BodyPreview.Should().BeNull();
            message.Recipients.Should().BeEmpty();
            message.CcRecipients.Should().BeEmpty();
            message.BccRecipients.Should().BeEmpty();
            message.Attachments.Should().BeEmpty();
            message.Priority.Should().Be((int)MessagePriority.Normal);
            //message.CreatedDate.Should().Be(now);
            message.Attempts.Should().Be(expectedAttempts);
            message.SentDate.Should().BeNull();
            message.RecordStatus.Should().Be(expectedStatus);
        }

        [Fact]
        public void IncrementAttempts_Success()
        {
            //Arrange
            var message = new Message { };
            var expectedAttempts = 1;

            //Act
            message.IncrementAttempts();

            //Assert
            message.Attempts.Should().Be(expectedAttempts);
        }

        [Fact]
        public void MarkSent_Success()
        {
            //Arrange
            var message = new Message { };
            var now = DateTime.Now;
            var expectedAttempts = 1;
            var expectedStatus = 3;

            //Act
            message.MarkSent(now);

            //Assert
            message.Attempts.Should().Be(expectedAttempts);
            message.SentDate.Should().Be(now);
            message.RecordStatus.Should().Be(expectedStatus);
        }
    }
}
