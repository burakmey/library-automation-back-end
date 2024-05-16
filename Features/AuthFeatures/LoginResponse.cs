namespace library_automation_back_end.Features.AuthFeatures
{
    public class LoginResponse(User user, Token token)
    {
        public string AccessToken { get; set; } = token.AccessToken;
        public string Email { get; set; } = user.Email;
        public string Name { get; set; } = user.Name;
        public string Surname { get; set; } = user.Surname;
        public DateTime RegisteredAt { get; set; } = user.RegisteredAt;
        public string Nation { get; set; } = user.Country!.Name;
        public string Role { get; set; } = user.Role!.Name;
        public ICollection<Fine>? Fines { get; set; } = user.Fines;
        public ICollection<UserBookBorrow>? UserBookBorrows { get; set; } = user.UserBookBorrows;
        public ICollection<UserBookReserve>? UserBookReserves { get; set; } = user.UserBookReserves;
    }
}
