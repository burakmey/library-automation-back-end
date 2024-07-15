namespace library_automation_back_end.Configurations.Models
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Brave New World", Year = 1932, Count = 1, PageCount = 272, LanguageId = 3, PublisherId = 1 },
                    new() { Id = 2, Name = "The Three-Body Problem", Year = 2008, Count = 2, PageCount = 416, LanguageId = 1, PublisherId = 1 },
                    new() { Id = 3, Name = "Project Hail Mary", Year = 2021, Count = 3, PageCount = 536, LanguageId = 3, PublisherId = 1 },
                    new() { Id = 4, Name = "God's Warriors", Year = 2001, Count = 1, PageCount = 439, LanguageId = 1, PublisherId = 2 },
                    new() { Id = 5, Name = "Osmancık", Year = 1982, Count = 2, PageCount = 376, LanguageId = 1, PublisherId = 3 },
                    new() { Id = 6, Name = "I, Claudius", Year = 1934, Count = 3, PageCount = 528, LanguageId = 3, PublisherId = 4 },
                    new() { Id = 7, Name = "Harry Potter", Year = 1997, Count = 7, PageCount = 276, LanguageId = 1, PublisherId = 5 },
                    new() { Id = 8, Name = "The Lord of the Rings", Year = 1954, Count = 3, PageCount = 416, LanguageId = 1, PublisherId = 6 },
                    new() { Id = 9, Name = "Dragons of Autumn Twilight", Year = 1984, Count = 3, PageCount = 412, LanguageId = 4, PublisherId = 7 },
                    new() { Id = 10, Name = "The Museum of Innocence", Year = 2008, Count = 1, PageCount = 465, LanguageId = 3, PublisherId = 5 },
                    new() { Id = 11, Name = "The Red-Haired Woman", Year = 2016, Count = 1, PageCount = 220, LanguageId = 1, PublisherId = 5 },
                    new() { Id = 12, Name = "The Hunchback of Notre-Dame", Year = 1831, Count = 1, PageCount = 560, LanguageId = 4, PublisherId = 8 }
                ]);
        }
    }
}
