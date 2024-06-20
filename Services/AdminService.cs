namespace library_automation_back_end.Services
{
    public class AdminService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<GetAllDesiresResponse?> GetAllDesires()
        {
            ICollection<UserDesire>? userDesires = await dataContext.UserDesires.Include(ud => ud.User).Include(ud => ud.Book).Include(ud => ud.DesireSituation).ToListAsync();
            if (userDesires == null)
                return null;

            GetAllDesiresResponse response = new(userDesires);
            return response;
        }

        public async Task<DesireResponse> AcceptBorrow(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud =>
                ud.Id == request.DesireId && ud.DesireSituationId == (int)DesireSituationEnum.Borrow);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user desire found for the Id in request." };
            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            if (book!.Count <= 0)
                return new() { Succeeded = false, Message = "Out of stock." };

            book.Count -= 1;
            DateTime returnDueDate = DateTime.UtcNow.Date.AddDays(8).AddTicks(-1);
            UserBookBorrow ubb = new()
            {
                UserId = userDesire.UserId,
                BookId = userDesire.BookId,
                BorrowDate = DateTime.UtcNow,
                ReturnDueDate = returnDueDate,
                BorrowSituationId = (int)BorrowSituationEnum.Borrowed
            };
            dataContext.UserDesires.Remove(userDesire);
            dataContext.Books.Update(book);
            dataContext.UserBookBorrows.Add(ubb);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The borrow request has been approved!" };
        }

        public async Task<DesireResponse> AcceptReserveBorrow(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud =>
                ud.Id == request.DesireId && ud.DesireSituationId == (int)DesireSituationEnum.ReserveBorrow);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user desire found for the Id in request." };
            UserBookReserve? ubr = await dataContext.UserBookReserves.FirstOrDefaultAsync(ubr =>
                ubr.UserId == userDesire.UserId && ubr.BookId == userDesire.BookId && ubr.ReserveSituationId == (int)ReserveSituationEnum.Waiting);
            if (ubr == null)
                return new() { Succeeded = false, Message = "There is no reservation for selected book." };
            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            if (book == null)
                return new() { Succeeded = false, Message = "Invalid bookId." };

            ubr!.ReserveSituationId = (int)ReserveSituationEnum.Borrowed;
            ubr.BorrowDate = DateTime.Now;
            DateTime returnDueDate = DateTime.UtcNow.Date.AddDays(8).AddTicks(-1);
            UserBookBorrow ubb = new()
            {
                UserId = userDesire.UserId,
                BookId = userDesire.BookId,
                BorrowDate = DateTime.UtcNow,
                ReturnDueDate = returnDueDate,
                BorrowSituationId = (int)BorrowSituationEnum.Borrowed
            };
            dataContext.UserDesires.Remove(userDesire);
            dataContext.UserBookReserves.Update(ubr);
            dataContext.UserBookBorrows.Add(ubb);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The borrow request has been approved!" };
        }

        public async Task<DesireResponse> AcceptReturn(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud =>
                ud.Id == request.DesireId && ud.DesireSituationId == (int)DesireSituationEnum.Return);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user desire found for the Id in request." };
            UserBookBorrow? ubb = await dataContext.UserBookBorrows.FirstOrDefaultAsync(ubb => ubb.UserId == userDesire.UserId && ubb.BookId == userDesire.BookId);
            if (ubb == null)
                return new() { Succeeded = false, Message = "No borrowing found for the Id in request." };
            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            if (book == null)
                return new() { Succeeded = false, Message = "Invalid bookId." };

            book!.Count += 1;
            ubb!.ReturnDate = DateTime.Now;
            ubb!.BorrowSituationId = (int)BorrowSituationEnum.Returned;
            dataContext.UserDesires.Remove(userDesire);
            dataContext.UserBookBorrows.Update(ubb);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The return request has been approved!" };
        }

        public async Task<DesireResponse> RejectDesire(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.Id == request.DesireId);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user desire found for the Id in request." };

            dataContext.UserDesires.Remove(userDesire);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The request has been rejected!" };
        }

        public async Task<AddResponse> AddBook(AddBookRequest request)
        {
            if (request.AuthorIds.Count == 0)
                return new() { Succeeded = false, Message = "Authors must not be empty." };
            if (request.CategoryIds.Count == 0)
                return new() { Succeeded = false, Message = "Categories must not be empty." };
            bool isLanguageExist = await dataContext.Languages.Where(l => l.Id == request.LanguageId).AnyAsync();
            if (!isLanguageExist)
                return new() { Succeeded = false, Message = "Language does not exist in the database." };
            bool isPublisherExist = await dataContext.Publishers.Where(p => p.Id == request.PublisherId).AnyAsync();
            if (!isPublisherExist)
                return new() { Succeeded = false, Message = "Publisher does not exist in the database." };
            bool areAuthorsExist = await dataContext.Authors.Where(a => request.AuthorIds.Contains(a.Id)).CountAsync() == request.AuthorIds.Count;
            if (!areAuthorsExist)
                return new() { Succeeded = false, Message = "One or more authors do not exist in the database." };
            bool areCategoriesExist = await dataContext.Categories.Where(c => request.CategoryIds.Contains(c.Id)).CountAsync() == request.CategoryIds.Count;
            if (!areCategoriesExist)
                return new() { Succeeded = false, Message = "One or more categories do not exist in the database." };
            Book? existingBook = await dataContext.Books.Include(b => b.BookAuthors!).ThenInclude(ba => ba.Author).FirstOrDefaultAsync(b =>
                b.Name == request.Name && b.Year == request.Year && b.LanguageId == request.LanguageId && b.PublisherId == request.PublisherId);
            if (existingBook != null)
                return new() { Succeeded = false, Message = "The book already exists." };

            Book book = new()
            {
                Name = request.Name,
                Year = request.Year,
                Count = request.Count,
                PageCount = request.PageCount,
                LanguageId = request.LanguageId,
                PublisherId = request.PublisherId,
                BookAuthors = request.AuthorIds.Select(authorId => new BookAuthor { AuthorId = authorId }).ToList(),
                BookCategories = request.CategoryIds.Select(categoryId => new BookCategory { CategoryId = categoryId }).ToList()
            };
            dataContext.Books.Add(book);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Book added successfully!" };
        }

        public async Task<AddResponse> AddPublisher(AddPublisherRequest request)
        {
            Publisher? existingPublisher = await dataContext.Publishers.Include(p => p.Country).FirstOrDefaultAsync(p =>
                p.Name == request.Publisher.Name && p.CountryId == request.Publisher.CountryId);
            if (existingPublisher == null)
                return new() { Succeeded = false, Message = "Publisher already exist!" };
            dataContext.Publishers.Add(request.Publisher);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Publisher has been registered." };
        }

        public async Task<AddResponse> AddAuthor(AddAuthorRequest request)
        {
            int count = 0;
            foreach (Author author in request.Authors)
            {
                Author? existingAuthor = await dataContext.Authors.Include(a => a.Country).FirstOrDefaultAsync(a =>
                    a.Name == author.Name && a.CountryId == author.CountryId);
                if (existingAuthor != null)
                    continue;
                count++;
                dataContext.Authors.Add(author);
                await dataContext.SaveChangesAsync();
            }
            return new() { Succeeded = count > 0, Message = $"Number of registered authors: {count}." };
        }

        public async Task<AddResponse> AddCategory(AddCategoryRequest request)
        {
            int count = 0;
            foreach (Category category in request.Categories)
            {
                Category? existingCategory = await dataContext.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (existingCategory != null)
                    continue;
                count++;
                dataContext.Categories.Add(category);
                await dataContext.SaveChangesAsync();
            }
            return new() { Succeeded = count > 0, Message = $"Number of registered categories: {count}." };
        }

    }
}
