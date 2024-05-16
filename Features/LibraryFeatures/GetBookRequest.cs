namespace library_automation_back_end.Features.LibraryFeatures
{
    public class GetBookRequest
    {
        [Required][FromQuery(Name = "book-id")] public int BookId { get; set; }
    }
}
