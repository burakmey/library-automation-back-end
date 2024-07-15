namespace library_automation_back_end.Configurations.Models
{
    public class DesireSituationConfiguration : IEntityTypeConfiguration<DesireSituation>
    {
        public void Configure(EntityTypeBuilder<DesireSituation> builder)
        {
            builder.HasData(
            [
                new() { Id = 1, Situation = DesireSituationEnum.Borrow.ToString() },
                new() { Id = 2, Situation = DesireSituationEnum.ReserveBorrow.ToString() },
                new() { Id = 3, Situation = DesireSituationEnum.Return.ToString() }
            ]);
        }
    }
}
