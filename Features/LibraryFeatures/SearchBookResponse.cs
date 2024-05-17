namespace library_automation_back_end.Features.LibraryFeatures
{
    public class SearchBookResponse(Book book)
    {
        public string Name { get; set; } = book.Name;
    }
}
