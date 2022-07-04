namespace Minicore_Backend.Models
{
    public class Userpass
    {
        public Userpass() { }

        public Userpass(int userpassid, int userid, int passtypeid, DateTime purchase)
        {
            this.userpassid = userpassid;
            this.userid = userid;
            this.passtypeid = passtypeid;
            this.purchase = purchase;
        }
        
        public int userpassid { set; get; }

        public int userid { set; get; }

        public int passtypeid { set; get; }

        public DateTime purchase { set; get; }
    }
}
