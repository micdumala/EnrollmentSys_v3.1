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
    public class CashierController : Controller
    {
        // GET: Cashier
        public ActionResult Index(int? RegId,string search, string field)
        {
            List<RegisterAccnt> NewStudent = new List<RegisterAccnt>();
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                con.Open();
                string getRegList = "select * from studenticc.dbo.vwForPayment;";
                if (search != null)
                {
                    getRegList = $"select * from studenticc.dbo.vwForPayment where {field} like '%' +@search+'%';";
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
                            newAcc.LastName = (string)readList["LastName"];
                            newAcc.FirstName = (string)readList["FirstName"];
                            newAcc.AppStatus = (string)readList["Status"];
                            newAcc.Course = (string)readList["Course"];
                            newAcc.CourseAmount = (int)readList["CAmount"];
                            newAcc.AmountPay = (int)readList["AmountPay"];
                            newAcc.AmountBal = (int)readList["AmountBal"];
                            newAcc.PayTerm = (string)readList["PayTerm"];
                            NewStudent.Add(newAcc);
                        };
                    }
                }
                ViewBag.List = NewStudent;
                ViewData["registeredAccnts"] = NewStudent;
                return View(NewStudent);
            }
        }

        [HttpPost]
        public ActionResult ViewForPayment()
        {
            return View();
        }
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
        //GET: Payment information
        public ActionResult InputPayment(int RegId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from studenticc.dbo.vwForPayment where RegisterId=@RegisterId", con);
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
                        PayTerm=(string)rd["PayTerm"].ToString(),
                        CourseAmount=(int)rd["CAmount"],
                        AmountPay=(int)rd["AmountPay"]
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
        [HttpPost]
        public ActionResult SavePayment(RegisterAccnt reg)
        {
            RegisterAccnt acc = TempData["RegisterAccnts"] as RegisterAccnt;
            string UpdStr = "update studenticc.dbo.tbl_preregister set PayTerm=@PayTerm, AmountPay=@AmountPay, AmountBal=@AmountBal,  Updatedate=@Updatedate  where RegisterId=@RegisterId";
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand(UpdStr, con))
                {
                    cmd.Parameters.Add("RegisterId", System.Data.SqlDbType.Int).Value = acc.RegId;
                    cmd.Parameters.Add("StudentType", System.Data.SqlDbType.VarChar, 50).Value = reg.StudentType;
                    cmd.Parameters.Add("Course", System.Data.SqlDbType.VarChar, 50).Value = reg.Course;
                    cmd.Parameters.Add("LastName", System.Data.SqlDbType.VarChar, 15).Value = reg.LastName;
                    cmd.Parameters.Add("FirstName", System.Data.SqlDbType.VarChar, 15).Value = reg.FirstName;
                    cmd.Parameters.Add("@Updatedate", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@CAmount", System.Data.SqlDbType.Int).Value = reg.CourseAmount;
                    cmd.Parameters.Add("@PayTerm", System.Data.SqlDbType.VarChar,50).Value = reg.PayTerm;
                    cmd.Parameters.Add("@AmountPay", System.Data.SqlDbType.Int).Value = reg.AmountPay;
                    cmd.Parameters.Add("@AmountBal", System.Data.SqlDbType.Int).Value = reg.CourseAmount - reg.AmountPay;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                UpdEnroll(acc.RegId);
                return RedirectToAction("Index");
            }
        }
    private ActionResult UpdEnroll(int RegId)
        {
            string UpdStr = "exec studenticc.dbo.insEnrolled @RegisterId=";
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc"))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand(UpdStr+RegId, con))
                {
                    cmd.Parameters.Add("@RegisterId", System.Data.SqlDbType.Int).Value = RegId; //For Update
                    //cmd.Parameters.Add("@Updatedate", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }

        }
    }

}