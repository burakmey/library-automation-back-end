﻿namespace library_automation_back_end.Features.FeaturesAuth.Responses
{
    public class UserDesireResponseModel(UserDesire userDesire)
    {
        public int Id { get; set; } = userDesire.Id;
        public string BookName { get; set; } = userDesire.Book!.Name;
        public string Situation { get; set; } = userDesire.DesireSituation!.Situation;
    }
}
