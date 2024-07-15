using System.Security.Cryptography;

namespace library_automation_back_end.Configurations.Models
{
    public class UserConfiguration(IConfiguration configuration) : IEntityTypeConfiguration<User>
    {
        readonly IConfiguration configuration = configuration;

        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Get email and create password for admin.
            string adminEmail = configuration["ADMIN_EMAIL"] ?? throw new Exception("ADMIN_EMAIL not found!");
            string adminPassword = configuration["ADMIN_PASSWORD"] ?? throw new Exception("ADMIN_PASSWORD not found!");
            CreatePasswordHash(adminPassword, out byte[] passwordHash, out byte[] passwordSalt);

            builder.HasData(
                [
                    new() { Id = 1, Email = adminEmail, Name = "AdminName", Surname = "AdminSurname", PasswordHash = passwordHash, PasswordSalt = passwordSalt, RegisteredAt = DateTime.UtcNow, CountryId = 11, RoleId = 1 }
                ]);
        }

        static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

}
