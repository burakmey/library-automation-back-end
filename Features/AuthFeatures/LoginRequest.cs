﻿namespace library_automation_back_end.Features.AuthFeatures
{
    public class LoginRequest
    {
        [Required, EmailAddress] public required string Email { get; set; }
        [Required] public required string Password { get; set; }
    }
}
