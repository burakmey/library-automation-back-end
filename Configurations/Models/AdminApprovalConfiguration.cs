namespace library_automation_back_end.Configurations.Models
{
    public class AdminApprovalConfiguration : IEntityTypeConfiguration<AdminApproval>
    {
        public void Configure(EntityTypeBuilder<AdminApproval> builder)
        {
            builder.HasAlternateKey(aa => new { aa.UserId, aa.BookId });

            builder.HasOne(aa => aa.User).WithMany().HasForeignKey(aa => aa.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(aa => aa.Book).WithMany().HasForeignKey(aa => aa.BookId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(aa => aa.ApprovalSituation).WithMany().HasForeignKey(aa => aa.ApprovalSituationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
