using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ESS.UnitTests.Core.Application.Services
{
    public class BaseServiceTests
    {
        private readonly Mock<IRepository<Message>> _messagesRepositoryMock;

        public BaseServiceTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            _messagesRepositoryMock = fixture.Freeze<Mock<IRepository<Message>>>();
        }

        [Fact]
        public void TestMethod()
        {
            // Arrange
            var id = Guid.Parse("");
            Message message = null;

            _messagesRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(message);

            // Act
            // Assert
        }
    }
}
