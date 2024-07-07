namespace library_automation_back_end.Features.UserFeatures
{
    public class ReserveResponse(BookResponse bookResponse, ICollection<UserBookReserve>? userReservations)
    {
        public string Message { get; set; } = bookResponse.Message;
        public ICollection<UserBookReserveResponse>? Reservations { get; set; } = userReservations?.Select(ubr => new UserBookReserveResponse(ubr)).ToList();
    }
}
