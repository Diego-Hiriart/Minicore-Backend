using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;
using System.Diagnostics;
using Minicore_Backend.Models;
using Minicore_Backend.Config;

namespace Minicore_Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public UsersController(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;
            this.env = env;
            this.db = new DBConfig(this.config, this.env).dbConn;
        }
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        private string db;

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            List<User> users = new List<User>();
            string readUsers = "SELECT * FROM simpleusers";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(db))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (NpgsqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = readUsers;
                            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var user = new User();
                                    user.userid = reader.GetInt32(0);//Get int from the first column
                                    //Use castings so that nulls get created if needed
                                    user.username = reader[1] as string;
                                    user.email = reader[2] as string;
                                    users.Add(user);//Add brand to list
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                return Ok(users);
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                return StatusCode(500);
            }
        }
    }
}
