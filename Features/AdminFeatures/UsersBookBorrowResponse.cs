namespace library_automation_back_end.Features.AdminFeatures
{
    public class UsersBookBorrowResponse(UserBookBorrow userBookBorrow)
    {
        public string UserName { get; set; } = userBookBorrow.User!.Name + " " + userBookBorrow.User.Surname;
        public string BookName { get; set; } = userBookBorrow.Book!.Name;
        public string Situation { get; set; } = userBookBorrow.BorrowSituation!.Situation;
    }
}
