namespace library_automation_back_end.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
