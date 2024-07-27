namespace API.Entities
{
    public class Connection
    {
        public Connection(){
            
        }
        public Connection(string ConnectionId, string username)
        {
            ConnectionId = ConnectionId;
            username = username;
        }

        public string ConnectionId { get; set; }

        public string Username { get; set; }
    }
}