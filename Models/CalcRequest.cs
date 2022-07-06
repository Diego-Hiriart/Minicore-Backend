namespace Minicore_Backend.Models
{
    public class CalcRequest
    {
        public CalcRequest(){ }

        public CalcRequest(DateTime startDate)
        {
            this.StartDate = startDate;
        }

        public DateTime StartDate { set; get; }
    }
}
