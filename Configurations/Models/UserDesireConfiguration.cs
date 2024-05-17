namespace library_automation_back_end.Configurations.Models
{
    public class UserDesireConfiguration : IEntityTypeConfiguration<UserDesire>
    {
        public void Configure(EntityTypeBuilder<UserDesire> builder)
        {
            builder.HasAlternateKey(ud => new { ud.UserId, ud.BookId });

            builder.HasOne(ud => ud.User).WithMany().HasForeignKey(ud => ud.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(ud => ud.Book).WithMany().HasForeignKey(ud => ud.BookId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(ud => ud.DesireSituation).WithMany().HasForeignKey(ud => ud.DesireSituationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
