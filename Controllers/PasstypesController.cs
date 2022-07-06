using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;
using System.Diagnostics;
using Minicore_Backend.Models;
using Minicore_Backend.Config;

namespace Minicore_Backend.Controllers
{
    [ApiController]
    [Route("api/pass-types")]
    public class PasstypesController : ControllerBase
    {
        public PasstypesController(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;
            this.env = env;
            this.db = new DBConfig(this.config, this.env).dbConn;
        }
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        private string db;

        [HttpGet]
        public async Task<ActionResult<List<Passtype>>> GetPassTypes()
        {
            List<Passtype> passTypes = new List<Passtype>();
            string readPassTypes = "SELECT * FROM passtypes";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(db))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (NpgsqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = readPassTypes;
                            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var passType = new Passtype();
                                    passType.PasstypeId = reader.GetInt32(0);//Get int from the first column
                                    //Use castings so that nulls get created if needed
                                    passType.Name = reader[1] as string;
                                    passType.MonthsDuration = reader.GetInt32(2);
                                    passType.Cost = reader.GetFloat(3);
                                    passType.PassesAmount = reader.GetInt32(4);
                                    passTypes.Add(passType);//Add passtype to list
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                return Ok(passTypes);
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                return StatusCode(500);
            }
        }
    }
}
