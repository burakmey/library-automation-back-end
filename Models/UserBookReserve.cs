namespace library_automation_back_end.Models
{
    public class UserBookReserve
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public required DateTime ReserveDate { get; set; }
        public DateTime? BorrowDate { get; set; }
        public required DateTime ReserveDueDate { get; set; }
        [ForeignKey(nameof(ReserveSituation))] public required int ReserveSituationId { get; set; }
        public ReserveSituation? ReserveSituation { get; set; }
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
