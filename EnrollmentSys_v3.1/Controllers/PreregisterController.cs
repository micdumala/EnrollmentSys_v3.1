using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrollmentSys_v3._1.Models;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;


namespace EnrollmentSys_v3._1.Controllers
{
    public class PreregisterController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc";
        }
        
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Courses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verify(RegisterAccnt reg)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            /*cmd.CommandText = "select count(RegisterId) from studenticc.dbo.tbl_preregister where FirstName='" + reg.FirstName + "' and LastName='" + reg.LastName + "'and MiddleName='" + reg.MiddleName + "' and Course='"+reg.Course +"';";
            object accRead = cmd.ExecuteScalar();
            int accNum = Convert.ToInt32(accRead);
            if (accNum > 0)
            {
                con.Close();
                return View("Failed");
            }
            else
            {*/
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                string cmdInsInfo = "Insert into studenticc.dbo.tbl_preregister (StudentType,Course,LastName,FirstName,MiddleName,Birthdate,Gender,CivilStatus,Contact,Email,ParentName,MaidenName,Address,LastSchool,LastSchoolYr,SchoolAddress,Createdate,Updatedate,EmerName,EmerContact) " +
                    "values(@StudentType,@Course,@LastName,@FirstName,@MiddleName,@Birthdate,@Gender,@CivilStatus,@Contact,@Email,@ParentName,@MaidenName,@Address,@LastSchool,@LastSchoolYr,@SchoolAddress,@Createdate,@Updatedate,@EmerName,@EmerContact)";

                using (SqlCommand cmd = new SqlCommand(cmdInsInfo, con))
                {
                    cmd.Parameters.Add("@StudentType", System.Data.SqlDbType.VarChar,50).Value = reg.StudentType;
                    cmd.Parameters.Add("@Course", System.Data.SqlDbType.VarChar, 50).Value = reg.Course;
                    cmd.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar, 50).Value = reg.LastName;
                    cmd.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar, 50).Value = reg.FirstName;
                    cmd.Parameters.Add("@MiddleName", System.Data.SqlDbType.VarChar, 50).Value = reg.MiddleName;
                    cmd.Parameters.Add("@Birthdate", System.Data.SqlDbType.Date).Value = reg.Bdate; 
                    cmd.Parameters.Add("@Gender", System.Data.SqlDbType.VarChar, 10).Value = reg.Gender;
                    cmd.Parameters.Add("@CivilStatus", System.Data.SqlDbType.VarChar, 10).Value = reg.CStatus;
                    cmd.Parameters.Add("@Contact", System.Data.SqlDbType.VarChar, 11).Value = reg.Contact;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 30).Value = reg.Email;
                    cmd.Parameters.Add("@ParentName", System.Data.SqlDbType.VarChar, 50).Value = reg.ParentName;
                    cmd.Parameters.Add("@MaidenName", System.Data.SqlDbType.VarChar, 50).Value = reg.MaidenName;
                    cmd.Parameters.Add("@Address", System.Data.SqlDbType.Text).Value = reg.Address;
                    cmd.Parameters.Add("@LastSchool", System.Data.SqlDbType.VarChar, 50).Value = reg.LastSchool;
                    cmd.Parameters.Add("@LastSchoolYr", System.Data.SqlDbType.VarChar, 4).Value = reg.SchoolYr;
                    cmd.Parameters.Add("@SchoolAddress", System.Data.SqlDbType.Text).Value = reg.SchoolAddress;
                    //cmd.Parameters.Add("@Notes", System.Data.SqlDbType.Text).Value = reg.Notes;
                    cmd.Parameters.Add("@Createdate", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Updatedate", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@EmerName", System.Data.SqlDbType.VarChar, 50).Value = reg.EmerName;
                    cmd.Parameters.Add("@EmerContact", System.Data.SqlDbType.VarChar, 11).Value = reg.EmerContact;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }                
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select max(RegisterId) from studenticc.dbo.tbl_preregister;";
                object getRegId = cmd.ExecuteScalar();
                int lastResult = Convert.ToInt32(getRegId);
                ViewData["RegId"] = lastResult;
                return View("Success");
            //}
        }
    }

}