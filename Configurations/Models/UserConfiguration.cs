namespace library_automation_back_end.Configurations.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                [
                    //new() { Id = 1, Email = "admin@hotmail.com", Name = "AdminName", Surname = "AdminSurname", PasswordHash = [], PasswordSalt = [], RegisteredAt = DateTime.UtcNow, CountryId = 11, RoleId = 1 }
                ]);
        }
    }
}
