using System;
using System.Collections.Generic;
using System.Linq;
using BookJump.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookJump.Data
{
    public class BookJumpRepository : IBookJumpRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IBookJumpRepository> _logger;

        public BookJumpRepository(ApplicationDbContext context, ILogger<IBookJumpRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public Book GetBook(int bookId)
        {
            return _context.Books.SingleOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Book> GetBooksByOwner(string ownerId)
        {
            return _context.Books
                .Where(b => b.OwnerId == ownerId)
                .ToList();
        }

        public void AddTradeRequest(TradeRequest tradeRequest)
        {
            _context.TradeRequests.Add(tradeRequest);
        }

        public TradeRequest GetTradeRequest(int bookId, string borrowerId)
        {
            return _context.TradeRequests
                .Include(tr => tr.Book)
                .SingleOrDefault(tr => tr.BookId == bookId && tr.BorrowerId == borrowerId);
        }

        public IEnumerable<TradeRequest> GetBorrowRequests(string userId)
        {
            return _context.TradeRequests
                .Include(tr => tr.Book)
                .Where(tr => tr.BorrowerId == userId)
                .ToList();
        }

        public IEnumerable<TradeRequest> GetLendingRequests(string userId)
        {
            return _context.TradeRequests
                .Include(tr => tr.Book)
                .Where(tr => tr.Book.OwnerId == userId)
                .ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
