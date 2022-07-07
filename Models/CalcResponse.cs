namespace Minicore_Backend.Models
{
    public class CalcResponse
    {
        public CalcResponse() { }

        public CalcResponse(User user, Passtype passType, UserPass userPass, DateTime estimatedEndDate, int estimatedRemainingPasses)
        {
            this.User = user;
            this.PassType = passType;
            this.UserPass = userPass;
            this.EstimatedEndDate = estimatedEndDate;
            this.EstimatedRemainingPasses = estimatedRemainingPasses;
        }
        
        
        public User User { set; get; }
        public Passtype PassType { set; get; }
        public UserPass UserPass { set; get; }
        public DateTime EstimatedEndDate { set; get; }
        public int EstimatedRemainingPasses { set; get; }
    }
}
