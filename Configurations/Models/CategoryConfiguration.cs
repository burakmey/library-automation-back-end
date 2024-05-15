namespace library_automation_back_end.Configurations.Models
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "Bilim Kurgu" },
                    new() { Id = 2, Name = "Tarihsel Kurgu" },
                    new() { Id = 3, Name = "Edebi Kurgu" },
                    new() { Id = 4, Name = "Fantastik" },
                    new() { Id = 5, Name = "Thriller" },
                    new() { Id = 6, Name = "Gerilim" },
                    new() { Id = 7, Name = "Romantik" },
                    new() { Id = 8, Name = "Macera" },
                    new() { Id = 9, Name = "Çizgi Roman" },
                    new() { Id = 10, Name = "Gizem" },
                    new() { Id = 11, Name = "Biyografi" },
                    new() { Id = 12, Name = "Klasikler" },
                    new() { Id = 13, Name = "Tarih" },
                    new() { Id = 14, Name = "Polisiye Kurgu" },
                    new() { Id = 15, Name = "Anı" },
                    new() { Id = 16, Name = "Ansiklopedi" }
                ]);
        }
    }
}
