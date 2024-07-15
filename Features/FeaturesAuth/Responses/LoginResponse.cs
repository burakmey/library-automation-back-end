namespace library_automation_back_end.Features.FeaturesAuth.Responses
{
    public class LoginResponse(User user, ICollection<UserDesire>? userDesires, Token token)
    {
        public string AccessToken { get; set; } = token.AccessToken;
        public string Email { get; set; } = user.Email;
        public string Name { get; set; } = user.Name;
        public string Surname { get; set; } = user.Surname;
        public DateTime RegisteredAt { get; set; } = user.RegisteredAt;
        public string Nation { get; set; } = user.Country!.Name;
        public string Role { get; set; } = user.Role!.Name;
        public ICollection<Fine>? Fines { get; set; } = user.Fines;
        public ICollection<BorrowedResponseModel>? UserBookBorrows { get; set; } = user.UserBookBorrows?.Select(ubb => new BorrowedResponseModel(ubb)).ToList();
        public ICollection<ReservedResponseModel>? UserBookReserves { get; set; } = user.UserBookReserves?.Select(ubr => new ReservedResponseModel(ubr)).ToList();
        public ICollection<UserDesireResponseModel>? Desires { get; set; } = userDesires?.Select(ud => new UserDesireResponseModel(ud)).ToList();
    }
}
