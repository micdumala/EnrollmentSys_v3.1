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
    public class LoginController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=studenticc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; database=studenticc";
        }
        [HttpPost]
        public ActionResult Check(Login acc)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from studenticc.dbo.tbl_user where Username='" + acc.Username + "' and Password='" + acc.Password + "';";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                con.Close();
                string erMsg = "Incorrect Username/Password";
                ViewData["erMsg"] = erMsg;
                return View("Login");
            }
        }
    }
}