using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace library_automation_back_end.Services
{
    public class TokenService(IConfiguration configuration)
    {
        readonly IConfiguration configuration = configuration;
        public Token CreateAccessToken(User user, string? refreshToken = null)
        {
            int expirationMinute = int.Parse(configuration["Token:AccessTokenMinute"] ?? throw new Exception("AccessTokenMinute not found!"));
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["SECURITY_KEY"] ?? throw new Exception("SECURITY_KEY not found!")));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name + " " + user.Surname),
                new Claim(ClaimTypes.Role, user.Role!.Name),
            ];
            JwtSecurityToken securityToken = new(
                audience: configuration["Token:Audience"],
                issuer: configuration["Token:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinute),
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler securityTokenHandler = new();
            Token token = new()
            {
                AccessToken = securityTokenHandler.WriteToken(securityToken),
                Expiration = DateTime.UtcNow.AddSeconds(expirationMinute),
                RefreshToken = refreshToken ?? CreateRefreshToken()
            };
            return token;
        }

        public string? GetUserEmailFromAccessToken(string accessToken)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken token = tokenHandler.ReadJwtToken(accessToken);
            string? userEmail = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return userEmail;
        }

        public string CreateRefreshToken()
        {
            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            return Convert.ToBase64String(numbers);
        }
    }
}
