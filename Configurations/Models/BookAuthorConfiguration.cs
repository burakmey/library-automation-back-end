namespace library_automation_back_end.Configurations.Models
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(ba => new { ba.BookId, ba.AuthorId });
            builder.HasOne(ba => ba.Book).WithMany(b => b.BookAuthors).HasForeignKey(ba => ba.BookId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(ba => ba.Author).WithMany(a => a.BookAuthors).HasForeignKey(ba => ba.AuthorId).OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(ba => ba.Author).AutoInclude();

            builder.HasData(
            [
                new() { BookId = 1, AuthorId = 1 },
                new() { BookId = 2, AuthorId = 2 },
                new() { BookId = 3, AuthorId = 3 },
                new() { BookId = 4, AuthorId = 4 },
                new() { BookId = 5, AuthorId = 5 },
                new() { BookId = 6, AuthorId = 6 },
                new() { BookId = 7, AuthorId = 7 },
                new() { BookId = 8, AuthorId = 8 },
                new() { BookId = 9, AuthorId = 9 },
                new() { BookId = 9, AuthorId = 10 },
                new() { BookId = 10, AuthorId = 11 },
                new() { BookId = 11, AuthorId = 11 },
                new() { BookId = 12, AuthorId = 12 }
            ]);
        }
    }
}
