namespace library_automation_back_end.Configurations.Models
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Turkiye" },
                    new() { Id = 2, Name = "Azerbaijan" },
                    new() { Id = 3, Name = "Russia" },
                    new() { Id = 4, Name = "United Kingdom" },
                    new() { Id = 5, Name = "France" },
                    new() { Id = 6, Name = "Italy" },
                    new() { Id = 7, Name = "Spain" },
                    new() { Id = 8, Name = "Belgium" },
                    new() { Id = 9, Name = "Greece" },
                    new() { Id = 10, Name = "Portugal" },
                    new() { Id = 11, Name = "United States" },
                    new() { Id = 12, Name = "Brazil" },
                    new() { Id = 13, Name = "Argentina" },
                    new() { Id = 14, Name = "Canada" },
                    new() { Id = 15, Name = "Japan" },
                    new() { Id = 16, Name = "Germany" },
                    new() { Id = 17, Name = "China" }
                ]);
        }
    }
}
