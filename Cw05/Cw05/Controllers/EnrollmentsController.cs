using Cw05.DTOs.Requests;
using Cw05.DTOs.Responses;
using Cw05.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq;

namespace Cw05.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    [Authorize(Roles ="employee")]
    public class EnrollmentsController : ControllerBase
    {

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse esr;
            var db = new s19282Context();

            var studies = db.Studies.Where(s => s.Name.Equals(request.Studies)).FirstOrDefault();
            if (studies==null)
                return BadRequest("Studia nie istnieją");

            var enrollment = db.Enrollment.Where(s => s.IdStudy == studies.IdStudy && s.Semester==1).OrderBy(s => s.StartDate).First();
            
            if (enrollment == null)
            {
                enrollment = new Enrollment
                {
                    IdEnrollment = db.Enrollment.OrderBy(e => e.IdEnrollment).Last().IdEnrollment + 1,
                    Semester = 1,
                    IdStudy = studies.IdStudy,
                    StartDate=DateAndTime.Today
                };
                db.Attach(enrollment);
                db.Entry(enrollment).State = EntityState.Modified;
            }
            var student = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = enrollment.IdEnrollment
            };
            db.Attach(student);
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();

            esr = new EnrollStudentResponse(enrollment);
            return Created("",esr);
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            var db = new s19282Context();
            var studies = db.Enrollment
                .Join(db.Studies, e => e.IdStudy, s => s.IdStudy, (e, s) => new { s.Name, s.IdStudy, e.Semester,e.IdEnrollment })
                .Where(e => e.Semester == request.Semester && e.Name.Equals(request.Studies)).FirstOrDefault();
            if (studies == null)
                return NotFound();

            var enrollment = db.Enrollment.Where(e => e.Semester == studies.Semester+1)
                .Join(db.Studies,e=>e.IdEnrollment,s=>s.IdStudy,(e,s)=> new { e.IdEnrollment, e.Semester, e.IdStudy, e.StartDate }).FirstOrDefault();
            if(enrollment==null)
            {
                enrollment = new
                {
                    IdEnrollment = db.Enrollment.OrderBy(e => e.IdEnrollment).Last().IdEnrollment + 1,
                    Semester = studies.Semester+1,
                    studies.IdStudy,
                    StartDate = DateAndTime.Today
                };
                db.Attach(enrollment);
                db.Entry(enrollment).State = EntityState.Modified;
            }

            var updateStudents = db.Student.Where(s => s.IdEnrollment == studies.IdEnrollment);
            foreach(var s in updateStudents)
            {
                s.IdEnrollment = enrollment.IdEnrollment;
            }
            db.Attach(updateStudents);
            db.Entry(updateStudents).State = EntityState.Modified;
            db.SaveChanges();

            
            return Created("",enrollment);
        }

    }
}