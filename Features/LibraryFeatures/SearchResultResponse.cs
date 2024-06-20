namespace library_automation_back_end.Features.LibraryFeatures
{
    public class SearchResultResponse(int totalCount, ICollection<Book> books)
    {
        public int TotalCount { get; set; } = totalCount;
        public ICollection<string> BookAuthors { get; set; } = books.Select(b => b.Name).ToList();
        //public ICollection<SearchBookResponse> Books { get; set; } = books.Select(book => new SearchBookResponse(book)).ToList();

    }
}
