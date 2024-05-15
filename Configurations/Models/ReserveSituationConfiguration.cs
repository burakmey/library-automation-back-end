namespace library_automation_back_end.Configurations.Models
{
    public class ReserveSituationConfiguration : IEntityTypeConfiguration<ReserveSituation>
    {
        public void Configure(EntityTypeBuilder<ReserveSituation> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Situation = "Beklemede" },
                    new() { Id = 2, Situation = "Ödünç Alınmış" },
                    new() { Id = 3, Situation = "Süresi Geçmiş" }
                ]);
        }
    }
}
