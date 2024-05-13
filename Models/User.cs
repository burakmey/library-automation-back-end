namespace library_automation_back_end.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required byte[] PasswordHash { get; set; } = new byte[32];
        public required byte[] PasswordSalt { get; set; } = new byte[32];
        public required DateTime RegisteredAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        [ForeignKey(nameof(Country))] public required int CountryId { get; set; }
        [ForeignKey(nameof(Role))] public required int RoleId { get; set; }
        public Country? Country { get; set; }
        public Role? Role { get; set; }
        public ICollection<Fine>? Fines { get; set; }
        public ICollection<UserBookBorrow>? UserBookBorrows { get; set; }
        public ICollection<UserBookReserve>? UserBookReserves { get; set; }
    }
}
