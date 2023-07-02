using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentSys_v3._1.Models
{
    public class Preregister
    {
        [Key]
        public int RegisterId { get; set; }
        public string StudentType { get; set; }
        public string Course { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Birthdate { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string ParentName { get; set; }
        public string MaidenName { get; set; }
        public string Address { get; set; }
        public string LastSchool { get; set; }
        public string LastSchoolYr { get; set; }
        public string SchoolAddress { get; set; }
        public string Notes { get; set; }
        public string EmerName { get; set; }
        public string EmerContact { get; set; }
        public string Createdate { get; set; }
        public string Updatedate { get; set; }


    }
}