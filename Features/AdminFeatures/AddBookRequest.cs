namespace library_automation_back_end.Features.AdminFeatures
{
    public class AddBookRequest
    {
        [Required] public required string Name { get; set; }
        [Required] public int Year { get; set; }
        [Required] public int Count { get; set; }
        [Required] public int PageCount { get; set; }
        [Required] public int LanguageId { get; set; }
        [Required] public int PublisherId { get; set; }
        [Required] public required ICollection<int> AuthorIds { get; set; }
        [Required] public required ICollection<int> CategoryIds { get; set; }
    }
}
