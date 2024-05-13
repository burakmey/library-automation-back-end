namespace library_automation_back_end.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public Book? Book { get; set; }
        public Author? Author { get; set; }
    }
}
