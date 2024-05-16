namespace library_automation_back_end.Services
{
    public class BookService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<BookResponse> SendBorrowRequest(BookRequest request, int userId)
        {
            bool isBookExists = await dataContext.Books.AnyAsync(book => book.Id == request.BookId && book.Count > 0);
            if (!isBookExists)
                return new() { Succeeded = false, Message = "Invalid selected book or out of stock." };
            bool hasAdminApproval = await dataContext.AdminApprovals.AnyAsync(aa => aa.UserId == userId && aa.BookId == request.BookId);
            if (hasAdminApproval)
                return new() { Succeeded = false, Message = "Borrow request has already been in process with admin." };
            bool hasBorrowedOrReserved = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId) ||
                                            await dataContext.UserBookReserves.AnyAsync(ubr => ubr.UserId == userId && ubr.BookId == request.BookId);
            if (hasBorrowedOrReserved)
                return new() { Succeeded = false, Message = "You already have the selected book or have reserved it." };
            AdminApproval aa = new()
            {
                UserId = userId,
                BookId = request.BookId,
                ApprovalSituationId = 1
            };
            dataContext.AdminApprovals.Add(aa);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Borrow request has been sent to admin for approval!" };
        }

        public async Task<BookResponse> DeleteBorrowRequest(BookRequest request, int userId)
        {
            AdminApproval? aa = await dataContext.AdminApprovals.FirstOrDefaultAsync(aa => aa.UserId == userId && aa.BookId == request.BookId);
            if (aa == null)
                return new() { Succeeded = false, Message = "No request found for the selected book and user." };
            dataContext.AdminApprovals.Remove(aa);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The borrow request has been delete!" };
        }

        public async Task<BookResponse> SendReturnRequest(BookRequest request, int userId)
        {
            bool hasBorrowed = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId);
            if (!hasBorrowed)
                return new() { Succeeded = false, Message = "You does not have the selected book." };
            bool hasAdminApproval = await dataContext.AdminApprovals.AnyAsync(aa => aa.UserId == userId && aa.BookId == request.BookId);
            if (hasAdminApproval)
                return new() { Succeeded = false, Message = "Return request has already been in process with admin." };
            AdminApproval aa = new()
            {
                UserId = userId,
                BookId = request.BookId,
                ApprovalSituationId = 2
            };
            dataContext.AdminApprovals.Add(aa);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The return request has been sent to admin for approval!" };
        }

        public async Task<BookResponse> DeleteReturnRequest(BookRequest request, int userId)
        {
            AdminApproval? aa = await dataContext.AdminApprovals.FirstOrDefaultAsync(aa => aa.UserId == userId && aa.BookId == request.BookId);
            if (aa == null)
                return new() { Succeeded = false, Message = "No request found for the selected book and user." };
            dataContext.AdminApprovals.Remove(aa);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The return request has been delete!" };
        }

        public async Task<BookResponse> ReserveBook(BookRequest request, int userId)
        {
            bool isBookExists = await dataContext.Books.AnyAsync(book => book.Id == request.BookId && book.Count > 0);
            if (!isBookExists)
                return new() { Succeeded = false, Message = "Invalid selected book or out of stock." };
            bool hasAdminApproval = await dataContext.AdminApprovals.AnyAsync(aa => aa.UserId == userId && aa.BookId == request.BookId);
            if (hasAdminApproval)
                return new() { Succeeded = false, Message = "Selected book has already been in process with admin." };
            bool hasBorrowedOrReserved = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId) ||
                                            await dataContext.UserBookReserves.AnyAsync(ubr => ubr.UserId == userId && ubr.BookId == request.BookId);
            if (hasBorrowedOrReserved)
                return new() { Succeeded = false, Message = "You already have the selected book or have reserved it." };
            DateTime reserveDueDate = DateTime.UtcNow.Date.AddDays(8).AddTicks(-1);
            UserBookReserve ubr = new()
            {
                UserId = userId,
                BookId = request.BookId,
                ReserveDate = DateTime.UtcNow,
                ReserveDueDate = reserveDueDate,
                ReserveSituationId = 1
            };
            dataContext.UserBookReserves.Add(ubr);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The reservation has been completed!" };
        }
    }
}
