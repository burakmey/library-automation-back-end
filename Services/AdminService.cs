namespace library_automation_back_end.Services
{
    public class AdminService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<GetAllDesiresResponse?> GetAllDesires()
        {
            ICollection<UserDesire>? userDesires = await dataContext.UserDesires.Include(ud => ud.User).Include(ud => ud.Book).ToListAsync();
            if (userDesires == null)
                return null;

            GetAllDesiresResponse response = new(userDesires);
            return response;
        }

        public async Task<DesireResponse> AcceptBorrow(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.Id == request.DesireId);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user desire found for the Id in request." };
            bool isBookAvailable = await dataContext.Books.AnyAsync(b => b.Id == userDesire.BookId && b.Count > 0);
            if (!isBookAvailable)
                return new() { Succeeded = false, Message = "Out of stock or invalid bookId." };

            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            book!.Count -= 1;
            dataContext.UserDesires.Remove(userDesire);
            DateTime returnDueDate = DateTime.UtcNow.Date.AddDays(8).AddTicks(-1);
            UserBookBorrow ubb = new()
            {
                UserId = userDesire.UserId,
                BookId = userDesire.BookId,
                BorrowDate = DateTime.UtcNow,
                ReturnDueDate = returnDueDate,
                BorrowSituationId = 1
            };
            dataContext.Books.Update(book);
            dataContext.UserBookBorrows.Add(ubb);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The borrow request has been approved!" };
        }

        public async Task<DesireResponse> AcceptReturn(DesireRequest request)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.Id == request.DesireId);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No user desire found for the Id in request." };
            bool isBookAvailable = await dataContext.Books.AnyAsync(b => b.Id == userDesire.BookId);
            if (!isBookAvailable)
                return new() { Succeeded = false, Message = "Invalid bookId." };

            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == userDesire.BookId);
            book!.Count += 1;
            dataContext.UserDesires.Remove(userDesire);
            UserBookBorrow? ubb = await dataContext.UserBookBorrows.FirstOrDefaultAsync(ubb => ubb.UserId == userDesire.UserId && ubb.BookId == userDesire.BookId);
            ubb!.ReturnDate = DateTime.Now;
            ubb!.BorrowSituationId = 2;
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
    }
}
