namespace library_automation_back_end.Features.AdminFeatures
{
    public class GetBorrowedBooksResponse(ICollection<UserBookBorrow> userBookBorrows)
    {
        public ICollection<UsersBookBorrowResponse> UserBookBorrows { get; set; } = userBookBorrows.Select(ubb => new UsersBookBorrowResponse(ubb)).ToList();
    }
}
