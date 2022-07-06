using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;
using System.Diagnostics;
using Minicore_Backend.Models;
using Minicore_Backend.Config;

namespace Minicore_Backend.Controllers
{
    [ApiController]
    [Route("api/calc")]
    public class CoreController : ControllerBase
    {
        //A constructor for this class is needed so that when it is called the config and evnironment info needed are passed
        public CoreController(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;
            this.env = env;
            this.db = new DBConfig(this.config, this.env).dbConn;
        }
        //These configurations and environment info are needed to create a DBConfig instance that has the right connection string depending on whether the app is running on a development or production environment
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        private string db;//Connection string

        [HttpPost]
        public async Task<ActionResult<List<CalcResponse>>> CalculatePassesData(CalcRequest calcRequest)
        {
            DateTime requestStartDate = calcRequest.StartDate;
            DateTime today = DateTime.Today;
            User user = new User();
            Passtype passType = new Passtype();        
            List<UserPass> userPasses = new List<UserPass>();
            List<CalcResponse> calcResponses = new List<CalcResponse>();
            string readUsers = "SELECT * FROM simpleusers WHERE userid = @0";
            string readPassTypes = "SELECT * FROM passtypes WHERE passtypeid = @0";
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

                        //Calculations
                        foreach (UserPass unfilteredPass in userPasses)
                        {
                            using (NpgsqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = readUsers;
                                cmd.Parameters.AddWithValue("@0", unfilteredPass.UserId);
                                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        user.UserId = reader.GetInt32(0);//Get int from the first column
                                        //Use castings so that nulls get created if needed
                                        user.Username = reader[1] as string;
                                        user.Email = reader[2] as string;
                                    }
                                }
                            }

                            using (NpgsqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = readPassTypes;
                                cmd.Parameters.AddWithValue("@0", unfilteredPass.PasstypeId);
                                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        passType.PasstypeId = reader.GetInt32(0);//Get int from the first column
                                        //Use castings so that nulls get created if needed
                                        passType.Name = reader[1] as string;
                                        passType.MonthsDuration = reader.GetInt32(2);
                                        passType.Cost = reader.GetFloat(3);
                                        passType.PassesAmount = reader.GetInt32(4);
                                    }
                                }
                            }

                            //The used days are the days in which the passes can be used, which are all but Sunday
                            int usedDays = GetBusinessDays(unfilteredPass.Purchase, today, new int[] { 1, 2, 3, 4, 5, 6 });//0 is sunday
                            int remainingPasses = passType.PassesAmount - usedDays;
                            DateTime estimatedEndDate = unfilteredPass.Purchase.AddMonths(passType.MonthsDuration);

                            //Add the result only if conditions are met
                            if (remainingPasses > 0 && estimatedEndDate >= requestStartDate)
                            {
                                calcResponses.Add(new CalcResponse(user.UserId, user.Username, user.Email, passType.PasstypeId, passType.Name,
                                unfilteredPass.UserPassId, unfilteredPass.Purchase, estimatedEndDate, remainingPasses));
                            }
                            
                            //Reset user and passtype
                            user = new User();
                            passType = new Passtype();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                return StatusCode(500);
            }
            return Ok(calcResponses);
        }

        //Get the number of business days in a range of dates, it can be customized to count more than 5 business days 
        private int GetBusinessDays(DateTime start, DateTime end, int[] businessDays)
        {
            if (businessDays is null || businessDays.Length == 0)//If an empty array is passed, set business days as monday to friday
            {
                businessDays = new int[] { 1 , 2, 3, 4, 5};
            }
            int totalBusinessDays = 0;
            for(int i = 0; start.AddDays(i) < end; i++)
            {
                //If the day of the week that is currently being checked is in the businessDays array, the amount of business days must be increased
                if (businessDays.ToList().Contains((int)start.AddDays(i).DayOfWeek))//Casting DayOfWeek to int returns the nuumeric value
                {
                    totalBusinessDays++;
                }
            }
            return totalBusinessDays;
        }
    }
}
