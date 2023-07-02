using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrollmentSys_v3._1.Models;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;
using EnrollmentSys_v3._1.Context;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace EnrollmentSys_v3._1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Display Registration Entry
        public ActionResult Index()
        {
            return View("Index");
        }
        // GET: Search by field
        public ActionResult NewStudent(string search, string field)
        {
            List<RegisterAccnt> NewStudent = new List<RegisterAccnt>();
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                con.Open();
                string getRegList = "select * from studenticc.dbo.vwRegister where AppStatus=1";
                if (search!=null)
                {
                    getRegList = $"select * from studenticc.dbo.vwRegister where AppStatus=1 and {field} like '%' +@search+'%';";   
                }
                
                using (SqlCommand cmd = new SqlCommand(getRegList, con))
                {
                    if (search != null)
                    {
                        cmd.Parameters.AddWithValue("@search", search);
                    }
                    using (SqlDataReader readList = cmd.ExecuteReader())
                    {
                        while (readList.Read())
                        {                         
                            RegisterAccnt newAcc = new RegisterAccnt();
                            newAcc.RegId =(int)readList["RegisterId"];
                            newAcc.StudentType = (string)readList["StudentType"];
                            newAcc.Course = (string)readList["Course"];
                            newAcc.LastName = (string)readList["LastName"];
                            newAcc.FirstName = (string)readList["FirstName"];
                            newAcc.Gender = (string)readList["Gender"];
                            newAcc.AppStatus = (string)readList["Status"];
                            //newAcc.Course = Convert.IsDBNull(newAcc.Course) ? null: (string)newAcc.Course;
                            NewStudent.Add(newAcc);
                        };
                    }
                }
                ViewBag.List = NewStudent;
                ViewData["registeredAccnts"] = NewStudent;
                return View(NewStudent);
            }
        }
        //GET: Student Information
        public ActionResult EditView(int RegId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from studenticc.dbo.tbl_preregister where RegisterId=@RegisterId", con);
                cmd.Parameters.AddWithValue("@RegisterId", RegId);

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    RegisterAccnt acc = new RegisterAccnt
                    {
                        RegId = (int)rd["RegisterId"],
                        StudentType = (string)rd["StudentType"].ToString(),
                        Course = (string)rd["Course"].ToString(),
                        LastName = (string)rd["LastName"].ToString(),
                        FirstName = (string)rd["FirstName"].ToString(),
                        MiddleName = (string)rd["MiddleName"].ToString(),
                        Bdate = (string)rd["Birthdate"].ToString(),
                        Gender = (string)rd["Gender"].ToString(),
                        CStatus = (string)rd["CivilStatus"].ToString(),
                        Contact = (string)rd["Contact"].ToString(),
                        Email = (string)rd["Email"].ToString(),
                        ParentName = (string)rd["ParentName"].ToString(),
                        MaidenName = (string)rd["MaidenName"].ToString(),
                        Address = (string)rd["Address"].ToString(),
                        LastSchool = (string)rd["LastSchool"].ToString(),
                        SchoolYr = (string)rd["LastSchoolYr"].ToString(),
                        SchoolAddress = (string)rd["SchoolAddress"].ToString(),
                        Notes = (string)rd["Notes"].ToString(),
                        AcademicRec = (bool)rd["AcademicRec"],
                        GoodMoral = (bool)rd["GoodMoral"],
                        BirthCert = (bool)rd["BirthCert"],
                        RecoLetter = (bool)rd["RecoLetter"]
                    };
                    ViewData["RegisterAccnts"] = acc;
                    TempData["RegisterAccnts"] = ViewData["RegisterAccnts"];
                    return View(acc);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }
        //POST: Account update
        [HttpPost]
        public ActionResult SaveEdit(RegisterAccnt reg)
        {
            RegisterAccnt acc= TempData["RegisterAccnts"] as RegisterAccnt;
            string UpdStr = "update studenticc.dbo.tbl_preregister set StudentType=@StudentType,Course=@Course,LastName=@LastName, FirstName=@FirstName, MiddleName=@MiddleName, Birthdate=@Birthdate, Gender=@Gender, CivilStatus=@CivilStatus, Contact=@Contact, Email=@Email, ParentName=@ParentName, MaidenName=@MaidenName, Address=@Address, LastSchool=@LastSchool, LastSchoolYr=@LastSchoolYr, SchoolAddress=@SchoolAddress, Updatedate=@Updatedate, AcademicRec=@AcademicRec,GoodMoral=@GoodMoral, BirthCert=@BirthCert, RecoLetter=@RecoLetter   where RegisterId=@RegisterId";
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                
                using (SqlCommand cmd = new SqlCommand(UpdStr, con))
                {
                    cmd.Parameters.Add("RegisterId",System.Data.SqlDbType.Int).Value=acc.RegId;
                    cmd.Parameters.Add("StudentType",System.Data.SqlDbType.VarChar,50).Value = reg.StudentType;
                    cmd.Parameters.Add("Course",System.Data.SqlDbType.VarChar,50).Value=reg.Course;
                    cmd.Parameters.Add("LastName", System.Data.SqlDbType.VarChar, 15).Value = reg.LastName;
                    cmd.Parameters.Add("FirstName",System.Data.SqlDbType.VarChar,15).Value=reg.FirstName;
                    cmd.Parameters.Add("MiddleName", System.Data.SqlDbType.VarChar, 15).Value = reg.MiddleName;
                    cmd.Parameters.Add("Birthdate", System.Data.SqlDbType.Date).Value = reg.Bdate;
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
                    cmd.Parameters.Add("@Updatedate", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("AcademicRec", System.Data.SqlDbType.Bit).Value = reg.AcademicRec;
                    cmd.Parameters.Add("GoodMoral", System.Data.SqlDbType.Bit).Value = reg.GoodMoral;
                    cmd.Parameters.Add("BirthCert", System.Data.SqlDbType.Bit).Value = reg.BirthCert;
                    cmd.Parameters.Add("RecoLetter", System.Data.SqlDbType.Bit).Value = reg.RecoLetter;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                UpdStatus(acc.RegId);
                return RedirectToAction("ViewForReq");
            }
        }

        //GET: Check for Requirements
        public ActionResult ViewForReq(int? RegId, string search, string field)
        {
            List<RegisterAccnt> NewStudent = new List<RegisterAccnt>();
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                con.Open();
                string getRegList = "select * from studenticc.dbo.vwForRequirements";
                if(RegId != null)
                {
                    getRegList = getRegList + " where AppStatus=2 and RegisterId = " + RegId;
                }
                if (search != null)
                {
                    getRegList = $"select * from studenticc.dbo.vwForRequirements where AppStatus=2 and {field} like '%' +@search+'%';";
                }

                using (SqlCommand cmd = new SqlCommand(getRegList, con))
                {
                    if (search != null)
                    {
                        cmd.Parameters.AddWithValue("@search", search);
                    }
                    using (SqlDataReader readList = cmd.ExecuteReader())
                    {
                        while (readList.Read())
                        {
                            RegisterAccnt newAcc = new RegisterAccnt();
                            newAcc.RegId = (int)readList["RegisterId"];
                            newAcc.StudentType = (string)readList["StudentType"];
                            newAcc.Course = (string)readList["Course"];
                            newAcc.LastName = (string)readList["LastName"];
                            newAcc.FirstName = (string)readList["FirstName"];
                            newAcc.AcademicRec = (bool)readList["AcademicRec"];
                            newAcc.GoodMoral = (bool)readList["GoodMoral"];
                            newAcc.BirthCert = (bool)readList["BirthCert"];
                            newAcc.RecoLetter = (bool)readList["RecoLetter"];
                            newAcc.AppStatus = (string)readList["Status"];
                            NewStudent.Add(newAcc);
                        };
                    }
                }
                ViewBag.List = NewStudent;
                ViewData["registeredAccnts"] = NewStudent;
                return View(NewStudent);
            }
        }

        // POST: Update Status
        [HttpPost]
        private ActionResult UpdStatus(int RegId)
        {
            string UpdStr = "update studenticc.dbo.tbl_preregister set AppStatus=2 where RegisterId=@RegisterId";
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand(UpdStr, con))
                {
                    cmd.Parameters.Add("@RegisterId", System.Data.SqlDbType.Int).Value = RegId; //For Update
                    cmd.Parameters.Add("@Updatedate", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("NewStudent");
            }
        }

        public ActionResult GoTo(int RegId)
        {
            UpdStatus(RegId);
            return RedirectToAction("NewStudent");
        }
         
        //GET: Student Type drop list
        private SelectList GetStudentyType()
        {
            var StudType = new List<SelectListItem>
            {
                new SelectListItem {Value="Incoming 1st Year",Text="Incoming 1st Year"},
                new SelectListItem {Value="Incoming 1st Year",Text="Transferee"},
                new SelectListItem {Value="Incoming 1st Year",Text="On-Going Student"},
                new SelectListItem {Value="Incoming 1st Year",Text="For CPE Only"},
            };
            return new SelectList(StudType, "Value", "Text");
        }
        //POST: Test only
        [HttpPost]
        public ActionResult InsertDate()
        {
            using(SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                using (SqlCommand cmd = new SqlCommand("Insert into dbo.tblCreatedate (Createdate) values (@Createdate);",con))
                {
                    cmd.Parameters.Add("@Createdate", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            return View();
        }

    } 
}
