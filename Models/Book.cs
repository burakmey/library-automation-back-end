namespace library_automation_back_end.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Year { get; set; }
        public required int Count { get; set; }
        public required int PageCount { get; set; }
        [ForeignKey(nameof(Language))] public required int LanguageId { get; set; }
        [ForeignKey(nameof(Publisher))] public required int PublisherId { get; set; }
        public Language? Language { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
        public ICollection<UserBookBorrow>? UserBookBorrows { get; set; }
        public ICollection<UserBookReserve>? UserBookReserves { get; set; }
    }
}
