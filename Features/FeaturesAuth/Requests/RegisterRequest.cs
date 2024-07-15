namespace library_automation_back_end.Features.FeaturesAuth.Requests
{
    public class RegisterRequest
    {
        [Required, EmailAddress] public required string Email { get; set; }
        [Required] public required string Name { get; set; }
        [Required] public required string Surname { get; set; }
        [Required] public int CountryId { get; set; }
        [Required, MinLength(6, ErrorMessage = "Password should not be less than 6 character!")] public required string Password { get; set; }
        [Required, Compare("Password")] public required string ConfirmPassword { get; set; }
    }
}
