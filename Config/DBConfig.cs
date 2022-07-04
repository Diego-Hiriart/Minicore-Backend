namespace Minicore_Backend.Config
{
    public class DBConfig
    {
        public DBConfig(IConfiguration Configuration, IWebHostEnvironment Environment)
        {
            this.Configuration = Configuration;
            this.Environment = Environment;
            if (Environment.IsProduction())
            {
                this.dbConn = Configuration.GetConnectionString("Deployment");
            }else if (Environment.IsDevelopment())
            {
                this.dbConn = Configuration.GetConnectionString("Dev");
            }
        }

        public IConfiguration Configuration { set; get; }

        public IWebHostEnvironment Environment { set; get; }

        public string dbConn { set; get; }
    }
}
