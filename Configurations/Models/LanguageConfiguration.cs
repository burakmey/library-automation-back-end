namespace library_automation_back_end.Configurations.Models
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Turkish" },
                    new() { Id = 2, Name = "Russian" },
                    new() { Id = 3, Name = "English" },
                    new() { Id = 4, Name = "French" },
                    new() { Id = 5, Name = "Italian" },
                    new() { Id = 6, Name = "Spanish" },
                    new() { Id = 7, Name = "Greek" },
                    new() { Id = 8, Name = "Portuguese" },
                    new() { Id = 9, Name = "Japanese" },
                    new() { Id = 10, Name = "German" },
                    new() { Id = 11, Name = "Dutch" },
                    new() { Id = 12, Name = "Chinese" }
                ]);
        }
    }
}
