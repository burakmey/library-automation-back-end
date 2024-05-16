namespace library_automation_back_end.Configurations.Models
{
    public class UserBookBorrowConfiguration : IEntityTypeConfiguration<UserBookBorrow>
    {
        public void Configure(EntityTypeBuilder<UserBookBorrow> builder)
        {
            builder.HasKey(ubb => new { ubb.UserId, ubb.BookId });
            builder.HasOne(ubb => ubb.User).WithMany(u => u.UserBookBorrows).HasForeignKey(ubb => ubb.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(ubb => ubb.Book).WithMany(b => b.UserBookBorrows).HasForeignKey(ubb => ubb.BookId).OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(ubb => ubb.BorrowSituation).AutoInclude();
        }
    }
}
