namespace library_automation_back_end.Features.FeaturesAdmin.Requests
{
    public class AddCategoryRequest
    {
        [Required][MinLength(1, ErrorMessage = "At least one category must be exist!")] public required ICollection<CategoryRequestModel> Categories { get; set; }
    }
    public class CategoryRequestModel
    {
        [Required] public required string Name { get; set; }
    }
}
