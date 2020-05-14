using Cw05.DTOs.Requests;
using System;
using System.Collections.Generic;

namespace Cw05.Models
{
    public partial class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }

        public virtual Enrollment IdEnrollmentNavigation { get; set; }

        //public string Studies { get; set; }


        public Student(ModifyStudentRequest msr)
        {
            IndexNumber = msr.IndexNumber;
            FirstName = msr.FirstName;
            LastName = msr.LastName;
            BirthDate = msr.BirthDate;
        }
        //public Student(EnrollStudentRequest esr)
        //{
        //    this.IndexNumber = esr.IndexNumber;
        //    this.FirstName = esr.FirstName;
        //    this.LastName = esr.LastName;
        //    this.BirthDate = esr.BirthDate;
        //    this.Studies = esr.Studies;
        //}

        public Student()
        {
        }
    }
}
