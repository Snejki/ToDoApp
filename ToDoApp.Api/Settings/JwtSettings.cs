namespace ToDoApp.Api.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public int ExpiryTime { get; set; }
        public string Issuer { get; set; }
    }
}
