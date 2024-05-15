namespace library_automation_back_end.Configurations.Models
{
    public class BorrowSituationConfiguration : IEntityTypeConfiguration<BorrowSituation>
    {
        public void Configure(EntityTypeBuilder<BorrowSituation> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Situation = "Ödünç Alınmış" },
                    new() { Id = 2, Situation = "İade Edilmiş" },
                    new() { Id = 3, Situation = "Süresi Geçmiş" }
                ]);
        }
    }
}
