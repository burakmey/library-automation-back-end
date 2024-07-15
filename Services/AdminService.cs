using library_automation_back_end.Features.FeaturesAdmin.Requests;
using library_automation_back_end.Features.FeaturesAdmin.Responses;

namespace library_automation_back_end.Services
{
    public class AdminService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<GetAllDesiresResponse?> GetAllDesires()
        {
            ICollection<UserDesire> userDesires = await dataContext.UserDesires.Include(ud => ud.User).Include(ud => ud.Book).Include(ud => ud.DesireSituation).ToListAsync();
            GetAllDesiresResponse response = new(userDesires);
            return response;
        }

        public async Task<GetBorrowedBooksResponse?> GetBorrowedBooks()
        {
            ICollection<UserBookBorrow> userBookBorrows = await dataContext.UserBookBorrows.Include(ubb => ubb.User).Include(ubb => ubb.Book)
                .Include(ubb => ubb.BorrowSituation).Where(ubb => ubb.BorrowSituationId != (int)BorrowSituationEnum.Returned).ToListAsync();
            GetBorrowedBooksResponse response = new(userBookBorrows);
            return response;
        }

        public async Task<GetReservedBooksResponse?> GetReservedBooks()
        {
            ICollection<UserBookReserve> userBookReserves = await dataContext.UserBookReserves.Include(ubb => ubb.User).Include(ubb => ubb.Book)
                .Include(ubb => ubb.ReserveSituation).Where(ubr => ubr.ReserveSituationId != (int)ReserveSituationEnum.Borrowed).ToListAsync();
            GetReservedBooksResponse response = new(userBookReserves);
            return response;
        }

        public async Task<MessageResponse> AcceptBorrow(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud =>
                ud.Id == request.DesireId && ud.DesireSituationId == (int)DesireSituationEnum.Borrow);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user borrow desire found for the Id in request!" };
            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            if (book == null)
                return new() { Succeeded = false, Message = "No book found for the desire!" };
            else if (book.Count <= 0)
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

        public async Task<MessageResponse> AcceptReserveBorrow(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.Id == request.DesireId && ud.DesireSituationId == (int)DesireSituationEnum.ReserveBorrow);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user reserve-borrow desire found for the Id in request!" };
            UserBookReserve? ubr = await dataContext.UserBookReserves.FirstOrDefaultAsync(ubr => ubr.UserId == userDesire.UserId && ubr.BookId == userDesire.BookId && ubr.ReserveSituationId == (int)ReserveSituationEnum.Waiting);
            if (ubr == null)
                return new() { Succeeded = false, Message = "No reservation found for the desired book!" };
            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            if (book == null)
                return new() { Succeeded = false, Message = "No book found for the desire!" };

