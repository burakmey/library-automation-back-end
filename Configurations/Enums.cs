namespace library_automation_back_end.Configurations
{
    public enum BorrowSituationEnum
    {
        Borrowed = 1,
        Returned,
        TimeOut
    }
    public enum DesireSituationEnum
    {
        Borrow = 1,
        ReserveBorrow,
        Return
    }
    public enum ReserveSituationEnum
    {
        Waiting = 1,
        Borrowed,
        TimeOut
    }
}
