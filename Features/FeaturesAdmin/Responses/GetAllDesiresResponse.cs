namespace library_automation_back_end.Features.FeaturesAdmin.Responses
{
    public class GetAllDesiresResponse(ICollection<UserDesire> userDesires)
    {
        public ICollection<UsersDesireResponseModel> UsersDesires { get; set; } = userDesires.Select(ud => new UsersDesireResponseModel(ud)).ToList();
    }
    public class UsersDesireResponseModel(UserDesire userDesire)
    {
        public int Id { get; set; } = userDesire.Id;
        public string UserName { get; set; } = userDesire.User!.Name + " " + userDesire.User.Surname;
        public string BookName { get; set; } = userDesire.Book!.Name;
        public string DesireSituation { get; set; } = userDesire.DesireSituation!.Situation;
    }
}