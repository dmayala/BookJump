using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookJump.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookJump.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookJumpRepository _repository;

        public HomeController(IBookJumpRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var allBooks = _repository.GetBooks();
            return View(allBooks);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
