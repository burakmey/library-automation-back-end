namespace library_automation_back_end.Features.FeaturesAdmin.Responses
{
    public class GetReservedBooksResponse(ICollection<UserBookReserve> userBookReserves)
    {
        public ICollection<UsersBookReserveResponseModel> UserBookReserves { get; set; } = userBookReserves.Select(ubr => new UsersBookReserveResponseModel(ubr)).ToList();

    }
    public class UsersBookReserveResponseModel(UserBookReserve userBookReserve)
    {
        public string UserName { get; set; } = userBookReserve.User!.Name + " " + userBookReserve.User.Surname;
        public string BookName { get; set; } = userBookReserve.Book!.Name;
        public string Situation { get; set; } = userBookReserve.ReserveSituation!.Situation;
    }
}