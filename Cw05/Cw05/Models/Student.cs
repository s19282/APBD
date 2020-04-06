using Cw05.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw05.Models
{
    public class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Studies { get; set; }

        public Student(EnrollStudentRequest esr)
        {
            this.IndexNumber = esr.IndexNumber;
            this.FirstName = esr.FirstName;
            this.LastName = esr.LastName;
            this.BirthDate = esr.BirthDate;
            this.Studies = esr.Studies;
        }

    }
}
