namespace Minicore_Backend.Models
{
    public class User
    {
        public User() { }

        public User(int userid, string username, string email)
        {
            this.userid = userid;
            this.username = username;
            this.email = email;
        }
        
        public int userid { set; get; }
        public string username { set; get; }

        public string email { set; get; }
    }
}
