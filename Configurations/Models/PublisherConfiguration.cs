namespace library_automation_back_end.Configurations.Models
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Name = "İthaki", CountryId = 1 },
                    new() { Id = 2, Name = "Panama", CountryId = 1 },
                    new() { Id = 3, Name = "Ötüken", CountryId = 1 },
                    new() { Id = 4, Name = "Profile Books", CountryId = 4 },
                    new() { Id = 5, Name = "Yapı Kredi", CountryId = 1 },
                    new() { Id = 6, Name = "Metis", CountryId = 1 },
                    new() { Id = 7, Name = "Actes Sud", CountryId = 5 },
                    new() { Id = 8, Name = "İş Bankası Kültür", CountryId = 1 }
                ]);
        }
    }
}
