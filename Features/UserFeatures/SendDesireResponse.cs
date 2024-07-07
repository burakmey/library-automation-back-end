namespace library_automation_back_end.Features.UserFeatures
{
    public class SendDesireResponse(BookResponse bookResponse, ICollection<UserDesire>? userDesires)
    {
        public string Message { get; set; } = bookResponse.Message;
        public ICollection<UserDesireResponse>? Desires { get; set; } = userDesires?.Select(ud => new UserDesireResponse(ud)).ToList();
    }
}
