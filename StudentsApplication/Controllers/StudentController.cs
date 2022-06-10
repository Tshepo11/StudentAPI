using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Serialization;
using StudentsAPI_4_Praxis.Models;

namespace StudentsAPI_4_Praxis.Controllers

//Controller to add API Methods for the Student table
{
    [Route("api/[controller]")]
    //[ApiController]
    public class StudentController : Controller
    {
        //We need to access the configurations from the Appsettings file
        private readonly IConfiguration _appSettingsConfiguration;

        public void ConfigureServices(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            //Enable CORS - restricting other domains
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //services.AddControllersWithViews().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => 
            //options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            //services.AddControllers();

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }
       
        public StudentController(IConfiguration configuration)
        {
            _appSettingsConfiguration = configuration;
        }

        //API Method to get all the Student details
        [HttpGet]
        public JsonResult GetStudent()
        {
            string query = @"SELECT id,Name,DOB,Gender,Grade,School_Code,School_Name FROM DB.dbo.student;";
            DataTable table = new DataTable();
            //Variable to store the DB string
            string sqlDataSource = _appSettingsConfiguration.GetConnectionString("StudentApplicationConn");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
            {
                //Open database
                myConnection.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();

                    //Close the database
                    myConnection.Close();
                    myReader.Close();
                }
            }
            //return the data table
            return new JsonResult(table);
        } 

        [HttpPut]
        public JsonResult Put(Students student, string v)
        {
            string query = @"update dbo.Student set Name = '"+student.Name+@"','"+student.DOB+@"', 
            '"+student.Gender+@"','"+student.Grade+@"','"+student.School_Name+@"','"+student.School_Code+@"' 
            where ID = '"+student.ID+ v;

            DataTable table = new DataTable();
            string sqlDataSource = _appSettingsConfiguration.GetConnectionString("StudentApplicationConn");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using(SqlCommand myCommand = new SqlCommand(query, sqlConnection))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult post(Students student)
        {
            string query = @"insert into DB.dbo.student values('" + student.Name + @"', '" + student.DOB + @"', '" + student.Gender + @"','" + student.Grade +@"', '"+student.School_Name+@"','"+student.School_Code+@"')";
            DataTable table = new DataTable();
            //Variable to store the DB string
            string sqlDataSource = _appSettingsConfiguration.GetConnectionString("StudentApplicationConn");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
            {
                //Open database
                myConnection.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();

                    //Close the database
                    myConnection.Close();
                    myReader.Close();
                }
            }
            //return the data table
            return new JsonResult("Student has been added successfully");
        }
    
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string sql = @"Delete * from DB.dbo.student
                         where ID = " + id + @"";
            DataTable table = new DataTable();
            //Variable to store the DB string
            string sqlDataSource = _appSettingsConfiguration.GetConnectionString("StudentApplicationConn");
            SqlDataReader myReader;
            using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
            {
                //Open database
                myConnection.Open();

                using (SqlCommand myCommand = new SqlCommand(sql, myConnection))
                {
                    myReader = myCommand.ExecuteReader();

                    //Close the database
                    myConnection.Close();
                    myReader.Close();
                }
            }
            //return the data table
            return new JsonResult("Student has been deleted successfully");
        }
    }
    
}
