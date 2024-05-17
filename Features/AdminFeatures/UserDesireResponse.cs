namespace library_automation_back_end.Features.AdminFeatures
{
    public class UserDesireResponse(UserDesire userDesire)
    {
        public int Id { get; set; } = userDesire.Id;
        public string UserName { get; set; } = userDesire.User!.Name + userDesire.User.Surname;
        public string BookName { get; set; } = userDesire.Book!.Name;
        public int DesireSituationId { get; set; } = userDesire.DesireSituationId;
        public string DesireSituation { get; set; } = userDesire.DesireSituation!.Situation;
    }
}
