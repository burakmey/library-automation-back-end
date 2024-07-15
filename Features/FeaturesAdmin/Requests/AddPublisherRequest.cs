namespace library_automation_back_end.Features.FeaturesAdmin.Requests
{
    public class AddPublisherRequest
    {
        [Required] public required string Name { get; set; }
        [Required] public required string Country { get; set; }
    }
}
