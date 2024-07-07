namespace library_automation_back_end.Features.AdminFeatures
{
    public class GetAllDesiresResponse(ICollection<UserDesire> userDesires)
    {
        public ICollection<UsersDesireResponse> UsersDesires { get; set; } = userDesires.Select(ud => new UsersDesireResponse(ud)).ToList();
    }
}
