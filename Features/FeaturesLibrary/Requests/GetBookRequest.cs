namespace library_automation_back_end.Features.FeaturesLibrary.Requests
{
    public class GetBookRequest
    {
        [Required] public int BookId { get; set; }
    }
}
