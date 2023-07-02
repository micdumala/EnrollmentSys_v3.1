using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentSys_v3._1.Models
{
    public class RegisterAccnt
    {
        public int RegId { get; set; }
        public string StudentType { get; set; }
        public string Course { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Bdate { get; set; }
        public string Gender { get; set; }
        public string CStatus { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string ParentName { get; set; }
        public string MaidenName { get; set; }
        public string Address { get; set; }
        public string LastSchool { get; set; }
        public string SchoolYr { get; set; }
        public string SchoolAddress { get; set; }
        public string Notes { get; set; }
        public string AppStatus { get; set; }
        public bool AcademicRec { get; set; }
        public bool GoodMoral { get; set; }
        public bool BirthCert { get; set; }
        public bool RecoLetter { get; set;}
        public int CourseAmount { get; set; }
        public int AmountPay { get; set; }
        public int DocStatus { get; set; }
        public string PayTerm { get; set; }
        public int AmountBal { get; set; }
        public string search { get; set; }
        public string field { get; set; }
        public string StudentNo { get; set; }
        public string EmerName { get; set; }
        public string EmerContact { get; set; }
        public string FullName { get; set; }
        public string SchoolEmail { get; set; }
        public bool GetPhoto { get; set; }
        public bool GetEmail { get; set; }
    }

}