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
    public class MISController : Controller
    {
        // GET: MIS
        public ActionResult Index(string search, string field)
        {
            List<RegisterAccnt> NewStudent = new List<RegisterAccnt>();
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                con.Open();
                string getRegList = "select * from studenticc.dbo.selMISDetail;";
                if (search != null)
                {
                    getRegList = $"select * from studenticc.dbo.selMISDetail where {field} like '%' +@search+'%';";
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
                            newAcc.StudentNo = (string)readList["StudentNo"];                 
                            newAcc.FullName = (string)readList["Name"];
                            newAcc.Address = (string)readList["Address"];
                            newAcc.Address = Convert.IsDBNull(newAcc.Address) ? null : (string)newAcc.Address;
                            newAcc.Course = (string)readList["Course"];
                            //newAcc.Bdate = (string)readList["Birthdate"];
                            newAcc.Contact = (string)readList["Contact"];
                            newAcc.EmerName = (string)readList["EmerName"];
                            newAcc.EmerContact = (string)readList["EmerContact"];
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
        public ActionResult ViewDetail(int RegId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from studenticc.dbo.tbl_enroll where RegisterId=@RegisterId", con);
                cmd.Parameters.AddWithValue("@RegisterId", RegId);

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    RegisterAccnt acc = new RegisterAccnt
                    {
                        RegId = (int)rd["RegisterId"],
                        FullName = (string)rd["Name"],
                        StudentNo = (string)rd["StudentNo"],
                        GetPhoto = (bool)rd["Photo"],
                        GetEmail = (bool)rd["WithEmail"],
                        SchoolEmail = (string)rd["SchoolEmail"]
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
        public ActionResult SaveMISDetail(RegisterAccnt reg)
            {
                RegisterAccnt acc = TempData["RegisterAccnts"] as RegisterAccnt;
                string UpdStr = "update studenticc.dbo.tbl_enroll set Photo=@Photo,WithEmail=@WithEmail,SchoolEmail=@SchoolEmail where RegisterId=@RegisterId";
                using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand(UpdStr, con))
                    {
                        cmd.Parameters.Add("RegisterId", System.Data.SqlDbType.Int).Value = acc.RegId;
                        cmd.Parameters.Add("@Photo", System.Data.SqlDbType.Bit).Value = reg.GetPhoto;
                        cmd.Parameters.Add("@WithEmail", System.Data.SqlDbType.Bit).Value = reg.GetEmail;
                        cmd.Parameters.Add("@SchoolEmail", System.Data.SqlDbType.VarChar, 50).Value = reg.SchoolEmail;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    return RedirectToAction("Index");
                }

            }
    
    }
}