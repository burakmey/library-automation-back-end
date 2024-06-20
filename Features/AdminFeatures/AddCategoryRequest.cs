namespace library_automation_back_end.Features.AdminFeatures
{
    public class AddCategoryRequest
    {
        [Required] public required ICollection<Category> Categories { get; set; }
    }
}
