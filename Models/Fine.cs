namespace library_automation_back_end.Models
{
    public class Fine
    {
        public int Id { get; set; }
        public required int Fee { get; set; }
        public required DateTime PaymentDueDate { get; set; }
        [ForeignKey(nameof(User))] public required int UserId { get; set; }
        public User? User { get; set; }
    }
}
