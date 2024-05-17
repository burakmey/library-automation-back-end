namespace library_automation_back_end.Models
{
    public class UserDesire
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))] public required int UserId { get; set; }
        [ForeignKey(nameof(Book))] public required int BookId { get; set; }
        [ForeignKey(nameof(DesireSituation))] public required int DesireSituationId { get; set; }
        public DesireSituation? DesireSituation { get; set; }
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
