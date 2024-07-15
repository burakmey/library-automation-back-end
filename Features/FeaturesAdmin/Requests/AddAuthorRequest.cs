namespace library_automation_back_end.Features.FeaturesAdmin.Requests
{
    public class AddAuthorRequest
    {
        [Required][MinLength(1, ErrorMessage = "At least one author must be exist!")] public required ICollection<AuthorRequestModel> Authors { get; set; }
    }
    public class AuthorRequestModel
    {
        [Required] public required string Name { get; set; }

        [Required] public required string Country { get; set; }
    }
}
