namespace library_automation_back_end.Features.AdminFeatures
{
    public class GetAllDesiresResponse(ICollection<UserDesire> userDesires)
    {
        public ICollection<UserDesireResponse> UserDesireResponses { get; set; } = userDesires.Select(ud => new UserDesireResponse(ud)).ToList();
    }
}
