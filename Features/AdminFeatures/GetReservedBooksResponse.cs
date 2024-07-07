namespace library_automation_back_end.Features.AdminFeatures
{
    public class GetReservedBooksResponse(ICollection<UserBookReserve> userBookReserves)
    {
        public ICollection<UsersBookReserveResponse> UserBookReserves { get; set; } = userBookReserves.Select(ubr => new UsersBookReserveResponse(ubr)).ToList();

    }
}
