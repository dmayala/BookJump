using System.Collections.Generic;
using BookJump.Models;

namespace BookJump.Data
{
    public interface IBookJumpRepository
    {
        bool SaveAll();
        void AddBook(Book book);
        Book GetBook(int bookId);
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooksByOwner(string ownerId);
        TradeRequest GetTradeRequest(int bookId, string borrowerId);
        void AddTradeRequest(TradeRequest tradeRequest);
        IEnumerable<TradeRequest> GetBorrowRequests(string userId);
        IEnumerable<TradeRequest> GetLendingRequests(string userId);
    }
}