namespace library_automation_back_end.Features.FeaturesAuth.Responses
{
    public class BorrowedResponseModel(UserBookBorrow userBookBorrow)
    {
        public DateTime BorrowDate { get; set; } = userBookBorrow.BorrowDate;
        public DateTime? ReturnDate { get; set; } = userBookBorrow.ReturnDate;
        public DateTime ReturnDueDate { get; set; } = userBookBorrow.ReturnDueDate;
        public string BorrowSituation { get; set; } = userBookBorrow.BorrowSituation!.Situation;
        public string BookName { get; set; } = userBookBorrow.Book!.Name;
        public int BookId { get; set; } = userBookBorrow.Book!.Id;
    }
}
