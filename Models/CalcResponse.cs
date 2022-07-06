namespace Minicore_Backend.Models
{
    public class CalcResponse
    {
        public CalcResponse() { }

        public CalcResponse(int UserId, string Username, string Email, 
            int PasstypeId, string Name, int UserPassId, DateTime Purchase, DateTime EstimatedEndDate, int EstmRemainingPasses)
        {
            this.UserId = UserId;
            this.Username = Username;
            this.Email = Email;
            this.PasstypeId = PasstypeId;
            this.Name = Name;
            this.UserPassId = UserPassId;
            this.Purchase = Purchase;
            this.EstimatedEndDate = EstimatedEndDate;
            this.EstmRemainingPasses = EstmRemainingPasses;
        }
        
        public int UserId { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public int PasstypeId { set; get; }
        public string Name { set; get; }
        public int UserPassId { set; get; }
        public DateTime Purchase { set; get; }
        public DateTime EstimatedEndDate { set; get; }
        public int EstmRemainingPasses { set; get; }
    }
}
