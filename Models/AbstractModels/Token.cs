namespace library_automation_back_end.Models.AbstractModels
{
    public class Token
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
