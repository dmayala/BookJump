using System.Collections.Generic;
using System.Linq;
using BookJump.Models;
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

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Book> GetBooksByOwner(string ownerId)
        {
            return _context.Books.Where(b => b.OwnerId == ownerId).ToList();
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
