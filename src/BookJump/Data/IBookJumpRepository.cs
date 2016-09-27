using System.Collections.Generic;
using BookJump.Models;

namespace BookJump.Data
{
    public interface IBookJumpRepository
    {
        bool SaveAll();
        void AddBook(Book book);
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooksByOwner(string ownerId);
    }
}