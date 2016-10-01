using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookJump.Controllers.Web;
using BookJump.Data;
using BookJump.Models;
using BookJump.Tests.Extensions;
using BookJump.Tests.Fakes;
using BookJump.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace BookJump.Tests.Controllers.Web
{
    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IBookJumpRepository> _mockRepository;
        private Mock<ILogger<BooksController>> _logger;
        private FakeUserManager _userManager;
        private BooksController _controller;
        private string _userId;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IBookJumpRepository>();
            _logger = new Mock<ILogger<BooksController>>();
            _userManager = new FakeUserManager();
            _controller = new BooksController(_mockRepository.Object, _logger.Object, _userManager);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [Test]
        public void Mine_ValidRequest_ShouldReturnViewResultWithMyBooks()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetBooksByOwner(_userId))
                .Returns(GetTestLibrary().Where(l => l.OwnerId == _userId));

            // Act
            var result = _controller.Mine();

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>();
            var model = viewResult.Subject.ViewData.Model.Should().BeOfType<MineViewModel>();
            var library = model.Subject.MyBooks;
            library.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.OnlyContain(b => b.OwnerId == _userId);
        }

        [Test]
        public async Task Create_ReceivesInvalidModel_ShouldReturnUnalteredViewResult()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetBooksByOwner(_userId))
                .Returns(GetTestLibrary().Where(l => l.OwnerId == _userId));
            var vm = new MineViewModel();
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = await _controller.Create(vm);

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>();
            var model = viewResult.Subject.ViewData.Model.Should().BeOfType<MineViewModel>();
            var library = model.Subject.MyBooks;
            library.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.OnlyContain(b => b.OwnerId == _userId);
        }

        [Test]
        public async Task Create_ReceivesValidModel_ShouldReturnViewResultWithNewBook()
        {
            // Arrange
            var testLibrary = GetTestLibrary().ToList();
            _mockRepository.Setup(repo => repo.GetBooksByOwner(_userId))
                .Returns(testLibrary.Where(l => l.OwnerId == _userId));
            _mockRepository.Setup(repo => repo.AddBook(It.IsAny<Book>()))
                .Callback((Book book) => testLibrary.Add(book));

            var vm = new MineViewModel() { Title = "C#" };

            // Act
            var result = await _controller.Create(vm);

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>();
            var model = viewResult.Subject.ViewData.Model.Should().BeOfType<MineViewModel>();
            var library = model.Subject.MyBooks;
            library.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.OnlyContain(b => b.OwnerId == _userId);
        }

        private IEnumerable<Book> GetTestLibrary()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Test One",
                    OwnerId = _userId
                },
                new Book()
                {
                    Id = 2,
                    Title = "Test Two",
                    OwnerId = _userId
                },
                     new Book()
                {
                    Id = 2,
                    Title = "Test Three",
                    OwnerId = _userId + "3"
                }
            };
        }
    }
}
