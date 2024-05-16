namespace library_automation_back_end.Services
{
    public class LibraryService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<GetBookResponse?> GetBook(GetBookRequest request)
        {
            Book? book = await dataContext.Books.FirstOrDefaultAsync(book => book.Id == request.BookId);
            if (book == null)
                return null;
            GetBookResponse response = new(book);
            return response;
        }
        public async Task<SearchBookResponse?> GetSearchedBooks(SearchBookRequest request)
        {
            IQueryable<Book> query = dataContext.Books;
            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(b => b.Name.Contains(request.Search) ||
                b.BookAuthors!.Any(ba => ba.Author!.Name.Contains(request.Search) ||
                b.Publisher!.Name.Contains(request.Search)));
            int totalCount = await query.CountAsync();
            if (totalCount == 0)
                return null;
            ICollection<Book> books = await query.Skip(request.Page * request.Size).Take(request.Size).ToListAsync();
            SearchBookResponse response = new(totalCount, books);
            return response;
        }
    }
}
