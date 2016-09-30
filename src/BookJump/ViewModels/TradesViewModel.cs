using System.Collections.Generic;
using BookJump.Models;

namespace BookJump.ViewModels
{
    public class TradesViewModel
    {
        public IEnumerable<TradeRequest> BorrowRequests { get; set; }
        public IEnumerable<TradeRequest> LendingRequests { get; set; }
    }
}