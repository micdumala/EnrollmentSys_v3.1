using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentSys_v3._1.Models
{
    public class StudentAcademic
    {
        public string AccntId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentType { get; set; }
        public string StudentLevel { get; set; }
        public string YearLevel { get; set; }
        public string Course { get; set; }
        public string IsPassed { get; set; }
        public string AcademicStatus { get; set; }
    }
}