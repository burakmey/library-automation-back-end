namespace library_automation_back_end.Features.AuthFeatures
{
    public class UserBookBorrowResponse(UserBookBorrow userBookBorrow)
    {
        public DateTime BorrowDate { get; set; } = userBookBorrow.BorrowDate;
        public DateTime ReturnDueDate { get; set; } = userBookBorrow.ReturnDueDate;
        public string BorrowSituation { get; set; } = userBookBorrow.BorrowSituation!.Situation;
        public string BookName { get; set; } = userBookBorrow.Book!.Name;
    }
}
