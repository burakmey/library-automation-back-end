namespace library_automation_back_end.Features.LibraryFeatures
{
    public class GetBookResponse(Book book)
    {
        public string Name { get; set; } = book.Name;
        public int Year { get; set; } = book.Year;
        public int Count { get; set; } = book.Count;
        public int PageCount { get; set; } = book.PageCount;
        public string Language { get; set; } = book.Language!.Name;
        public string Publisher { get; set; } = book.Publisher!.Name;
        public ICollection<string> BookAuthors { get; set; } = book.BookAuthors!.Select(ba => ba.Author!.Name).ToList();
        public ICollection<string> BookCategories { get; set; } = book.BookCategories!.Select(bc => bc.Category!.Name).ToList();
    }
}
