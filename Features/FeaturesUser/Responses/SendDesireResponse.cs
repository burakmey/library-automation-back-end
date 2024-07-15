using library_automation_back_end.Features.FeaturesAuth.Responses;

namespace library_automation_back_end.Features.FeaturesUser.Responses
{
    public class SendDesireResponse(MessageResponse bookResponse, ICollection<UserDesire>? userDesires)
    {
        public string Message { get; set; } = bookResponse.Message;
        public ICollection<UserDesireResponseModel>? Desires { get; set; } = userDesires?.Select(ud => new UserDesireResponseModel(ud)).ToList();
    }
}
