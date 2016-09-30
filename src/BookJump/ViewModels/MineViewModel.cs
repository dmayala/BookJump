using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookJump.Models;

namespace BookJump.ViewModels
{
    public class MineViewModel
    {
        public IEnumerable<Book> MyBooks { get; internal set; }

        [Required]
        public string Title { get; set; }
    }
}
