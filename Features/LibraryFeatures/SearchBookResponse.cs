namespace library_automation_back_end.Features.LibraryFeatures
{
    public class SearchBookResponse(int totalCount, ICollection<Book> books)
    {
        public int TotalCount { get; set; } = totalCount;
        public ICollection<GetBookResponse> Books { get; set; } = books.Select(book => new GetBookResponse(book)).ToList();

    }
}
