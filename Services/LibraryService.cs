using library_automation_back_end.Features.FeaturesLibrary.Requests;
using library_automation_back_end.Features.FeaturesLibrary.Responses;

namespace library_automation_back_end.Services
{
    public class LibraryService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<GetBookResponse?> GetBook(GetBookRequest request)
        {
            Book? book = await dataContext.Books.Include(b => b.Language).Include(b => b.Publisher).Include(b => b.BookAuthors!).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories!).ThenInclude(bc => bc.Category).FirstOrDefaultAsync(b => b.Id == request.BookId);
            if (book == null)
                return null;

            GetBookResponse response = new(book);
            return response;
        }

        public async Task<SearchResultResponse?> GetSearchedBooks(SearchBookRequest request)
        {
            IQueryable<Book> query = dataContext.Books.Include(b => b.Publisher).Include(b => b.Language)
                .Include(b => b.BookAuthors!).ThenInclude(ba => ba.Author).Include(b => b.BookCategories!).ThenInclude(bc => bc.Category);

            if (!string.IsNullOrEmpty(request.Search))
            {
                string search = request.Search.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(search) || b.BookAuthors!.Any(ba => ba.Author!.Name.ToLower().Contains(search)) ||
                    b.Publisher!.Name.ToLower().Contains(search) || b.BookCategories!.Any(bc => bc.Category!.Name.ToLower().Contains(search)));
            }
            int totalCount = await query.CountAsync();
            if (totalCount == 0)
                return null;

            ICollection<Book> books = await query.Skip(request.Page * request.Size).Take(request.Size).ToListAsync();
            SearchResultResponse response = new(totalCount, books);
            return response;
        }
    }
}
