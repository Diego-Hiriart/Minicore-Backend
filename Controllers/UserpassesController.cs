using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;
using System.Diagnostics;
using Minicore_Backend.Models;
using Minicore_Backend.Config;


namespace Minicore_Backend.Controllers
{
    [ApiController]
    [Route("api/user-passes")]
    public class UserPassesController : ControllerBase
    {
        //A constructor for this class is needed so that when it is called the config and evnironment info needed are passed
        public UserPassesController(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;
            this.env = env;
            this.db = new DBConfig(this.config, this.env).dbConn;
        }
        //These configurations and environment info are needed to create a DBConfig instance that has the right connection string depending on whether the app is running on a development or production environment
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        private string db;//Connection string

        [HttpGet]
        public async Task<ActionResult<List<UserPass>>> GetUserPasses()
        {
            List<UserPass> userPasses = new List<UserPass>();
            string readUserPasses = "SELECT * FROM userpasses";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(db))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (NpgsqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = readUserPasses;
                            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var userPass = new UserPass();
                                    userPass.UserPassId = reader.GetInt32(0);//Get int from the first column
                                    //Use castings so that nulls get created if needed
                                    userPass.UserId = reader.GetInt32(1);
                                    userPass.PasstypeId = reader.GetInt32(2);
                                    userPass.Purchase = reader.GetDateTime(3);
                                    userPasses.Add(userPass);//Add userpass to list
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                return Ok(userPasses);
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                return StatusCode(500,new object[] { eSql.Message });
            }
        }
    }
}
