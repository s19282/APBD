using Cw05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw05.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        public EnrollStudentResponse(Enrollment e)
        {
            IdEnrollment = e.IdEnrollment;
            Semester = e.Semester;
            IdStudy = e.IdStudy;
            StartDate = e.StartDate;
        }

    }
}
