namespace library_automation_back_end.Models
{
    public class AdminApproval
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))] public required int UserId { get; set; }
        [ForeignKey(nameof(Book))] public required int BookId { get; set; }
        [ForeignKey(nameof(ApprovalSituation))] public required int ApprovalSituationId { get; set; }
        public ApprovalSituation? ApprovalSituation { get; set; }
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
