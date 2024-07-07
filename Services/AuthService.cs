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
            User? user = await dataContext.Users.Include(u => u.Country).Include(u => u.Role).Include(u => u.Fines)
                .Include(u => u.UserBookBorrows)
                .ThenInclude(ubb => ubb.Book)
                .Include(u => u.UserBookReserves)
                .ThenInclude(ubr => ubr.Book)
                .FirstOrDefaultAsync(user => user.Email == request.Email);
            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;
            if (!user.Fines.Any()) user.Fines = null;
            if (!user.UserBookBorrows.Any()) user.UserBookBorrows = null;
            if (!user.UserBookReserves.Any()) user.UserBookReserves = null;
            return user;
        }
        public async Task<User?> GetUserWithRefreshToken(string refreshToken)
        {

            User? user = await dataContext.Users.Include(u => u.Country).Include(u => u.Role).Include(u => u.Fines)
                .Include(u => u.UserBookBorrows)
                .ThenInclude(ubb => ubb.Book)
                .Include(u => u.UserBookReserves)
                .ThenInclude(ubr => ubr.Book).FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenEndDate < DateTime.UtcNow)
                return null;
            if (!user.Fines.Any()) user.Fines = null;
            if (!user.UserBookBorrows.Any()) user.UserBookBorrows = null;
            if (!user.UserBookReserves.Any()) user.UserBookReserves = null;
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
        public async Task<ICollection<UserDesire>?> GetUserDesires(int userId)
        {
            ICollection<UserDesire> userDesires = await dataContext.UserDesires.Where(ud => ud.UserId == userId).Include(ud => ud.Book).Include(ud => ud.DesireSituation).ToListAsync();
            if (userDesires.Count == 0)
                return null;
            return userDesires;
        }
        public async Task<ICollection<UserBookReserve>?> GetUserReservations(int userId)
        {
            ICollection<UserBookReserve> userReservations = await dataContext.UserBookReserves.Where(ubr => ubr.UserId == userId).Include(ubr => ubr.Book).Include(ubr => ubr.ReserveSituation).ToListAsync();
            if (userReservations.Count == 0)
                return null;
            return userReservations;
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
