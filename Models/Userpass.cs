namespace Minicore_Backend.Models
{
    public class UserPass
    {
        public UserPass() { }

        public UserPass(int userPassId, int userId, int passtypeId, DateTime purchase)
        {
            this.UserPassId = userPassId;
            this.UserId = userId;
            this.PasstypeId = passtypeId;
            this.Purchase = purchase;
        }
        
        public int UserPassId { set; get; }
        public int UserId { set; get; }
        public int PasstypeId { set; get; }
        public DateTime Purchase { set; get; }
    }
}
