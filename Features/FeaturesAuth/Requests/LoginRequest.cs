namespace library_automation_back_end.Features.FeaturesAuth.Requests
{
    public class LoginRequest
    {
        [Required, EmailAddress] public required string Email { get; set; }
        [Required] public required string Password { get; set; }
    }
}
