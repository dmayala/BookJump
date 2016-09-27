using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookJump.Models
{
    public class MineViewModel
    {
        public IEnumerable<Book> MyBooks { get; internal set; }

        [Required]
        public string Title { get; set; }
    }
}
