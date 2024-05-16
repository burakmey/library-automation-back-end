namespace library_automation_back_end.Configurations.Models
{
    public class UserBookReserveConfiguration : IEntityTypeConfiguration<UserBookReserve>
    {
        public void Configure(EntityTypeBuilder<UserBookReserve> builder)
        {
            builder.HasKey(ubr => new { ubr.UserId, ubr.BookId });
            builder.HasOne(ubr => ubr.User).WithMany(u => u.UserBookReserves).HasForeignKey(ubr => ubr.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(ubr => ubr.Book).WithMany(b => b.UserBookReserves).HasForeignKey(ubr => ubr.BookId).OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(ubr => ubr.ReserveSituation).AutoInclude();
        }
    }
}
