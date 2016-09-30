namespace BookJump.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }

        public ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
