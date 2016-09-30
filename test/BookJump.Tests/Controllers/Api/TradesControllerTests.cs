using BookJump.Controllers.Api;
using BookJump.Data;
using BookJump.Dtos;
using BookJump.Models;
using BookJump.Tests.Extensions;
using BookJump.Tests.Fakes;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BookJump.Tests.Controllers.Api
{
    [TestFixture]
    public class TradesControllerTests
    {
        private TradesController _controller;
        private Mock<IBookJumpRepository> _mockRepository;
        private FakeUserManager _userManager;
        private string _userId;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IBookJumpRepository>();
            _userManager = new FakeUserManager();
            _controller = new TradesController(_mockRepository.Object, _userManager);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [Test]
        public void RequestTrade_UserBorrowsOwnBook_ShouldReturnBadRequest()
        {
            // Arrange
            var book = new Book { OwnerId = _userId };
            var dto = new TradeRequestDto { BookId = book.Id };
            _mockRepository.Setup(r => r.GetBook(book.Id)).Returns(book);

            // Act
            var result = _controller.RequestTrade(dto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public void RequestTrade_UserPreviouslyRequested_ShouldReturnBadRequest()
        {
            // Arrange
            var book = new Book();
            var dto = new TradeRequestDto { BookId = book.Id };
            _mockRepository.Setup(r => r.GetBook(book.Id)).Returns(book);
            _mockRepository.Setup(r => r.GetTradeRequest(dto.BookId, _userId)).Returns(new TradeRequest());

            // Act
            var result = _controller.RequestTrade(dto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public void RequestTrade_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var book = new Book();
            var dto = new TradeRequestDto { BookId = book.Id };
            _mockRepository.Setup(r => r.GetBook(book.Id)).Returns(book);

            // Act
            var result = _controller.RequestTrade(dto);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
