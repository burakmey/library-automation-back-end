namespace library_automation_back_end.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public Book? Book { get; set; }
        public Category? Category { get; set; }
    }
}
