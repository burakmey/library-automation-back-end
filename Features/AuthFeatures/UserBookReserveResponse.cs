﻿namespace library_automation_back_end.Features.AuthFeatures
{
    public class UserBookReserveResponse(UserBookReserve userBookReserve)
    {
        public DateTime ReserveDate { get; set; } = userBookReserve.ReserveDate;
        public DateTime ReserveDueDate { get; set; } = userBookReserve.ReserveDueDate;
        public string ReserveSituation { get; set; } = userBookReserve.ReserveSituation!.Situation;
        public string BookName { get; set; } = userBookReserve.Book!.Name;
    }
}
