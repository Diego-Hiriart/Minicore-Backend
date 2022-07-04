namespace Minicore_Backend.Models
{
    public class Passtype
    {
        public Passtype(){}

        public Passtype(int passtypeid, string name, int daysduration, float cost, int passesnumber)
        {
            this.passtypeid = passtypeid;
            this.name = name;
            this.daysduration = daysduration;
            this.cost = cost;
            this.passesnumber = passesnumber;
        }

        public int passtypeid { set; get; }

        public string name { set; get; }

        public int daysduration { set; get; }

        public float cost { set; get; }

        public int passesnumber { set; get; }
    }
}
