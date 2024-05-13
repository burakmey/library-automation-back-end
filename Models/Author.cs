namespace library_automation_back_end.Models
{
    public class Author
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [ForeignKey(nameof(Country))] public required int CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
