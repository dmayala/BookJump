using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookJump.Models
{
    public class TradeRequest
    {
        public Book Book { get; set; }
        public ApplicationUser Borrower { get; set; }

        public int BookId { get; set; }
        public string BorrowerId { get; set; }
        public bool IsApproved { get; set; }
    }
}
