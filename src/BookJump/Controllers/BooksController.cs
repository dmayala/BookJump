using System.Collections.Generic;
using System.Threading.Tasks;
using BookJump.Data;
using BookJump.Models;
using BookJump.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookJump.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookJumpRepository _repository;
        private readonly ILogger<BooksController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(IBookJumpRepository repository, 
            ILogger<BooksController> logger, 
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Mine()
        {
            var userId = _userManager.GetUserId(User);
            var viewModel = new MineViewModel()
            {
                MyBooks = _repository.GetBooksByOwner(userId) ?? new List<Book>()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MineViewModel viewModel)
        {
            var userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {

                var service = new BooksService();
                var volume = await service.SearchTitleAsync(viewModel.Title);

                var book = new Book
                {
                    Title = volume.VolumeInfo.Title,
                    Cover = volume.VolumeInfo.ImageLinks?.Thumbnail,
                    OwnerId = userId
                };

                _repository.AddBook(book);
                _repository.SaveAll();
            }

            viewModel.MyBooks = _repository.GetBooksByOwner(userId);

            return View("Mine", viewModel);
        }
    }
}
