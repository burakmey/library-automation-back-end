namespace library_automation_back_end.Features.LibraryFeatures
{
    public class SearchBookRequest
    {
        [Required] public required string Search { get; set; }
        [Required] public int Page { get; set; }
        [Required] public int Size { get; set; }
    }
}
