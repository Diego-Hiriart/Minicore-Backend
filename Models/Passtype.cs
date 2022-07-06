namespace Minicore_Backend.Models
{
    public class Passtype
    {
        public Passtype(){}

        public Passtype(int passtypeId, string name, int monthsDuration, float cost, int passesAmount)
        {
            this.PasstypeId = passtypeId;
            this.Name = name;
            this.MonthsDuration = MonthsDuration;
            this.Cost = cost;
            this.PassesAmount = passesAmount;
        }

        public int PasstypeId { set; get; }
        public string Name { set; get; }
        public int MonthsDuration { set; get; }
        public float Cost { set; get; }
        public int PassesAmount { set; get; }
    }
}
