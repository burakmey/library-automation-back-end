namespace library_automation_back_end.Configurations.Models
{
    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new { bc.BookId, bc.CategoryId });
            builder.HasOne(bc => bc.Book).WithMany(b => b.BookCategories).HasForeignKey(bc => bc.BookId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(bc => bc.Category).WithMany(c => c.BookCategories).HasForeignKey(bc => bc.CategoryId).OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(bc => bc.Category).AutoInclude();

            builder.HasData(
                [
                    new() { BookId = 1, CategoryId = 1 },
                    new() { BookId = 1, CategoryId = 12 },
                    new() { BookId = 2, CategoryId = 1 },
                    new() { BookId = 3, CategoryId = 1 },
                    new() { BookId = 4, CategoryId = 2 },
                    new() { BookId = 4, CategoryId = 11 },
                    new() { BookId = 5, CategoryId = 2 },
                    new() { BookId = 6, CategoryId = 2 },
                    new() { BookId = 7, CategoryId = 4 },
                    new() { BookId = 7, CategoryId = 8 },
                    new() { BookId = 7, CategoryId = 10 },
                    new() { BookId = 8, CategoryId = 4 },
                    new() { BookId = 8, CategoryId = 8 },
                    new() { BookId = 9, CategoryId = 4 },
                    new() { BookId = 9, CategoryId = 10 },
                    new() { BookId = 10, CategoryId = 3 },
                    new() { BookId = 11, CategoryId = 3 },
                    new() { BookId = 12, CategoryId = 3 }
                ]);
        }
    }
}
