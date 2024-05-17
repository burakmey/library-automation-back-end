namespace library_automation_back_end.Configurations.Models
{
    public class DesireSituationConfiguration : IEntityTypeConfiguration<DesireSituation>
    {
        public void Configure(EntityTypeBuilder<DesireSituation> builder)
        {
            builder.HasData(
            [
                new() { Id = 1, Situation = "Ödünç" },
                new() { Id = 2, Situation = "İade" }
            ]);
        }
    }
}
