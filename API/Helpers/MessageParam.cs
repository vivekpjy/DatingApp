namespace API.Helpers
{
    public class MessageParam : PaginationParams
    {
        public string? Username { get; set; }
        public string Container { get; set; } = "Unread";
    }
}