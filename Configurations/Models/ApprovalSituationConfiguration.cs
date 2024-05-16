namespace library_automation_back_end.Configurations.Models
{
    public class ApprovalSituationConfiguration : IEntityTypeConfiguration<ApprovalSituation>
    {
        public void Configure(EntityTypeBuilder<ApprovalSituation> builder)
        {
            builder.HasData(
            [
                new() { Id = 1, Situation = "Ödünç" },
                new() { Id = 2, Situation = "İade" }
            ]);
        }
    }
}
