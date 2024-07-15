namespace library_automation_back_end.Configurations.Models
{
    public class ReserveSituationConfiguration : IEntityTypeConfiguration<ReserveSituation>
    {
        public void Configure(EntityTypeBuilder<ReserveSituation> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Situation = ReserveSituationEnum.Waiting.ToString() },
                    new() { Id = 2, Situation = ReserveSituationEnum.Borrowed.ToString() },
                    new() { Id = 3, Situation = ReserveSituationEnum.TimeOut.ToString() }
                ]);
        }
    }
}
