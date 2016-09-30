using BookJump.Data;
using BookJump.Models;
using BookJump.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookJump.Controllers.Web
{
    public class TradesController : Controller
    {
        private readonly IBookJumpRepository _repository;
        private readonly ILogger<BooksController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public TradesController(IBookJumpRepository repository,
            ILogger<BooksController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            var viewModel = new TradesViewModel()
            {
                BorrowRequests = _repository.GetBorrowRequests(userId),
                LendingRequests = _repository.GetLendingRequests(userId)
            };

            return View(viewModel);
        }
    }
}