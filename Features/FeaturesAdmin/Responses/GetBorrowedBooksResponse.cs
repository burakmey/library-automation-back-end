namespace library_automation_back_end.Features.FeaturesAdmin.Responses
{
    public class GetBorrowedBooksResponse(ICollection<UserBookBorrow> userBookBorrows)
    {
        public ICollection<UsersBookBorrowResponseModel> UserBookBorrows { get; set; } = userBookBorrows.Select(ubb => new UsersBookBorrowResponseModel(ubb)).ToList();
    }
    public class UsersBookBorrowResponseModel(UserBookBorrow userBookBorrow)
    {
        public string UserName { get; set; } = userBookBorrow.User!.Name + " " + userBookBorrow.User.Surname;
        public string BookName { get; set; } = userBookBorrow.Book!.Name;
        public string Situation { get; set; } = userBookBorrow.BorrowSituation!.Situation;
    }
}