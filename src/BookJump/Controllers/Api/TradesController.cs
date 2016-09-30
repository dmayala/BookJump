using BookJump.Data;
using BookJump.Dtos;
using BookJump.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookJump.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/trades")]
    public class TradesController : Controller
    {
        private readonly IBookJumpRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TradesController(IBookJumpRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult RequestTrade([FromBody] TradeRequestDto dto)
        {
            var userId = _userManager.GetUserId(User);

            var tradeRequest = _repository.GetTradeRequest(dto.BookId, userId);
            if (tradeRequest != null)
                return BadRequest(tradeRequest.Book.OwnerId == userId ? 
                    "You cannot borrow your own book." :
                    "You have already requested this book.");

            tradeRequest = new TradeRequest()
            {
                BookId = dto.BookId,
                BorrowerId = userId
            };

            _repository.AddTradeRequest(tradeRequest);
            _repository.SaveAll();

            return Ok();
        }
    }
}