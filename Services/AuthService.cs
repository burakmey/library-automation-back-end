using System.Security.Cryptography;

namespace library_automation_back_end.Services
{
    public class AuthService(IConfiguration configuration, DataContext dataContext)
    {
        readonly IConfiguration configuration = configuration;
        readonly DataContext dataContext = dataContext;

        public async Task<RegisterResponse> CreateUser(RegisterRequest request)
        {
            if (dataContext.Users.Any(user => user.Email == request.Email))
                return new() { Succeeded = false, Message = "Email already exists." };

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new()
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CountryId = request.CountryId,
                RegisteredAt = DateTime.Now,
                RoleId = 2,
            };
            dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();
            return new() { Succeeded = true, Message = "User created!" };
        }

        public async Task<User?> GetUser(LoginRequest request)
        {
            User? user = await dataContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            await dataContext.Entry(user).Reference(u => u.Country).LoadAsync();
            await dataContext.Entry(user).Reference(u => u.Role).LoadAsync();
            if (user.Fines != null) await dataContext.Entry(user).Collection(u => u.Fines!).LoadAsync();
            if (user.UserBookBorrows != null) await dataContext.Entry(user).Collection(u => u.UserBookBorrows!)
                .Query().Where(ubb => ubb.BorrowSituationId == 1).Include(ubb => ubb.Book).LoadAsync();
            if (user.UserBookReserves != null) await dataContext.Entry(user).Collection(u => u.UserBookReserves!)
                .Query().Where(ubr => ubr.ReserveSituationId == 1).Include(ubr => ubr.Book).LoadAsync();
            return user;
        }
        public async Task<User?> GetUserWithRefreshToken(string refreshToken)
        {
            User? user = await dataContext.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenEndDate < DateTime.UtcNow)
                return null;

            await dataContext.Entry(user).Reference(u => u.Country).LoadAsync();
            await dataContext.Entry(user).Reference(u => u.Role).LoadAsync();
            if (user.Fines != null) await dataContext.Entry(user).Collection(u => u.Fines!).LoadAsync();
            if (user.UserBookBorrows != null) await dataContext.Entry(user).Collection(u => u.UserBookBorrows!)
                .Query().Where(ubb => ubb.BorrowSituationId == 1).Include(ubb => ubb.Book).LoadAsync();
            if (user.UserBookReserves != null) await dataContext.Entry(user).Collection(u => u.UserBookReserves!)
                .Query().Where(ubr => ubr.ReserveSituationId == 1).Include(ubr => ubr.Book).LoadAsync();
            return user;
        }
        public async Task UpdateRefreshToken(User user, Token token)
        {
            int additionalMinute = int.Parse(configuration["Token:RefreshTokenMinute"] ?? throw new Exception("RefreshTokenMinute not found!"));
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(additionalMinute);
            dataContext.Users.Update(user);
            await dataContext.SaveChangesAsync();
        }
        static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
