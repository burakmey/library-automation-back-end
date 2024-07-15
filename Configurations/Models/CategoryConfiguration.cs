namespace library_automation_back_end.Configurations.Models
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Science Fiction" },
                    new() { Id = 2, Name = "Historical Fiction" },
                    new() { Id = 3, Name = "Literary Fiction" },
                    new() { Id = 4, Name = "Fantasy" },
                    new() { Id = 5, Name = "Thriller" },
                    new() { Id = 6, Name = "Suspense" },
                    new() { Id = 7, Name = "Romantic" },
                    new() { Id = 8, Name = "Adventure" },
                    new() { Id = 9, Name = "Comic Book" },
                    new() { Id = 10, Name = "Mystery" },
                    new() { Id = 11, Name = "Biography" },
                    new() { Id = 12, Name = "Classics" },
                    new() { Id = 13, Name = "History" },
                    new() { Id = 14, Name = "Detective Fiction" },
                    new() { Id = 15, Name = "Memoir" },
                    new() { Id = 16, Name = "Encyclopedia" }
                ]);
        }
    }
}
