namespace library_automation_back_end.Features.AdminFeatures
{
    public class UsersBookReserveResponse(UserBookReserve userBookReserve)
    {
        public string UserName { get; set; } = userBookReserve.User!.Name + " " + userBookReserve.User.Surname;
        public string BookName { get; set; } = userBookReserve.Book!.Name;
        public string Situation { get; set; } = userBookReserve.ReserveSituation!.Situation;
    }
}
