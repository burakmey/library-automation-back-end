namespace library_automation_back_end.Features.FeaturesLibrary.Requests
{
    public class SearchBookRequest
    {
        [Required] public required string Search { get; set; }
        [Required] public int Page { get; set; }
        [Required] public int Size { get; set; }
    }
}
