namespace Ordering.Application.Models
{
    public class EmailSettings
    {
        public string? ApiKey { get; set; }
        public string? FromAddress { get; set; }
        public string? FromName { get; set; }
        public string? Host { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int Port { get; set; }
    }
}
