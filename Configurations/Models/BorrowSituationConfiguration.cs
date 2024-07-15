namespace library_automation_back_end.Configurations.Models
{
    public class BorrowSituationConfiguration : IEntityTypeConfiguration<BorrowSituation>
    {
        public void Configure(EntityTypeBuilder<BorrowSituation> builder)
        {
            builder.HasData(
                [
                    new() { Id = 1, Situation = BorrowSituationEnum.Borrowed.ToString() },
                    new() { Id = 2, Situation = BorrowSituationEnum.Returned.ToString() },
                    new() { Id = 3, Situation = BorrowSituationEnum.TimeOut.ToString() }
                ]);
        }
    }
}
