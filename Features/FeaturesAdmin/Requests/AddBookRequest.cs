namespace library_automation_back_end.Features.FeaturesAdmin.Requests
{
    public class AddBookRequest
    {
        [Required] public required string Name { get; set; }
        [Required] public int Year { get; set; }
        [Required] public int Count { get; set; }
        [Required] public int PageCount { get; set; }
        [Required] public required string Language { get; set; }
        [Required] public required string Publisher { get; set; }
        [Required][MinLength(1, ErrorMessage = "Authors must not be empty!")] public required ICollection<string> Authors { get; set; }
        [Required][MinLength(1, ErrorMessage = "Categories must not be empty!")] public required ICollection<string> Categories { get; set; }
    }
}