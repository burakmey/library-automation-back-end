namespace library_automation_back_end.Services
{
    public class UserService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<BookResponse> SendBorrowRequest(BookRequest request, int userId)
        {
            bool hasBorrowedOrReserved = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId) ||
                await dataContext.UserBookReserves.AnyAsync(ubr => ubr.UserId == userId && ubr.BookId == request.BookId);
            if (hasBorrowedOrReserved)
                return new() { Succeeded = false, Message = "You already have the selected book or have reserved it." };
            bool hasUserDesire = await dataContext.UserDesires.AnyAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (hasUserDesire)
                return new() { Succeeded = false, Message = "Borrow request has already been in process with admin." };
            bool isBookExists = await dataContext.Books.AnyAsync(book => book.Id == request.BookId && book.Count > 0);
            if (!isBookExists)
                return new() { Succeeded = false, Message = "Invalid selected book or out of stock." };

            UserDesire ud = new()
            {
                UserId = userId,
                BookId = request.BookId,
                DesireSituationId = 1
            };
            dataContext.UserDesires.Add(ud);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Borrow request has been sent to admin for approval!" };
        }

        public async Task<BookResponse> DeleteBorrowRequest(BookRequest request, int userId)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No request found for the selected book and user." };

            dataContext.UserDesires.Remove(userDesire);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The borrow request has been delete!" };
        }

        public async Task<BookResponse> SendReturnRequest(BookRequest request, int userId)
        {
            bool hasBorrowed = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId);
            if (!hasBorrowed)
                return new() { Succeeded = false, Message = "User does not have the selected book." };
            bool hasUserDesire = await dataContext.UserDesires.AnyAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (hasUserDesire)
                return new() { Succeeded = false, Message = "Return request has already been in process with admin." };

            UserDesire ud = new()
            {
                UserId = userId,
                BookId = request.BookId,
                DesireSituationId = 2
            };
            dataContext.UserDesires.Add(ud);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The return request has been sent to admin for approval!" };
        }

        public async Task<BookResponse> DeleteReturnRequest(BookRequest request, int userId)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No request found for the selected book and user." };

            dataContext.UserDesires.Remove(userDesire);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The return request has been delete!" };
        }

        public async Task<BookResponse> ReserveBook(BookRequest request, int userId)
        {
            bool hasBorrowedOrReserved = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId) ||
                await dataContext.UserBookReserves.AnyAsync(ubr => ubr.UserId == userId && ubr.BookId == request.BookId);
            if (hasBorrowedOrReserved)
                return new() { Succeeded = false, Message = "You already have the selected book or have reserved it." };
            bool hasUserDesire = await dataContext.UserDesires.AnyAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (hasUserDesire)
                return new() { Succeeded = false, Message = "Selected book has already been in process with admin." };
            bool isBookExists = await dataContext.Books.AnyAsync(book => book.Id == request.BookId && book.Count > 0);
            if (!isBookExists)
                return new() { Succeeded = false, Message = "Invalid selected book or out of stock." };

            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == request.BookId);
            book!.Count -= 1;
            DateTime reserveDueDate = DateTime.UtcNow.Date.AddDays(8).AddTicks(-1);
            UserBookReserve ubr = new()
            {
                UserId = userId,
                BookId = request.BookId,
                ReserveDate = DateTime.UtcNow,
                ReserveDueDate = reserveDueDate,
                ReserveSituationId = 1
            };
            dataContext.Books.Update(book);
            dataContext.UserBookReserves.Add(ubr);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The reservation has been completed!" };
        }

        public async Task<BookResponse> SendReservedBorrowRequest(BookRequest request, int userId)
        {

            bool isBookReserved = await dataContext.UserBookReserves.AnyAsync(ubs => ubs.BookId == request.BookId && ubs.UserId == userId);
            if (!isBookReserved)
                return new() { Succeeded = false, Message = "There is no reservation for selected book." };

            UserDesire ud = new()
            {
                UserId = userId,
                BookId = request.BookId,
                DesireSituationId = 1
            };
            dataContext.UserDesires.Add(ud);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Borrow request has been sent to admin for approval!" };
        }
    }
}
