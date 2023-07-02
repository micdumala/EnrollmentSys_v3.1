using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EnrollmentSys_v3._1.Models;
using System.Data.Entity;

namespace EnrollmentSys_v3._1.Models
{
    public class StudentDetails: DbContext
    {

        [Key]
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
    }
    

}