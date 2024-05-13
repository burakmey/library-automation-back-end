namespace library_automation_back_end.Models
{
    public class UserBookBorrow
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public required DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public required DateTime ReturnDueDate { get; set; }
        [ForeignKey(nameof(BorrowSituation))] public required int BorrowSituationId { get; set; }
        public BorrowSituation? BorrowSituation { get; set; }
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
