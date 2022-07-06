namespace Minicore_Backend.Models
{
    public class User
    {
        public User() { }

        public User(int userId, string username, string email)
        {
            this.UserId = userId;
            this.Username = username;
            this.Email = email;
        }
        
        public int UserId { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
    }
}
