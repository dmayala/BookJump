using System.ComponentModel.DataAnnotations;

namespace BookJump.Dtos
{
    public class TradeRequestDto
    {
        [Required]
        public int BookId { get; set; }

        public string BorrowerId { get; set; }

        public bool IsApproved { get; set; }
    }
}