namespace library_automation_back_end.Configurations.Models
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Türkiye" },
                    new() { Id = 2, Name = "Azerbaycan" },
                    new() { Id = 3, Name = "Rusya" },
                    new() { Id = 4, Name = "İngiltere" },
                    new() { Id = 5, Name = "Fransa" },
                    new() { Id = 6, Name = "İtalya" },
                    new() { Id = 7, Name = "İspanya" },
                    new() { Id = 8, Name = "Belçika" },
                    new() { Id = 9, Name = "Yunanistan" },
                    new() { Id = 10, Name = "Portekiz" },
                    new() { Id = 11, Name = "Amerika" },
                    new() { Id = 12, Name = "Brezilya" },
                    new() { Id = 13, Name = "Arjantin" },
                    new() { Id = 14, Name = "Kanada" },
                    new() { Id = 15, Name = "Japonya" },
                    new() { Id = 16, Name = "Almanya" },
                    new() { Id = 17, Name = "Çin" }
                ]);
        }
    }
}
