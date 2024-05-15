namespace library_automation_back_end.Configurations.Models
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Aldous Huxley", CountryId = 4 },
                    new() { Id = 2, Name = "Cixin Liu", CountryId = 17 },
                    new() { Id = 3, Name = "Andy Weir", CountryId = 11 },
                    new() { Id = 4, Name = "James Reston", CountryId = 11 },
                    new() { Id = 5, Name = "Tağrık Buğra", CountryId = 1 },
                    new() { Id = 6, Name = "Robert Groves", CountryId = 4 },
                    new() { Id = 7, Name = "Joanne Kathleen Rowling", CountryId = 4 },
                    new() { Id = 8, Name = "John Ronald Reuel Tolkien", CountryId = 4 },
                    new() { Id = 9, Name = "Margaret Weis", CountryId = 11 },
                    new() { Id = 10, Name = "Tracy Hickman", CountryId = 11 },
                    new() { Id = 11, Name = "Orhan Pamuk", CountryId = 1 },
                    new() { Id = 12, Name = "Victor Hugo", CountryId = 5 }
                ]);
        }
    }
}
