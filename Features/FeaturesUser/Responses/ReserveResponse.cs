using library_automation_back_end.Features.FeaturesAuth.Responses;

namespace library_automation_back_end.Features.FeaturesUser.Responses
{
    public class ReserveResponse(MessageResponse bookResponse, ICollection<UserBookReserve>? userReservations)
    {
        public string Message { get; set; } = bookResponse.Message;
        public ICollection<ReservedResponseModel>? Reservations { get; set; } = userReservations?.Select(ubr => new ReservedResponseModel(ubr)).ToList();
    }
}