            ubr.ReserveSituationId = (int)ReserveSituationEnum.Borrowed;
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
            return new() { Succeeded = true, Message = "The reserve-borrow desire has been approved." };
        }

        public async Task<MessageResponse> AcceptReturn(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.Id == request.DesireId && ud.DesireSituationId == (int)DesireSituationEnum.Return);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user return desire found for the Id in request!" };
            UserBookBorrow? ubb = await dataContext.UserBookBorrows.FirstOrDefaultAsync(ubb => ubb.UserId == userDesire.UserId && ubb.BookId == userDesire.BookId);
            if (ubb == null)
                return new() { Succeeded = false, Message = "No borrowing found for the desire!" };
            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            if (book == null)
                return new() { Succeeded = false, Message = "No book found for the desire!" };

            book.Count += 1;
            ubb.ReturnDate = DateTime.Now;
            ubb.BorrowSituationId = (int)BorrowSituationEnum.Returned;
            dataContext.UserDesires.Remove(userDesire);
            dataContext.Books.Update(book);
            dataContext.UserBookBorrows.Update(ubb);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The return desire has been approved." };
        }

        public async Task<MessageResponse> RejectDesire(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.Id == request.DesireId);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user desire found for the Id in request!" };

            dataContext.UserDesires.Remove(userDesire);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The desire has been rejected." };
        }

        public async Task<MessageResponse> AddBook(AddBookRequest request)
        {
            List<string> distinctAuthors = request.Authors.Distinct().ToList();
            List<string> distinctCategories = request.Categories.Distinct().ToList();
            MessageResponse response = new() { Succeeded = true, Message = "" };
            if (distinctAuthors.Count != request.Authors.Count)
            {
                response.Succeeded = false;
                response.Message += "-Duplicate authors found in the request!";
            }
            if (distinctCategories.Count != request.Categories.Count)
            {
                response.Succeeded = false;
                response.Message += "-Duplicate categories found in the request!";
            }
            if (!response.Succeeded)
                return response;

            Language? language = await dataContext.Languages.FirstOrDefaultAsync(l => l.Name == request.Language);
            Publisher? publisher = await dataContext.Publishers.FirstOrDefaultAsync(p => p.Name == request.Publisher);
            List<Author> authors = await dataContext.Authors.Where(a => request.Authors.Contains(a.Name)).ToListAsync();
            List<Category> categories = await dataContext.Categories.Where(c => request.Categories.Contains(c.Name)).ToListAsync();

            if (language == null)
            {
                response.Succeeded = false;
                response.Message = $"-Language does not exist in the database! (Language: ${request.Language})";
            }
            if (publisher == null)
            {
                response.Succeeded = false;
                response.Message += $"-Publisher does not exist in the database! (Publisher: ${request.Publisher})";
            }
            if (authors.Count != request.Authors.Count)
            {
                response.Succeeded = false;
                List<string> notExistAuthors = request.Authors.Where(exist => !authors.Any(notExist => notExist.Name == exist)).ToList();
                response.Message += $"-Authors do not exist in the database! (Authors: ${string.Join(", ", notExistAuthors)})";
            }
            if (categories.Count != request.Categories.Count)
            {
                response.Succeeded = false;
                List<string> notExistCategories = request.Categories.Where(exist => !categories.Any(notExist => notExist.Name == exist)).ToList();
                response.Message += $"-Categories do not exist in the database! (Categories: ${string.Join(", ", notExistCategories)})";
            }
            if (!response.Succeeded) 
                return response;

            Book? existingBook = await dataContext.Books.FirstOrDefaultAsync(b => b.Name == request.Name && b.Year == request.Year && b.LanguageId == language!.Id && b.PublisherId == publisher!.Id);
            if (existingBook != null)
                return new() { Succeeded = false, Message = "The book already exists!" };

            Book book = new()
            {
                Name = request.Name,
                Year = request.Year,
                Count = request.Count,
                PageCount = request.PageCount,
                LanguageId = language!.Id,
                PublisherId = publisher!.Id,
                BookAuthors = authors.Select(author => new BookAuthor { AuthorId = author.Id }).ToList(),
                BookCategories = categories.Select(category => new BookCategory { CategoryId = category.Id }).ToList()
            };
            dataContext.Books.Add(book);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Book added successfully." };
        }

        public async Task<MessageResponse> AddPublisher(AddPublisherRequest request)
        {
            Publisher? existingPublisher = await dataContext.Publishers.Include(p => p.Country).FirstOrDefaultAsync(p => p.Name == request.Name && p.Country!.Name == request.Country);
            if (existingPublisher != null)
                return new() { Succeeded = false, Message = "Publisher already exist!" };
            Country? country = await dataContext.Countries.FirstOrDefaultAsync(c => c.Name == request.Country);
            if (country == null)
                return new() { Succeeded = false, Message = "Country is not exist!" };

            Publisher publisher = new()
            {
                Name = request.Name,
                CountryId = country.Id
            };
            dataContext.Publishers.Add(publisher);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Publisher has been registered." };
        }

        public async Task<MessageResponse> AddAuthor(AddAuthorRequest request)
        {
            List<string> distinctAuthors = request.Authors.Select(a => a.Name).Distinct().ToList();
            MessageResponse response = new() { Succeeded = true, Message = "" };
            if (distinctAuthors.Count != request.Authors.Count)
            {
                response.Succeeded = false;
                response.Message += "-Duplicate authors found in the request!";
            }
            if (!response.Succeeded)
                return response;

            List<AuthorRequestModel> authorList = [];
            List<Country> countryList = [];
            foreach (AuthorRequestModel author in request.Authors)
            {
                Author? existingAuthor = await dataContext.Authors.Include(a => a.Country)
                    .FirstOrDefaultAsync(a => a.Name == author.Name && a.Country!.Name == author.Country);
                if (existingAuthor != null)
                {
                    response.Succeeded = false;
                    response.Message += $"-Author: {existingAuthor.Name} is already exist!";
                }
                else
                    authorList.Add(author);

                Country? country = await dataContext.Countries.FirstOrDefaultAsync(c => c.Name == author.Country);
                if (country == null)
                {
                    response.Succeeded = false;
                    response.Message += $"-Country: {author.Country} is not exist!";
                }
                else 
                    countryList.Add(country);
            }
            if (!response.Succeeded)
                return response;

            for (int i = 0; i < request.Authors.Count; i++)
            {
                Author author = new() { Name = authorList[i].Name, CountryId = countryList[i].Id };
                dataContext.Authors.Add(author);
            }
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = $"Number of registered authors: {authorList.Count}." };
        }

        public async Task<MessageResponse> AddCategory(AddCategoryRequest request)
        {
            List<string> distinctCategories = request.Categories.Select(c => c.Name).Distinct().ToList();
            MessageResponse response = new() { Succeeded = true, Message = "" };
            if (distinctCategories.Count != request.Categories.Count)
            {
                response.Succeeded = false;
                response.Message += "-Duplicate categories found in the request!";
            }
            if (!response.Succeeded)
                return response;

            List<CategoryRequestModel> categoryList = [];
            foreach (CategoryRequestModel category in request.Categories)
            {
                Category? existingCategory = await dataContext.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (existingCategory != null)
                {
                    response.Succeeded = false;
                    response.Message += $"-Country: {category.Name} is already exist!";
                }
                else
                    categoryList.Add(category);
            }
            if (!response.Succeeded)
                return response;

            for (int i = 0; i < request.Categories.Count; i++)
            {
                Category category = new() { Name = categoryList[i].Name};
                dataContext.Categories.Add(category);
            }
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = $"Number of registered categories: {categoryList.Count}." };
        }
    }
}
