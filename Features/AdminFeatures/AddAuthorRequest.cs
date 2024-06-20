namespace library_automation_back_end.Features.AdminFeatures
{
    public class AddAuthorRequest
    {
        [Required] public required ICollection<Author> Authors { get; set; }
    }
}
