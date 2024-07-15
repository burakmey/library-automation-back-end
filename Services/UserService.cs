using library_automation_back_end.Features.FeaturesUser.Requests;

namespace library_automation_back_end.Services
{
    public class UserService(DataContext dataContext)
    {
        readonly DataContext dataContext = dataContext;

        public async Task<MessageResponse> SendBorrowRequest(BookRequest request, int userId)
        {
            bool isBorrowed = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId && ubb.BorrowSituationId == (int)BorrowSituationEnum.Borrowed);
            if (isBorrowed)
                return new() { Succeeded = false, Message = "You already have the selected book!" };
            bool isReserved = await dataContext.UserBookReserves.AnyAsync(ubr => ubr.UserId == userId && ubr.BookId == request.BookId && ubr.ReserveSituationId == (int)ReserveSituationEnum.Waiting);
            if (isReserved)
                return new() { Succeeded = false, Message = "You already have a reservation for the selected book!" };
            bool hasUserDesire = await dataContext.UserDesires.AnyAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (hasUserDesire)
                return new() { Succeeded = false, Message = "The selected book for borrowing has already been in process with admin!" };
            bool isBookExists = await dataContext.Books.AnyAsync(book => book.Id == request.BookId && book.Count > 0);
            if (!isBookExists)
                return new() { Succeeded = false, Message = "Invalid selected book or out of stock!" };

            UserDesire ud = new()
            {
                UserId = userId,
                BookId = request.BookId,
                DesireSituationId = (int)DesireSituationEnum.Borrow
            };
            dataContext.UserDesires.Add(ud);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Borrow request has been sent to admin for approval." };
        }

        public async Task<MessageResponse> SendReservedBorrowRequest(BookRequest request, int userId)
        {
            bool isBookReserved = await dataContext.UserBookReserves.AnyAsync(ubs => ubs.BookId == request.BookId && ubs.UserId == userId && ubs.ReserveSituationId == (int)ReserveSituationEnum.Waiting);
            if (!isBookReserved)
                return new() { Succeeded = false, Message = "You do not have the reservation for selected book!" };

            UserDesire ud = new()
            {
                UserId = userId,
                BookId = request.BookId,
                DesireSituationId = (int)DesireSituationEnum.ReserveBorrow
            };
            dataContext.UserDesires.Add(ud);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Borrow request has been sent to admin for approval." };
        }

        public async Task<MessageResponse> SendReturnRequest(BookRequest request, int userId)
        {
            bool hasBorrowed = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId && ubb.BorrowSituationId == (int)BorrowSituationEnum.Borrowed);
            if (!hasBorrowed)
                return new() { Succeeded = false, Message = "You do not have the selected book!" };
            bool hasUserDesire = await dataContext.UserDesires.AnyAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (hasUserDesire)
                return new() { Succeeded = false, Message = "The selected book for returning has already been in process with admin!" };

            UserDesire ud = new()
            {
                UserId = userId,
                BookId = request.BookId,
                DesireSituationId = (int)DesireSituationEnum.Return
            };
            dataContext.UserDesires.Add(ud);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "Return request has been sent to admin for approval." };
        }

        public async Task<MessageResponse> ReserveBook(BookRequest request, int userId)
        {
            bool isBorrowed = await dataContext.UserBookBorrows.AnyAsync(ubb => ubb.UserId == userId && ubb.BookId == request.BookId && ubb.BorrowSituationId == (int)BorrowSituationEnum.Borrowed);
            if (isBorrowed)
                return new() { Succeeded = false, Message = "You already have the selected book!" };
            bool isReserved = await dataContext.UserBookReserves.AnyAsync(ubr => ubr.UserId == userId && ubr.BookId == request.BookId && ubr.ReserveSituationId == (int)ReserveSituationEnum.Waiting);
            if (isReserved)
                return new() { Succeeded = false, Message = "You already have reservation for the selected book!" };
            bool hasUserDesire = await dataContext.UserDesires.AnyAsync(ud => ud.UserId == userId && ud.BookId == request.BookId);
            if (hasUserDesire)
                return new() { Succeeded = false, Message = "The selected book for reservation has already been in process with admin!" };
            bool isBookExists = await dataContext.Books.AnyAsync(book => book.Id == request.BookId && book.Count > 0);
            if (!isBookExists)
                return new() { Succeeded = false, Message = "Invalid selected book or out of stock!" };
            
            Book? book = await dataContext.Books.FirstOrDefaultAsync(b => b.Id == request.BookId);
            if (book == null)
                return new() { Succeeded = false, Message = "No book found for reservation!" };

            book.Count -= 1;
            DateTime reserveDueDate = DateTime.UtcNow.Date.AddDays(8).AddTicks(-1);
            UserBookReserve ubr = new()
            {
                UserId = userId,
                BookId = request.BookId,
                ReserveDate = DateTime.UtcNow,
                ReserveDueDate = reserveDueDate,
                ReserveSituationId = (int)ReserveSituationEnum.Waiting
            };
            dataContext.Books.Update(book);
            dataContext.UserBookReserves.Add(ubr);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The reservation has been completed." };
        }

        public async Task<MessageResponse> DeleteRequest(DesireRequest request, int userId)
        {
            UserDesire? userDesire = await dataContext.UserDesires.FirstOrDefaultAsync(ud => ud.UserId == userId && ud.Id == request.DesireId);
            if (userDesire == null)
                return new() { Succeeded = false, Message = "No desire found for the selected book!" };

            dataContext.UserDesires.Remove(userDesire);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "The desire has been delete." };
        }
    }
}
