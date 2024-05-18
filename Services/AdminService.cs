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
    }
}
