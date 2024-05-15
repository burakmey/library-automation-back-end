namespace library_automation_back_end.Configurations.Models
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Türkçe" },
                    new() { Id = 2, Name = "Rusça" },
                    new() { Id = 3, Name = "İngilizce" },
                    new() { Id = 4, Name = "Fransızca" },
                    new() { Id = 5, Name = "İtalyanca" },
                    new() { Id = 6, Name = "İspanyolca" },
                    new() { Id = 7, Name = "Yunanca" },
                    new() { Id = 8, Name = "Portekizce" },
                    new() { Id = 9, Name = "Japonca" },
                    new() { Id = 10, Name = "Almanca" },
                    new() { Id = 11, Name = "Flemenkçe" },
                    new() { Id = 12, Name = "Çince" }
                ]);
        }
    }
}
